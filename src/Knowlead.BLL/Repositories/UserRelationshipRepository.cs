using System;
using System.Linq;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using static Knowlead.Common.Constants;
using Knowlead.DAL;
using Knowlead.DomainModel.UserModels;
using Microsoft.EntityFrameworkCore;
using Knowlead.BLL.Exceptions;

namespace Knowlead.BLL.Repositories
{
    public class UserRelationshipRepository : IUserRelationshipRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRelationshipRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SendFriendRequest(Guid currentUserId, Guid otherUserId)
        {
            if(currentUserId.Equals(otherUserId))
                throw new ErrorModelException(ErrorCodes.HackAttempt);

            var smallerUserId = (currentUserId.CompareTo(otherUserId) < 0)? currentUserId : otherUserId;
            var biggerUserId = (currentUserId.CompareTo(otherUserId) > 0)? currentUserId : otherUserId;

            var relationship = await _context.ApplicationUserRelationships
                                        .Where(x => x.ApplicationUserBiggerId == smallerUserId && x.ApplicationUserSmallerId == biggerUserId)
                                        .FirstOrDefaultAsync();

            if(relationship != null)
            {
                switch (relationship.Status) 
                {
                    case(ApplicationUserRelationship.UserRelationshipStatus.Accepted): //TODO: StatusEnums
                        throw new ErrorModelException(ErrorCodes.AlreadyFriend);
                    
                    case(ApplicationUserRelationship.UserRelationshipStatus.Pending):
                        throw new ErrorModelException(ErrorCodes.HackAttempt);

                    case(ApplicationUserRelationship.UserRelationshipStatus.Declined):
                        if(relationship.LastActionById != currentUserId)
                            throw new ErrorModelException(ErrorCodes.HackAttempt);
                        break;

                    case(ApplicationUserRelationship.UserRelationshipStatus.Blocked):
                        if(relationship.LastActionById == currentUserId)
                            throw new ErrorModelException(ErrorCodes.UserBlocked, otherUserId.ToString());
                        throw new ErrorModelException(ErrorCodes.HackAttempt);
                }
            }

            if(relationship == null)
            {
                relationship = new ApplicationUserRelationship();
                relationship.ApplicationUserSmallerId = smallerUserId;
                relationship.ApplicationUserBiggerId = biggerUserId;
                
                _context.ApplicationUserRelationships.Add(relationship);
            }

            relationship.Status = ApplicationUserRelationship.UserRelationshipStatus.Pending;
            relationship.LastActionById = currentUserId;

            return await SaveChangesAsync();
        }

        private async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}