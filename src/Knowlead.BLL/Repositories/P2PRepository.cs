using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Knowlead.BLL.QueryExtensions;
using Knowlead.BLL.Repositories.Interfaces;
using static Knowlead.Common.Constants;
using Knowlead.DAL;
using Knowlead.DomainModel.P2PModels;
using Knowlead.DTO.P2PModels;
using Knowlead.DTO.ResponseModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Knowlead.Common.Exceptions;
using Knowlead.Services.Interfaces;
using Knowlead.DomainModel.NotificationModels;
using static Knowlead.Common.Constants.EnumStatuses;

namespace Knowlead.BLL.Repositories
{
    public class P2PRepository : IP2PRepository
    {
        public static int ConsecutiveP2PMessageLimit = 2;
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly INotificationServices _notificationServices;
        private readonly ITransactionServices _transactionServices;

        public P2PRepository(ApplicationDbContext context,
                             IHostingEnvironment environment,
                             INotificationServices notificationServices,
                             ITransactionServices transactionServices)
        {
            _context = context;
            _environment = environment;
            _notificationServices = notificationServices;
            _transactionServices = transactionServices;
        }

        private bool isBookmarkable(P2P p2p)
        {
            return p2p.Status == P2PStatus.Active;
        }

        private async Task fillP2pBookmark(P2P p2p, Guid applicationUserId)
        {
            await this.fillP2pBookmark(p2p, async () => await _context.P2PBookmarks.Where(x => x.ApplicationUserId.Equals(applicationUserId) && x.P2pId == p2p.P2pId)
                                                         .CountAsync() == 1);
        }

        private async Task fillP2pBookmark(P2P p2p, bool didBookmark)
        {
            await this.fillP2pBookmark(p2p, () => Task.FromResult(didBookmark));
        }

        private async Task fillP2pBookmark(P2P p2p, Func<Task<bool>> predicate)
        {
            p2p.DidBookmark = await predicate();
            p2p.CanBookmark = isBookmarkable(p2p);
        }

        public async Task<P2P> GetP2PTemp(int p2pId, Guid? applicationUserId = null)
        {
            var p2p = await _context.P2p.IncludeEverything().Where(x => x.P2pId == p2pId).FirstOrDefaultAsync();
            if (applicationUserId.HasValue)
            {
                await this.fillP2pBookmark(p2p, applicationUserId.Value);
            }
            return p2p;
        }

        public async Task<IActionResult> GetP2P(int p2pId, Guid applicationUserId)
        {
            var p2p = await GetP2PTemp(p2pId, applicationUserId);
            
            if(p2p == null && !p2p.IsDeleted)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(P2P));

            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<P2PModel>(p2p)
            });
        }

        public async Task<IActionResult> GetMessages(int p2pId, Guid applicationUserId)
        {
            var p2p = await _context.P2p.Where(x => x.P2pId == p2pId).FirstOrDefaultAsync();

            if(p2p == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(P2P));

            IQueryable<P2PMessage> messagesQuery = _context.P2PMessages
                                                           .Where(x => x.P2pId == p2pId)
                                                           .Where(x => x.MessageToId == applicationUserId || x.MessageFromId == applicationUserId)
                                                           .Include(x => x.MessageFrom)
                                                           .Include(x => x.MessageTo); //TODO: Probably optimization is required

            if(p2p.CreatedById != applicationUserId)
                messagesQuery = messagesQuery
                                    .Where(x => x.MessageFromId == applicationUserId || x.MessageToId == applicationUserId);
                
            var messages = await messagesQuery.ToListAsync();

            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<List<P2PMessageModel>>(messages)
            });
        }

        public async Task<IActionResult> Create(P2PModel p2pModel, Guid applicationUserId)
        {
            var p2p = Mapper.Map<P2P>(p2pModel);
            p2p.CreatedById = applicationUserId;
            
            if(p2p.Deadline != null && p2p.Deadline < DateTime.Now.AddMinutes(1))
                throw new ErrorModelException(ErrorCodes.IncorrectValue, nameof(P2P.Deadline));
            
             if(p2p.InitialPrice < 0)
                throw new FormErrorModelException(nameof(P2PModel.InitialPrice), ErrorCodes.IncorrectValue);
            
            
            var blobIds = p2pModel.Blobs.Select(x => x.BlobId).ToList();
            var blobs = _context.Blobs.Where(x => blobIds.Contains(x.BlobId)).ToList();

            foreach (var blob in blobs)
            {
                if(blob.UploadedById != applicationUserId)
                    throw new ErrorModelException(ErrorCodes.OwnershipError, nameof(P2P.Deadline));
            
                if(blob.BlobType == "Image")
                    p2p.P2PImages.Add(new P2PImage
                    {
                        P2p = p2p,
                        ImageBlobId = blob.BlobId
                    });

                else if(blob.BlobType == "File")
                    p2p.P2PFiles.Add(new P2PFile
                    {
                        P2p = p2p,
                        FileBlobId = blob.BlobId
                    });
            }

            foreach (var language in p2pModel.Languages)
            {
                p2p.P2PLanguages.Add(new P2PLanguage
                {
                    P2p = p2p,
                    LanguageId = language.CoreLookupId
                });
            }
            
            _context.P2p.Add(p2p);
            await this.fillP2pBookmark(p2p, false);

            if (!await SaveChangesAsync())
            {
                var error = new ErrorModel(ErrorCodes.DatabaseError);
                return new BadRequestObjectResult(new ResponseModel(error));
            }

            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<P2PModel>(p2p)
            });
        }

        public async Task<IActionResult> Schedule(int p2pMessageId, Guid applicationUserId) //TODO: ReadyForSchedule(combine Accept and schedule)
        {
            var p2pMessage = await _context.P2PMessages.Where(x => x.P2pMessageId.Equals(p2pMessageId)).Include(x => x.P2p).FirstOrDefaultAsync();
            var p2p = p2pMessage?.P2p;

            if(p2pMessage == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(P2PMessage));

            var lastOfferInNegotiation = await _context.P2PMessages.Where(x => x.P2pId.Equals(p2p.P2pId))
                                                                   .Where(x => x.MessageFromId.Equals(p2pMessage.MessageFromId))
                                                                   .Where(x => x.MessageToId.Equals(p2pMessage.MessageToId))
                                                                   .OrderByDescending(x => x.Timestamp)
                                                                   .FirstOrDefaultAsync();

            if(!lastOfferInNegotiation.P2pMessageId.Equals(p2pMessage.P2pMessageId)) //Is this the last offer offered
                throw new ErrorModelException(ErrorCodes.NotLastOffer, p2pMessage.P2pMessageId.ToString());

            if(!p2p.CreatedById.Equals(applicationUserId)) //Only creator can schedule
                throw new ErrorModelException(ErrorCodes.HackAttempt);

            if(p2pMessage.MessageFromId.Equals(applicationUserId)) //Cant schedule p2p if you sent offer yourself
                throw new ErrorModelException(ErrorCodes.HackAttempt);

            if(p2p.IsDeleted)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(P2P));
            
            if(p2p.ScheduledWithId.HasValue)
                throw new ErrorModelException(ErrorCodes.AlreadyScheduled, nameof(P2P));

            if(p2pMessage.PriceOffer < 0)
               throw new ErrorModelException(ErrorCodes.IncorrectValue, nameof(P2PMessageModel.PriceOffer));

            var payment = await _transactionServices.PayWithMinutes(applicationUserId, p2pMessage.PriceOffer, TransactionReasons.ScheduledP2P);
            if(!payment)
                throw new ErrorModelException(ErrorCodes.NotEnoughMinutes);

            p2p.PriceAgreed = p2pMessage.PriceOffer;
            p2p.DateTimeAgreed = p2pMessage.DateTimeOffer;
            p2p.ScheduledWithId = p2pMessage.MessageFromId == applicationUserId ? p2pMessage.MessageToId : p2pMessage.MessageFromId;
            p2p.Status = P2PStatus.Scheduled;
            
            _context.P2p.Update(p2p);
            
            if (!await SaveChangesAsync())
            {
                var error = new ErrorModel(ErrorCodes.DatabaseError);
                return new BadRequestObjectResult(new ResponseModel(error));
            }

            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<P2PModel>(p2p)
            });
        }
        
        public async Task<IActionResult> Message(P2PMessageModel p2pMessageModel, Guid applicationUserId)
        {
            var p2p = await _context.P2p.Where(x => x.P2pId == p2pMessageModel.P2pId).FirstOrDefaultAsync();
            var negotiationWithApplicationUserId = (applicationUserId == p2p.CreatedById) ? p2pMessageModel.MessageToId : applicationUserId;

            if(p2p == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(P2P));

            if(p2p.IsDeleted)
                throw new ErrorModelException(ErrorCodes.DiscussionClosed, nameof(P2P));

            if(p2p.DateTimeAgreed.HasValue)
                throw new ErrorModelException(ErrorCodes.DiscussionClosed, nameof(P2P));

            if(p2pMessageModel.MessageToId == applicationUserId)
                throw new ErrorModelException(ErrorCodes.AuthorityError, nameof(P2PMessage));

            if(p2p.CreatedById != applicationUserId && p2pMessageModel.MessageToId != p2p.CreatedById)
                throw new ErrorModelException(ErrorCodes.HackAttempt); // outsiders can only message creator of p2p
            
            var pastFewOffers = await _context.P2PMessages.Where(x => x.P2pId == p2p.P2pId && (x.MessageFromId == negotiationWithApplicationUserId || x.MessageToId == negotiationWithApplicationUserId)).OrderBy(x => x.Timestamp).Take(ConsecutiveP2PMessageLimit).ToListAsync();

            if(p2p.CreatedById == applicationUserId) // check if creator of p2p is messaging someone who already messaged him 
                if(pastFewOffers.Count() == 0)
                    throw new ErrorModelException(ErrorCodes.HackAttempt, nameof(P2PMessage));

            if (pastFewOffers.Count == ConsecutiveP2PMessageLimit)
                if (pastFewOffers.Where(offer => offer.MessageFromId == applicationUserId).Count() == ConsecutiveP2PMessageLimit)
                    throw new ErrorModelException(ErrorCodes.ConsecutiveOffersLimit, ConsecutiveP2PMessageLimit.ToString());
            
            if(pastFewOffers.Count == 0)
            {
                p2p.OfferCount++;
                _context.P2p.Update(p2p);
            }

            var p2pMessage = Mapper.Map<P2PMessage>(p2pMessageModel);
            p2pMessage.MessageFromId = applicationUserId;

            _context.P2PMessages.Add(p2pMessage);

            if (!await SaveChangesAsync())
            {
                var error = new ErrorModel(ErrorCodes.DatabaseError);
                return new BadRequestObjectResult(new ResponseModel(error));
            }
            var notification = new Notification(p2pMessage.MessageToId, NotificationTypes.NewP2PComment, DateTime.UtcNow, applicationUserId, null, p2pMessage);
            await _notificationServices.SendNotification(notification);

            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<P2PMessageModel>(p2pMessage)
            });
        }

        public async Task<IActionResult> AcceptOffer(int p2pMessageId, Guid applicationUserId)
        {
            var offerToAccept = await _context.P2PMessages.Where(x => x.P2pMessageId == p2pMessageId).FirstOrDefaultAsync();
            var p2p = await _context.P2p.Where(x => x.P2pId == offerToAccept.P2pId).FirstOrDefaultAsync();
            var negotiationWithApplicationUserId = (applicationUserId == p2p.CreatedById) ? offerToAccept.MessageToId : applicationUserId;

            if(p2p == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(P2P));

            if(p2p.IsDeleted)
                throw new ErrorModelException(ErrorCodes.DiscussionClosed, nameof(P2P));

            if(p2p.DateTimeAgreed.HasValue)
                throw new ErrorModelException(ErrorCodes.DiscussionClosed, nameof(P2P));

            if(!offerToAccept.MessageFromId.Equals(p2p.CreatedById)) //Can only accept offers from creator
                throw new ErrorModelException(ErrorCodes.HackAttempt, nameof(P2PMessage));

            if(!offerToAccept.MessageToId.Equals(applicationUserId)) //important??
                throw new ErrorModelException(ErrorCodes.HackAttempt, nameof(P2PMessage));
            
            var lastOfferInNegotiation = await _context.P2PMessages.Where(x => x.P2pId == p2p.P2pId && (x.MessageFromId == negotiationWithApplicationUserId || x.MessageToId == negotiationWithApplicationUserId)).OrderBy(x => x.Timestamp).LastOrDefaultAsync();
            if(lastOfferInNegotiation.P2pMessageId != offerToAccept.P2pMessageId) // Is this the last offer in negotiation
                throw new ErrorModelException(ErrorCodes.NotLastOffer, nameof(P2PModel));

            var newP2pMessage = new P2PMessage();
            newP2pMessage.OfferAcceptedId = offerToAccept.P2pMessageId;
            newP2pMessage.MessageFromId = applicationUserId;
            newP2pMessage.MessageToId = offerToAccept.MessageFromId;
            newP2pMessage.PriceOffer = offerToAccept.PriceOffer;
            newP2pMessage.DateTimeOffer = offerToAccept.DateTimeOffer;
            newP2pMessage.P2pId = offerToAccept.P2pId;
            newP2pMessage.Text = "I ACCEPT";

            _context.P2PMessages.Add(newP2pMessage);

            if (!await SaveChangesAsync())
            {
                var error = new ErrorModel(ErrorCodes.DatabaseError);
                return new BadRequestObjectResult(new ResponseModel(error));
            }
            var notification = new Notification(newP2pMessage.MessageToId, NotificationTypes.P2POfferAccepted, DateTime.UtcNow, applicationUserId, null, newP2pMessage);
            await _notificationServices.SendNotification(notification);

            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<P2PMessageModel>(newP2pMessage)
            });
        }

        public async Task<bool> AddBookmark(int p2pId, Guid applicationUserId)
        {
            var p2p = await _context.P2p.Where(x => x.P2pId.Equals(p2pId)).FirstOrDefaultAsync();
            var p2pBookmarks = await _context.P2PBookmarks.Where(x => x.P2pId.Equals(p2pId)).ToListAsync();

            if(p2p.CreatedById.Equals(applicationUserId))
                throw new ErrorModelException(ErrorCodes.AuthorityError, nameof(P2P));

            if(p2pBookmarks.Where(x => applicationUserId.Equals(applicationUserId)).Count() == 1)
                throw new ErrorModelException(ErrorCodes.AlreadyBookmarked);

            var p2pbookmark = new P2PBookmark{
                P2pId = p2pId,
                ApplicationUserId = applicationUserId
            };

            p2p.BookmarkCount = p2pBookmarks.Count + 1;

            _context.P2PBookmarks.Add(p2pbookmark);
            _context.P2p.Update(p2p);

            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveBookmark(int p2pId, Guid applicationUserId)
        {
            var p2pBookmark = await _context.P2PBookmarks.Where(x => x.P2pId.Equals(p2pId))
                                                          .Where(x => x.ApplicationUserId.Equals(applicationUserId))
                                                          .FirstOrDefaultAsync();
            if(p2pBookmark == null)
                throw new ErrorModelException(ErrorCodes.WasntBookmarked);

            var p2p = await _context.P2p.Where(x => x.P2pId.Equals(p2pId)).FirstOrDefaultAsync();
            // TODO: FIX THIS COUNT
            p2p.BookmarkCount = p2p.BookmarkCount - 1;

            _context.P2p.Update(p2p);
            _context.P2PBookmarks.Remove(p2pBookmark);
            await SaveChangesAsync();
            
            return true;
        }

        public async Task<IActionResult> Delete(int p2pInt, Guid applicationUserId)
        {
            var p2p = await _context.P2p.Where(x => x.P2pId == p2pInt).FirstOrDefaultAsync();

            if (p2p == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(P2P));

            if(p2p.CreatedById != applicationUserId)
                return new BadRequestObjectResult(new ResponseModel(new ErrorModel(ErrorCodes.OwnershipError)));

            if(p2p.IsDeleted)
                return new BadRequestObjectResult(new ResponseModel(new ErrorModel(ErrorCodes.AlreadyDeleted, nameof(P2P))));

            p2p.IsDeleted = true;
            if (!await SaveChangesAsync())
            {
                var error = new ErrorModel(ErrorCodes.DatabaseError);
                return new BadRequestObjectResult(new ResponseModel(error));
            }

            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<P2PModel>(p2p)
            });
                
        }

        public async Task UpdateAndSave(P2P p2p)
        {
            _context.P2p.Update(p2p);
            await SaveChangesAsync();
        }

        private async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<IActionResult> ListMine(Guid applicationUserId)
        {
            var p2ps = await _context.P2p.IncludeEverything().Where(x => x.CreatedById == applicationUserId).ToListAsync();

            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<List<P2PModel>>(p2ps)
            });
        }

        public async Task<IActionResult> ListSchedulded(Guid applicationUserId)
        {
            var p2ps = await _context.P2p
                    .IncludeEverything()
                    .Where(x => x.Status == P2PStatus.Scheduled)
                    .Where(x => x.CreatedById == applicationUserId || x.ScheduledWithId == applicationUserId)
                    .ToListAsync();

            return new OkObjectResult(new ResponseModel {
                Object = Mapper.Map<List<P2PModel>>(p2ps)
            });
        }

        public async Task<List<P2P>> GetRecommendedP2P(Guid applicationUserId, int offset, int numItems)
        {
            var p2ps = await _context.P2p
                            .IncludeEverything()
                            .Where(x => !x.CreatedById.Equals(applicationUserId))
                            .Where(x => x.Status == P2PStatus.Active)
                            .Where(x => x.IsDeleted == false)
                            .OrderByDescending(x => x.DateCreated)
                            .Skip(offset).Take(numItems)
                            .ToListAsync();

            return p2ps;
        }

        public async Task<List<P2P>> GetByFos(int fosId, Guid applicationUserId)
        {
            var p2ps = await _context.P2p
                        .IncludeEverything()
                        .Where(x => x.FosId.Equals(fosId))
                        .Where(x => x.Status == P2PStatus.Active)
                        .OrderByDescending(x => x.DateCreated)
                        .ToListAsync();

            return p2ps;
        }

        public async Task<IActionResult> ListBookmarked(Guid applicationUserId)
        {
            var p2pBookmarks = await _context.P2PBookmarks.Where(x => x.ApplicationUserId.Equals(applicationUserId))
                                                            .Include("P2p")
                                                            .Include("P2p.P2PLanguages")
                                                            .Include("P2p.P2PFiles")
                                                            .Include("P2p.P2PImages")
                                                            .ToListAsync();

            var p2ps = new List<P2P>();
            foreach (var p2pBookmark in p2pBookmarks)
            {
                p2ps.Add(p2pBookmark.P2p);
            }


            return new OkObjectResult(new ResponseModel {
                Object = Mapper.Map<List<P2PModel>>(p2ps)
            });
        }

        public async  Task<IActionResult> ListDeleted(Guid applicationUserId)
        {
            var p2ps = await _context.P2p
                                    .IncludeEverything()
                                    .Where(x => x.CreatedById == applicationUserId)
                                    .Where(x => x.IsDeleted == true)
                                    .ToListAsync();

            return new OkObjectResult(new ResponseModel {
                Object = Mapper.Map<List<P2PModel>>(p2ps)
            });
        }

        public Task<IActionResult> ListActionRequired(Guid applicationUserId)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> ListAll(Guid applicationUserId)
        {
            if(!_environment.IsDevelopment())
                return new BadRequestObjectResult(new ResponseModel(new ErrorModel("Development only endpoint")));
            
            var p2ps = await _context.P2p.IncludeEverything().ToListAsync();
            var myBookmarks = await _context.P2PBookmarks.Where(x => x.ApplicationUserId.Equals(applicationUserId)).ToListAsync();

            foreach (var p2p in p2ps)
            {
                await fillP2pBookmark(p2p, applicationUserId);
            }

            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<List<P2PModel>>(p2ps)
            });
        }
    }
}