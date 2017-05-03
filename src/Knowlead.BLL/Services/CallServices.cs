using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.DomainModel.CallModels;
using Knowlead.DomainModel.NotificationModels;
using Knowlead.DTO.CallModels;
using Knowlead.DTO.ChatModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using static Knowlead.Common.Constants;
using static Knowlead.Common.Constants.EnumStatuses;

namespace Knowlead.Services
{
    public class CallServices<THub> : ICallServices where THub : Hub
    {
        private static List<_CallModel> Calls { get; set; } = new List<_CallModel>();
        private readonly IHubContext<THub> _hubContext;
        private readonly ITransactionServices _transactionServices;
        private readonly IP2PRepository _p2pRepository;
        private readonly INotificationServices _notificationServices;
        private readonly ICallRepository _callRepository;

        public CallServices(IHubContext<THub> hubContext, ITransactionServices transactionServices,
                             IP2PRepository p2pRepository, INotificationServices notificationServices, ICallRepository callRepository)
        {
            _hubContext = hubContext;
            _transactionServices = transactionServices;
            _p2pRepository = p2pRepository;
            _notificationServices = notificationServices;
            _callRepository = callRepository;
        }

        private string GetJsonString<T>(T callModel)
        {
            return JsonConvert.SerializeObject(Mapper.Map<T>(callModel), new JsonSerializerSettings 
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver() 
            });
        }

        public async Task SendInvitations(Guid callId)
        {
            _CallModel call = this.GetCall(callId);
            if (call != null)
            {
                await SendInvitations(call);
            }
        }

        public async Task SendInvitations(_CallModel callModel)
        {
            var json = GetJsonString(callModel);
            int invitationsSent = 0;
            foreach (var peer in callModel.Peers)
            {
                // Send invitations to all peers that are not connected
                if(peer.Status != PeerStatus.Accepted)
                {
                    await _hubContext.Clients.User(peer.PeerId.ToString()).InvokeAsync("callInvitation", json);
                    invitationsSent++;
                }
            }
            // If we sent invitations that means that we should send them again
            if (invitationsSent > 0)
            {
                BackgroundJob.Schedule<ICallServices>((callService) => callService.SendInvitations(callModel.CallId), TimeSpan.FromSeconds(10));
                callModel.Inviting = true;
                if (!callModel.InactiveSince.HasValue)
                {
                    callModel.InactiveSince = DateTime.UtcNow;
                }
                // If call was inactive more then minute, we should stop it
                if (DateTime.UtcNow.Subtract(callModel.InactiveSince.Value) > TimeSpan.FromSeconds(60))
                {
                    await this.CloseCall(callModel, CallEndReasons.Inactive);
                }
            } else
            {
                callModel.Inviting = false;
                callModel.InactiveSince = null;
            }
        }

        private async Task SendStartCall(_CallModel callModel, PeerInfoModel peer)
        {
            await _hubContext.Clients.Client(peer.ConnectionId).InvokeAsync("startCall", GetJsonString(callModel));
            peer.UpdateStatus(true);
        }

        public async Task StartCall(_CallModel callModel)
        {
            Calls.Add(callModel);
            await this.SendStartCall(callModel, callModel.Caller);
            await this.CheckCall(callModel);
        }

        public _CallModel GetCallForUser(Guid applicationUserId)
        {
            try
            {
                return Calls.Where(c => c.Peers.Where(p => p.PeerId.Equals(applicationUserId)).Count() > 0).First();
            } catch (Exception)
            {
                return null;
            }
        }

        public _CallModel GetCall(Guid callModelId)
        {
            return Calls.Where(x => x.CallId == callModelId).FirstOrDefault();
        }

        private async Task CheckCall(_CallModel call)
        {
            if (call.Peers.Where(p => p.Status == PeerStatus.Waiting || p.Status == PeerStatus.Accepted).Count() == 0)
            {
                await this.CloseCall(call, CallEndReasons.Inactive);
            }
            if (!call.Inviting)
            {
                await this.SendInvitations(call);
            }
        }

        public PeerInfoModel GetPeer(_CallModel callModel, Guid userId)
        {
            return callModel.Peers.Where(x => x.PeerId == userId).FirstOrDefault();
        }

        public async Task CloseCall(_CallModel callModel, string reason)
        {
            var p2pCallModel = callModel as P2PCallModel;

            if(p2pCallModel != null)
            {
                var p2p = await _p2pRepository.GetP2PTemp(p2pCallModel.P2pId);
                var callerPeerId = p2pCallModel.Caller.PeerId;
                var otherPeerId = p2pCallModel.CallReceiverId;
                var teacherPointsAward = p2p.PriceAgreed.Value * 1.7;
                var studentPointsAward = p2p.PriceAgreed.Value * 1.2;
                if(DateTime.UtcNow.Ticks > callModel.StartDate.AddSeconds(70).Ticks)
                {
                    await _transactionServices.RewardMinutes(callerPeerId, 0, (int)studentPointsAward, TransactionReasons.P2PCallEnded);
                    await _transactionServices.RewardMinutes(otherPeerId, p2p.PriceAgreed.Value, (int)teacherPointsAward, TransactionReasons.P2PCallEnded);
                    
                    p2p.Status = P2PStatus.Finished;
                    await _p2pRepository.UpdateAndSave(p2p);

                    var notification = new Notification(callerPeerId, NotificationTypes.LeaveP2PFeedback, DateTime.UtcNow, otherPeerId, p2p, null);
                    await _notificationServices.SendNotification(notification);
                }

                p2pCallModel.EndDate = DateTime.UtcNow;
                var p2pCall = Mapper.Map<P2PCall>(p2pCallModel);
                _callRepository.Add(p2pCall);
                await _callRepository.Commit();
            }

            Calls.Remove(callModel);
            foreach (var peer in callModel.Peers)
            {
                try {
                    await _hubContext.Clients.Client(peer.ConnectionId).InvokeAsync("stopCall", reason);
                } catch (Exception)
                {
                    Console.WriteLine("Tried to hang up call with disconnected peer");
                }
            }
        }

        public void CallMsg(ChatMessageModel message)
        {
            var call = this.GetCall(message.RecipientId.Value);
            var msg = this.GetJsonString(message);
            foreach (var peer in call.Peers)
            {
                try {
                    _hubContext.Clients.Client(peer.ConnectionId).InvokeAsync("callMsg", msg);
                } catch(Exception)
                {
                    Console.WriteLine("Tried to send chat message to disconnected peer");
                }
            }
        }

        public async Task DisconnectFromCall(_CallModel callModel, Guid userId)
        {
            callModel.Peers
                .Where(p => p.PeerId == userId)
                .First()
                .UpdateStatus(PeerStatus.Disconnected);
            await this.CheckCall(callModel);
        }

        public async Task CallModelUpdate(_CallModel callModelReceived, bool reset)
        {
            _CallModel callModel = Mapper.Map<_CallModel>(callModelReceived);
            var json = GetJsonString(callModel);

            foreach (var peer in callModel.Peers)
            {
                if(peer.Status == PeerStatus.Accepted ||
                    peer.Status == PeerStatus.Rejected)
                {
                    try
                    {
                        await _hubContext.Clients.Client(peer.ConnectionId).InvokeAsync((reset) ? "callReset" : "callModelUpdate", json);
                    } catch (Exception)
                    {
                        Console.WriteLine("Tried to reset disconnected peer");
                    }
                }
            }
            await this.CheckCall(callModel);
        }

        public async Task<bool> RemoveConnectionFromCall(string connectionId)
        {
            foreach (var call in Calls)
                foreach (var peer in call.Peers)
                    if(peer.ConnectionId == connectionId)
                    {
                        peer.UpdateStatus(PeerStatus.Disconnected);
                        peer.ConnectionId = null;
                        peer.sdps.Clear();
                        await this.CheckCall(call);
                        return true;
                    }
            
            return false;
        }

        public async Task AcceptCall(_CallModel callModel, PeerInfoModel peerInfoModel)
        {
            await this.SendStartCall(callModel, peerInfoModel);
        }
    }
}