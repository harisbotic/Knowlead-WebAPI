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
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.P2PModels;
using Knowlead.DTO.ResponseModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Knowlead.Common.Exceptions;
using Knowlead.Services.Interfaces;
using Knowlead.DomainModel.NotificationModels;

namespace Knowlead.BLL.Repositories
{
    public class P2PRepository : IP2PRepository
    {
        public static int ConsecutiveP2PMessageLimit = 2;
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly INotificationServices _notificationServices;

        public P2PRepository(ApplicationDbContext context,
                             IHostingEnvironment environment,
                             INotificationServices notificationServices)
        {
            _context = context;
            _environment = environment;
            _notificationServices = notificationServices;
        }

        public async Task<P2P> GetP2PTemp(int p2pId)
        {
            return await _context.P2p.IncludeEverything().Where(x => x.P2pId == p2pId).FirstOrDefaultAsync();
        }

        public async Task<IActionResult> GetP2P(int p2pId)
        {
            var p2p = await _context.P2p.IncludeEverything().Where(x => x.P2pId == p2pId).FirstOrDefaultAsync();
            
            if(p2p == null && !p2p.IsDeleted)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(P2P));

            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<P2PModel>(p2p)
            });
        }

        public async Task<IActionResult> GetMessages(int p2pId, ApplicationUser applicationUser)
        {
            var userId = applicationUser.Id;
            var p2p = await _context.P2p.Where(x => x.P2pId == p2pId).FirstOrDefaultAsync();

            if(p2p == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(P2P));

            IQueryable<P2PMessage> messagesQuery = _context.P2PMessages
                                                           .Where(x => x.P2pId == p2pId)
                                                           .Where(x => x.MessageToId == userId || x.MessageFromId == userId)
                                                           .Include(x => x.MessageFrom)
                                                           .Include(x => x.MessageTo); //TODO: Probably optimization is required

            if(p2p.CreatedById != applicationUser.Id)
                messagesQuery = messagesQuery
                                    .Where(x => x.MessageFromId == applicationUser.Id || x.MessageToId == applicationUser.Id);
                
            var messages = await messagesQuery.ToListAsync();

            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<List<P2PMessageModel>>(messages)
            });
        }

        public async Task<IActionResult> Create(P2PModel p2pModel, ApplicationUser applicationUser)
        {
            var p2p = Mapper.Map<P2P>(p2pModel);
            p2p.CreatedById = applicationUser.Id;
            
            if(p2p.Deadline != null && p2p.Deadline < DateTime.Now.AddMinutes(1))
                throw new ErrorModelException(ErrorCodes.IncorrectValue, nameof(P2P.Deadline));
            
             if(p2p.InitialPrice < 0)
                throw new FormErrorModelException(nameof(P2PModel.InitialPrice), ErrorCodes.IncorrectValue);
            
            
            var blobIds = p2pModel.Blobs.Select(x => x.BlobId).ToList();
            var blobs = _context.Blobs.Where(x => blobIds.Contains(x.BlobId)).ToList();

            foreach (var blob in blobs)
            {
                if(blob.UploadedById != applicationUser.Id)
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

            if (!await SaveChangesAsync())
            {
                var error = new ErrorModel(ErrorCodes.DatabaseError);
                return new BadRequestObjectResult(new ResponseModel(error));
            }

            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<P2PModel>(p2p)
            });
        }

        public async Task<IActionResult> Schedule(int p2pMessageId, Guid applicationUserId)
        {
            var p2pMessage = await _context.P2PMessages.Where(x => x.P2pMessageId.Equals(p2pMessageId)).Include(x => x.P2p).FirstOrDefaultAsync();
            var p2p = p2pMessage?.P2p;

            if(p2pMessage == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(P2PMessage));

            var lastOfferInNegotiation = await _context.P2PMessages.Where(x => x.P2pId.Equals(p2p.P2pId))
                                                                   .Where(x => x.MessageFromId.Equals(p2pMessage.MessageFromId))
                                                                   .Where(x => x.MessageToId.Equals(p2pMessage.MessageToId))
                                                                   .LastOrDefaultAsync();

            if(lastOfferInNegotiation.P2pMessageId.Equals(p2pMessage.P2pMessageId)) //Is this the last offer offered
                throw new ErrorModelException(ErrorCodes.NotLastOffer, p2pMessage.P2pMessageId.ToString());

            if(p2pMessage.MessageToId.Equals(applicationUserId)) //Can this user accept the offer
                throw new ErrorModelException(ErrorCodes.HackAttempt);

            if(p2p.IsDeleted)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(P2P));
            
            if(p2p.ScheduledWithId.HasValue)
                throw new ErrorModelException(ErrorCodes.AlreadyScheduled, nameof(P2P));

            if(p2pMessage.PriceOffer < 0)
               throw new ErrorModelException(ErrorCodes.IncorrectValue, nameof(P2PMessageModel.PriceOffer));

            p2p.PriceAgreed = p2pMessage.PriceOffer;
            p2p.DateTimeAgreed = p2pMessage.DateTimeOffer;

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
        
        public async Task<IActionResult> Message(P2PMessageModel p2pMessageModel, ApplicationUser applicationUser)
        {
            var p2p = await _context.P2p.Where(x => x.P2pId == p2pMessageModel.P2pId).FirstOrDefaultAsync();
            var threadWithApplicationUserId = (applicationUser.Id == p2p.CreatedById) ? p2pMessageModel.MessageToId : applicationUser.Id;

            if(p2p == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(P2P));

            if(p2p.IsDeleted)
                throw new ErrorModelException(ErrorCodes.DiscussionClosed, nameof(P2P));

            if(p2p.DateTimeAgreed.HasValue)
                throw new ErrorModelException(ErrorCodes.DiscussionClosed, nameof(P2P));

            if(p2pMessageModel.MessageToId == applicationUser.Id)
                throw new ErrorModelException(ErrorCodes.AuthorityError, nameof(P2PMessage));

            if(p2p.CreatedById != applicationUser.Id && p2pMessageModel.MessageToId != p2p.CreatedById)
                throw new ErrorModelException(ErrorCodes.HackAttempt); // outsiders can only message creator of p2p
            
            var pastFewOffers = await _context.P2PMessages.Where(x => x.P2pId == p2p.P2pId && (x.MessageFromId == threadWithApplicationUserId || x.MessageToId == threadWithApplicationUserId)).OrderByDescending(x => x.Timestamp).Take(ConsecutiveP2PMessageLimit).ToListAsync();

            if(p2p.CreatedById == applicationUser.Id) // check if creator of p2p is messaging someone who already messaged him 
                if(pastFewOffers.Count() == 0)
                    throw new ErrorModelException(ErrorCodes.HackAttempt, nameof(P2PMessage));

            if (pastFewOffers.Count == ConsecutiveP2PMessageLimit)
                if (pastFewOffers.Where(offer => offer.MessageFromId == applicationUser.Id).Count() == ConsecutiveP2PMessageLimit)
                    throw new ErrorModelException(ErrorCodes.ConsecutiveOffersLimit, ConsecutiveP2PMessageLimit.ToString());
            
            var p2pMessage = Mapper.Map<P2PMessage>(p2pMessageModel);
            p2pMessage.MessageFromId = applicationUser.Id;

            _context.P2PMessages.Add(p2pMessage);

            if (!await SaveChangesAsync())
            {
                var error = new ErrorModel(ErrorCodes.DatabaseError);
                return new BadRequestObjectResult(new ResponseModel(error));
            }
            var notification = new Notification(p2pMessage.MessageToId, NotificationTypes.NewP2PComment, DateTime.UtcNow, applicationUser.Id, null, p2pMessage);
            await _notificationServices.SendNotification(notification);

            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<P2PMessageModel>(p2pMessage)
            });
        }

        public async Task<IActionResult> Delete(int p2pInt, ApplicationUser applicationUser)
        {
            var p2p = await _context.P2p.Where(x => x.P2pId == p2pInt).FirstOrDefaultAsync();

            if (p2p == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(P2P));

            if(p2p.CreatedById != applicationUser.Id)
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

        private async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<IActionResult> ListMine(ApplicationUser applicationUser)
        {
            var p2ps = await _context.P2p.IncludeEverything().Where(x => x.CreatedById == applicationUser.Id).ToListAsync();
            // var p2pModels = new List<P2PModel>();
            // var applicationUserModel = Mapper.Map<ApplicationUserModel>(applicationUser);

            // //Optimization
            // foreach (var p2p in p2ps)
            // {
            //     var p2pModel = Mapper.Map<P2PModel>(p2p);
            //     p2pModel.CreatedBy = applicationUserModel;
            //     p2pModels.Add(p2pModel);
            // }

            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<List<P2PModel>>(p2ps)
            });
        }

        public async Task<IActionResult> ListSchedulded(ApplicationUser applicationUser)
        {
            var p2ps = await _context.P2p
                    .IncludeEverything()
                    .Where(x => x.DateTimeAgreed.HasValue)
                    .Where(x => x.CreatedById == applicationUser.Id || x.ScheduledWithId == applicationUser.Id)
                    .ToListAsync();

            return new OkObjectResult(new ResponseModel {
                Object = Mapper.Map<List<P2PModel>>(p2ps)
            });
        }

        public Task<IActionResult> ListBookmarked(ApplicationUser applicationUser)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> ListDeleted(ApplicationUser applicationUser)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> ListActionRequired(ApplicationUser applicationUser)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> ListAll()
        {
            if(!_environment.IsDevelopment())
                return new BadRequestObjectResult(new ResponseModel(new ErrorModel("Development only endpoint")));

            var p2ps = await _context.P2p.IncludeEverything().ToListAsync();

            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<List<P2PModel>>(p2ps)
            });
        }
    }
}