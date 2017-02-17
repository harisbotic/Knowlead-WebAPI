using System;
using System.Linq;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using static Knowlead.Common.Constants;
using Knowlead.DAL;
using Knowlead.DomainModel.UserModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static Knowlead.Common.Constants.EnumStatuses;
using static Knowlead.Common.Utils;
using Knowlead.DomainModel.ChatModels;
using Knowlead.Common.Exceptions;

namespace Knowlead.BLL.Repositories
{
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly ApplicationDbContext _context;

        public FriendshipRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Friendship> GetFriendship(Guid userIdOne, Guid userIdTwo)
        {
            var bsTuple = GetBiggerSmallerGuidTuple(userIdOne, userIdTwo);

            return await _context.Friendships
                                        .Where(x => x.ApplicationUserBiggerId == bsTuple.Item1 && x.ApplicationUserSmallerId == bsTuple.Item2)
                                        .FirstOrDefaultAsync();
        }

        public async Task<List<Friendship>> GetAllFriends(Guid applicationUserId, FriendshipStatus? status)
        {
            var query = _context.Friendships
                        .Where(x => x.ApplicationUserBiggerId == applicationUserId || x.ApplicationUserSmallerId == applicationUserId);
            
            if(status == null)
                query = _context.Friendships.Where(x => x.Status != FriendshipStatus.Blocked || x.LastActionById == applicationUserId);

            if(status != null)
                query = _context.Friendships.Where(x => x.Status == status);

            if(status == FriendshipStatus.Blocked)
                query = _context.Friendships.Where(x => x.LastActionById == applicationUserId);
 
            return await query.ToListAsync();
        }

        public async Task<Friendship> SendFriendRequest(Guid currentUserId, Guid otherUserId)
        {
            if(currentUserId.Equals(otherUserId))
                throw new ErrorModelException(ErrorCodes.IncorrectValue, nameof(otherUserId));

            var friendship = await GetFriendship(currentUserId, otherUserId);

            if(friendship != null)
            {
                switch (friendship.Status) 
                {
                    case(FriendshipStatus.Accepted):
                        throw new ErrorModelException(ErrorCodes.AlreadyFriends, otherUserId.ToString());
                    
                    case(FriendshipStatus.Pending):
                        if(friendship.LastActionById == currentUserId)
                            return friendship;
                        throw new ErrorModelException(ErrorCodes.SthWentWrong);
                    
                    case(FriendshipStatus.Declined):
                        if(friendship.LastActionById != currentUserId)
                            throw new ErrorModelException(ErrorCodes.HackAttempt);
                        break;

                    case(FriendshipStatus.Blocked):
                        if(friendship.LastActionById == currentUserId)
                            throw new ErrorModelException(ErrorCodes.UserBlocked, otherUserId.ToString());
                        throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(ApplicationUser));
                }
            }

            if(friendship == null)
            {
                friendship = new Friendship(currentUserId, otherUserId, FriendshipStatus.Pending);
                
                _context.Friendships.Add(friendship);
            }

            await SaveChangesAsync();
            return friendship;
        }

        public async Task<Friendship> RespondToFriendRequest(Guid currentUserId, Guid otherUserId, bool isAccepting)
        {
            var friendship = await GetFriendship(currentUserId, otherUserId);

            if(friendship == null)
                throw new ErrorModelException(ErrorCodes.RequestNotFound);

            switch (friendship.Status) 
            {
                case(FriendshipStatus.Accepted):
                    throw new ErrorModelException(ErrorCodes.AlreadyFriends, otherUserId.ToString());
                
                case(FriendshipStatus.Pending):
                    if(friendship.LastActionById == currentUserId)
                        throw new ErrorModelException(ErrorCodes.HackAttempt);
                    break;
                
                case(FriendshipStatus.Declined):
                    throw new ErrorModelException(ErrorCodes.RequestNotFound);

                case(FriendshipStatus.Blocked):
                    if(friendship.LastActionById == currentUserId)
                        throw new ErrorModelException(ErrorCodes.UserBlocked, otherUserId.ToString());
                    throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(ApplicationUser));
            }

            if(friendship.Status == FriendshipStatus.Pending)
                if(friendship.LastActionById == otherUserId)
                {
                    if(isAccepting)
                        ChangeFriendshipStatusTo(friendship, currentUserId, FriendshipStatus.Accepted);
                    else
                        ChangeFriendshipStatusTo(friendship, currentUserId, FriendshipStatus.Declined);
                }

            await SaveChangesAsync();
            return friendship;
        }

        public async Task<Friendship> CancelFriendRequest(Guid currentUserId, Guid otherUserId)
        {
            var friendship = await GetFriendship(currentUserId, otherUserId);

            if(friendship == null)
                return friendship;

            switch (friendship.Status) 
            {
                case(FriendshipStatus.Accepted):
                    throw new ErrorModelException(ErrorCodes.AlreadyFriends, otherUserId.ToString());
                
                case(FriendshipStatus.Pending):
                    if(friendship.LastActionById != currentUserId)
                        throw new ErrorModelException(ErrorCodes.SthWentWrong);
                    break;
                
                case(FriendshipStatus.Declined):
                    throw new ErrorModelException(ErrorCodes.RequestNotFound);

                case(FriendshipStatus.Blocked):
                    if(friendship.LastActionById == currentUserId)
                        throw new ErrorModelException(ErrorCodes.UserBlocked, otherUserId.ToString());
                    throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(ApplicationUser));
            }

            if(friendship.Status == FriendshipStatus.Pending)
                if(friendship.LastActionById == currentUserId)
                {
                    _context.Friendships.Remove(friendship);
                    friendship = null;
                }

            await SaveChangesAsync();
            return friendship;
        }

        public async Task<Friendship> RemoveFriend(Guid currentUserId, Guid otherUserId)
        {
            var friendship = await GetFriendship(currentUserId, otherUserId);

            if(friendship == null)
                return friendship;
            
            if(friendship.Status != FriendshipStatus.Accepted)
                throw new ErrorModelException(ErrorCodes.NotInFriendlist, otherUserId.ToString());

            if(friendship.Status == FriendshipStatus.Blocked)
            {
                if(friendship.LastActionById == currentUserId)
                    throw new ErrorModelException(ErrorCodes.UserBlocked, otherUserId.ToString());
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(ApplicationUser));
            }

            _context.Friendships.Remove(friendship);
            friendship = null;

            await SaveChangesAsync();
            return friendship;
        }

        public async Task<Friendship> BlockUser(Guid currentUserId, Guid otherUserId)
        {
            if(currentUserId.Equals(otherUserId))
                throw new ErrorModelException(ErrorCodes.IncorrectValue, nameof(otherUserId));

            var friendship = await GetFriendship(currentUserId, otherUserId);

            if(friendship != null)
            {
                if(friendship.Status == FriendshipStatus.Blocked)
                {
                    if(friendship.LastActionById != currentUserId)
                        throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(ApplicationUser));
                    return friendship;
                }

                ChangeFriendshipStatusTo(friendship, currentUserId, FriendshipStatus.Blocked);
            }

            if(friendship == null)
            {
                friendship = new Friendship(currentUserId, otherUserId, FriendshipStatus.Blocked);
                _context.Friendships.Add(friendship);
            }

            await SaveChangesAsync();
            return friendship;
        }

        public async Task<Friendship> UnblockUser(Guid currentUserId, Guid otherUserId)
        {
            var friendship = await GetFriendship(currentUserId, otherUserId);

            if(friendship == null)    
                return friendship;
            
            if (friendship.Status == FriendshipStatus.Blocked)
                if(friendship.LastActionById != currentUserId)
                    throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(ApplicationUser));

            _context.Friendships.Remove(friendship);
            friendship = null;

            await SaveChangesAsync();
            return friendship;
        }

        private void ChangeFriendshipStatusTo(Friendship friendship, Guid currentUserId, FriendshipStatus newStatus)
        {
            friendship.Status = newStatus;
            friendship.LastActionById = currentUserId;
        }

        private async Task SaveChangesAsync()
        {
            var success = await _context.SaveChangesAsync() > 0;
            if(!success)
                throw new ErrorModelException(ErrorCodes.DatabaseError); //No changed were made to entity
        }

    }
}