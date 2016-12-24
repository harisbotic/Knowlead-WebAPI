using System;
using System.Linq;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using static Knowlead.Common.Constants;
using Knowlead.DAL;
using Knowlead.DomainModel.UserModels;
using Microsoft.EntityFrameworkCore;
using Knowlead.BLL.Exceptions;
using System.Collections.Generic;

namespace Knowlead.BLL.Repositories
{
    public class UserRelationshipRepository : IUserRelationshipRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRelationshipRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationUserRelationship>> GetAllFriends(Guid applicationUserId)
        {
            return await _context.ApplicationUserRelationships
                                        .Where(x => x.ApplicationUserBiggerId == applicationUserId || x.ApplicationUserSmallerId == applicationUserId)
                                        .ToListAsync();
        }

        public async Task<ApplicationUserRelationship> SendFriendRequest(Guid currentUserId, Guid otherUserId)
        {
            var relationship = await GetApplicationUserRelationship(currentUserId, otherUserId);

            if(relationship != null)
            {
                switch (relationship.Status) 
                {
                    case(ApplicationUserRelationship.UserRelationshipStatus.Accepted): //TODO: StatusEnums
                        throw new ErrorModelException(ErrorCodes.AlreadyFriends, otherUserId.ToString());
                    
                    case(ApplicationUserRelationship.UserRelationshipStatus.Pending):
                        if(relationship.LastActionById == currentUserId)
                            return relationship;
                        throw new ErrorModelException(ErrorCodes.SthWentWrong);
                    
                    case(ApplicationUserRelationship.UserRelationshipStatus.Declined):
                        if(relationship.LastActionById != currentUserId)
                            throw new ErrorModelException(ErrorCodes.HackAttempt);
                        break;

                    case(ApplicationUserRelationship.UserRelationshipStatus.Blocked):
                        if(relationship.LastActionById == currentUserId)
                            throw new ErrorModelException(ErrorCodes.UserBlocked, otherUserId.ToString());
                        throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(ApplicationUser));
                }
            }

            if(relationship == null)
            {
                relationship = new ApplicationUserRelationship(currentUserId, otherUserId, ApplicationUserRelationship.UserRelationshipStatus.Pending);
                
                _context.ApplicationUserRelationships.Add(relationship);
            }

            await SaveChangesAsync();
            return relationship;
        }

        public async Task<ApplicationUserRelationship> RespondToFriendRequest(Guid currentUserId, Guid otherUserId, bool isAccepting)
        {
            var relationship = await GetApplicationUserRelationship(currentUserId, otherUserId);

            if(relationship == null)
                throw new ErrorModelException(ErrorCodes.RequestNotFound);

            switch (relationship.Status) 
            {
                case(ApplicationUserRelationship.UserRelationshipStatus.Accepted):
                    throw new ErrorModelException(ErrorCodes.AlreadyFriends, otherUserId.ToString());
                
                case(ApplicationUserRelationship.UserRelationshipStatus.Pending):
                    if(relationship.LastActionById == currentUserId)
                        throw new ErrorModelException(ErrorCodes.HackAttempt);
                    break;
                
                case(ApplicationUserRelationship.UserRelationshipStatus.Declined):
                    throw new ErrorModelException(ErrorCodes.RequestNotFound);

                case(ApplicationUserRelationship.UserRelationshipStatus.Blocked):
                    if(relationship.LastActionById == currentUserId)
                        throw new ErrorModelException(ErrorCodes.UserBlocked, otherUserId.ToString());
                    throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(ApplicationUser));
            }

            if(relationship.Status == ApplicationUserRelationship.UserRelationshipStatus.Pending)
                if(relationship.LastActionById == otherUserId)
                {
                    if(isAccepting)
                        ChangeFriendshipStatusTo(relationship,currentUserId, ApplicationUserRelationship.UserRelationshipStatus.Accepted);
                    else
                        ChangeFriendshipStatusTo(relationship,currentUserId, ApplicationUserRelationship.UserRelationshipStatus.Declined);
                }

            await SaveChangesAsync();
            return relationship;
        }

        public async Task<ApplicationUserRelationship> CancelFriendRequest(Guid currentUserId, Guid otherUserId)
        {
            var relationship = await GetApplicationUserRelationship(currentUserId, otherUserId);

            if(relationship == null)
                return relationship;

            switch (relationship.Status) 
            {
                case(ApplicationUserRelationship.UserRelationshipStatus.Accepted):
                    throw new ErrorModelException(ErrorCodes.AlreadyFriends, otherUserId.ToString());
                
                case(ApplicationUserRelationship.UserRelationshipStatus.Pending):
                    if(relationship.LastActionById != currentUserId)
                        throw new ErrorModelException(ErrorCodes.SthWentWrong);
                    break;
                
                case(ApplicationUserRelationship.UserRelationshipStatus.Declined):
                    throw new ErrorModelException(ErrorCodes.RequestNotFound);

                case(ApplicationUserRelationship.UserRelationshipStatus.Blocked):
                    if(relationship.LastActionById == currentUserId)
                        throw new ErrorModelException(ErrorCodes.UserBlocked, otherUserId.ToString());
                    throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(ApplicationUser));
            }

            if(relationship.Status == ApplicationUserRelationship.UserRelationshipStatus.Pending)
                if(relationship.LastActionById == currentUserId)
                {
                    _context.ApplicationUserRelationships.Remove(relationship);
                    relationship = null;
                }

            await SaveChangesAsync();
            return relationship;
        }

        public async Task<ApplicationUserRelationship> RemoveFriend(Guid currentUserId, Guid otherUserId)
        {
            var relationship = await GetApplicationUserRelationship(currentUserId, otherUserId);

            if(relationship == null)
                return relationship;
            
            if(relationship.Status != ApplicationUserRelationship.UserRelationshipStatus.Accepted)
                throw new ErrorModelException(ErrorCodes.NotInFriendlist, otherUserId.ToString());

            if(relationship.Status == ApplicationUserRelationship.UserRelationshipStatus.Blocked)
            {
                if(relationship.LastActionById == currentUserId)
                    throw new ErrorModelException(ErrorCodes.UserBlocked, otherUserId.ToString());
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(ApplicationUser));
            }

            _context.ApplicationUserRelationships.Remove(relationship);
            relationship = null;

            await SaveChangesAsync();
            return relationship;
        }

        public async Task<ApplicationUserRelationship> BlockUser(Guid currentUserId, Guid otherUserId)
        {
            var relationship = await GetApplicationUserRelationship(currentUserId, otherUserId);

            if(relationship != null)
            {
                if(relationship.Status == ApplicationUserRelationship.UserRelationshipStatus.Blocked)
                {
                    if(relationship.LastActionById != currentUserId)
                        throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(ApplicationUser));
                    return relationship;
                }

                ChangeFriendshipStatusTo(relationship,currentUserId, ApplicationUserRelationship.UserRelationshipStatus.Blocked);
            }

            if(relationship == null)
            {
                relationship = new ApplicationUserRelationship(currentUserId, otherUserId, ApplicationUserRelationship.UserRelationshipStatus.Blocked);
                _context.ApplicationUserRelationships.Add(relationship);
            }

            await SaveChangesAsync();
            return relationship;
        }

        public async Task<ApplicationUserRelationship> UnblockUser(Guid currentUserId, Guid otherUserId)
        {
            var relationship = await GetApplicationUserRelationship(currentUserId, otherUserId);

            if(relationship == null)    
                return relationship;
            
            if (relationship.Status == ApplicationUserRelationship.UserRelationshipStatus.Blocked)
                if(relationship.LastActionById != currentUserId)
                    throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(ApplicationUser));

            _context.ApplicationUserRelationships.Remove(relationship);
            relationship = null;

            await SaveChangesAsync();
            return relationship;
        }

        private async Task<ApplicationUserRelationship> GetApplicationUserRelationship(Guid userIdOne, Guid userIdTwo)
        {
            var bsTuple = GetBiggerSmallerGuidTuple(userIdOne, userIdTwo);

            return await _context.ApplicationUserRelationships
                                        .Where(x => x.ApplicationUserBiggerId == bsTuple.Item1 && x.ApplicationUserSmallerId == bsTuple.Item2)
                                        .FirstOrDefaultAsync();
        }

        private Tuple<Guid, Guid> GetBiggerSmallerGuidTuple(Guid guidOne, Guid guidTwo)
        { 
            //TODO: put this in utils and use for UserFriendship constructor
            if(guidOne.Equals(guidTwo))
                throw new ErrorModelException(ErrorCodes.HackAttempt);

            var biggerGuid = (guidOne.CompareTo(guidTwo) > 0)? guidOne : guidTwo;
            var smallerGuid = (guidOne.CompareTo(guidTwo) < 0)? guidOne : guidTwo;

            return new Tuple<Guid,Guid> (biggerGuid, smallerGuid);
        }

        private void ChangeFriendshipStatusTo(ApplicationUserRelationship relationship, Guid currentUserId, ApplicationUserRelationship.UserRelationshipStatus newStatus)
        {
            relationship.Status = newStatus;
            relationship.LastActionById = currentUserId;
        }

        private async Task SaveChangesAsync()
        {
            var success = await _context.SaveChangesAsync() > 0;
            if(!success)
                throw new ErrorModelException(ErrorCodes.DatabaseError); //No changed were made to entity
        }

    }
}