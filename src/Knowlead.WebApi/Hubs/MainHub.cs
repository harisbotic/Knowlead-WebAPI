using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Exceptions;
using Knowlead.Common.HttpRequestItems;
using Knowlead.DTO.CallModels;
using Knowlead.DTO.ChatModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using static Knowlead.Common.Constants;
using static Knowlead.Common.Constants.EnumStatuses;

namespace Knowlead.WebApi.Hubs
{
    public class MainHub : Hub
    {
        private readonly IP2PRepository _p2pRepo;
        private readonly ICallServices _callServices;
        private readonly IChatServices _chatServices;
        private readonly Auth _auth;
        
        public MainHub(IP2PRepository p2pRepo,
                       ICallServices callServices,
                       IChatServices chatServices,
                       Auth auth)
        {
            _p2pRepo = p2pRepo;
            _callServices = callServices;
            _chatServices = chatServices;
            _auth = auth;
        }

        private _CallModel GetCallModel(Guid callModelId)
        {
            var ret = _callServices.GetCall(callModelId);
            // Check if this call exists
            if (ret == null)
            {
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(_CallModel));
            }
            var userId = _auth.GetUserId();
            // Check if this guy has anything to do with this call
            if (ret.Peers.Where(p => p.PeerId.Equals(userId)).Count() == 0)
            {
                throw new ErrorModelException(ErrorCodes.AuthorityError);
            }
            return ret;
        }
        public async override Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).InvokeAsync("setConnectionId", Context.ConnectionId);
        }

        public async override Task OnDisconnectedAsync(Exception e)
        {   
            await _callServices.RemoveConnectionFromCall(Context.ConnectionId);
        }

        public async Task StartP2pCall(int p2pId)
        {
            Guid callerId = _auth.GetUserId();
            _CallModel currentCall;
            if ((currentCall = this._callServices.GetCallForUser(callerId)) != null)
            {
                throw new ErrorModelException(ErrorCodes.AlreadyInCall, currentCall.CallId.ToString());
            }

            var p2p = await _p2pRepo.GetP2PTemp(p2pId);

            //Is he related to this p2p
            if(!p2p.CreatedById.Equals(callerId) && !p2p.ScheduledWithId.GetValueOrDefault().Equals(callerId))
                return;

            if(p2p.Status == P2PStatus.Finished)
                throw new ErrorModelException(ErrorCodes.P2PFinished, p2p.P2pId.ToString());

            var p2pCallModel = new P2PCallModel(p2p, callerId, Context.ConnectionId);

            await _callServices.StartCall(p2pCallModel);
            
            // send a request to delete p2p 

        }

        public async Task CallRespond(Guid callModelId, bool accepted)
        {
            var callModel = GetCallModel(callModelId);
            var peerInfoModel = _callServices.GetPeer(callModel, _auth.GetUserId());

            if(peerInfoModel == null)
                return;

            peerInfoModel.ConnectionId = Context.ConnectionId;

            if (accepted)
                await _callServices.AcceptCall(callModel, peerInfoModel);
            else
                await _callServices.CloseCall(callModel, CallEndReasons.Rejected);
            
            return;
        }

        public async Task AddSDP(Guid callModelId, String sdp)
        {
            var callModel = GetCallModel(callModelId);
            var peerInfoModel = _callServices.GetPeer(callModel, _auth.GetUserId());

            if(peerInfoModel == null) // This shouldn't happen
                return;

            peerInfoModel.sdps.Add(sdp);
            await _callServices.CallModelUpdate(callModel, false);
        }

        public async Task StopCall(Guid callModelId, String reason)
        {
            var callModel = GetCallModel(callModelId);
            await _callServices.CloseCall(callModel, reason);
        }

        public async Task ResetCall(Guid callModelId)
        {
            var callModel = this.GetCallModel(callModelId);
            foreach (var peerInfoModelEach in callModel.Peers)
            {
                peerInfoModelEach.sdps.Clear();
                Console.WriteLine("Peer: " + peerInfoModelEach.PeerId + " - " + peerInfoModelEach.ConnectionId);
            }
            var peerInfoModel = _callServices.GetPeer(callModel, _auth.GetUserId());
            if (peerInfoModel == null) // Will this ever happen ?
            {
                // If user was previously disconnected, add him to call and put his status to accepted
                callModel.Peers.Add(new PeerInfoModel(_auth.GetUserId(), Context.ConnectionId, PeerStatus.Accepted));
                Console.WriteLine("Re-adding " + _auth.GetUserId() + " to call - connection id " + Context.ConnectionId);
            } else
            {
                // If user wasn't disconnected, just update his connection id to current connection
                peerInfoModel.ConnectionId = Context.ConnectionId;
                peerInfoModel.UpdateStatus(PeerStatus.Accepted);
                Console.WriteLine("Updating connection id for " + _auth.GetUserId() + " in call to " + Context.ConnectionId);
            }
            await _callServices.CallModelUpdate(callModel, true);
        }

        public void DisconnectFromCall(Guid callModelId)
        {
            var call = this.GetCallModel(callModelId);
            _callServices.DisconnectFromCall(call, _auth.GetUserId());
        }

        public void CallMsg(ChatMessageModel chatMessageModel)
        {
            if (!chatMessageModel.RecipientId.HasValue)
            {
                throw new ErrorModelException(ErrorCodes.HackAttempt);
            }
            this.GetCallModel(chatMessageModel.RecipientId.Value);
            chatMessageModel.Timestamp = DateTime.UtcNow;
            chatMessageModel.SenderId = _auth.GetUserId();
            _callServices.CallMsg(chatMessageModel);
        }

        public async Task Msg(ChatMessageModel chatMessageModel)
        {
            var currentUser = _auth.GetUserId();         
            var chatMessage = await _chatServices.SendChatMessage(chatMessageModel, currentUser);

            var json = JsonConvert.SerializeObject(Mapper.Map<ChatMessageModel>(chatMessage), new JsonSerializerSettings 
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver() 
            });

            await Clients.User(currentUser.ToString())
                .InvokeAsync(WebClientFuncNames.DisplayChatMsg, json);   

            await Clients.User(chatMessageModel.RecipientId.ToString())
                            .InvokeAsync(WebClientFuncNames.DisplayChatMsg, json);
        }
    }
}