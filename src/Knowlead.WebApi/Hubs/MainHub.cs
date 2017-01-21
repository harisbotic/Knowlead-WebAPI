using System;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.HttpRequestItems;
using Knowlead.DTO.CallModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using static Knowlead.Common.Constants.EnumStatuses;

namespace Knowlead.WebApi.Hubs
{
    public class MainHub : Hub
    {
        private readonly IP2PRepository _p2pRepo;
        private readonly ICallServices _callServices;
        private readonly Auth _auth;
        
        public MainHub(IP2PRepository p2pRepo,
                       ICallServices callServices,
                       Auth auth)
        {
            _p2pRepo = p2pRepo;
            _callServices = callServices;
            _auth = auth;
        }
        public async override Task OnConnectedAsync()
        {
            System.Console.WriteLine($"{Context.ConnectionId} {Context.User.Identity.Name} CONNECTED");
            await Clients.Client(Context.ConnectionId).InvokeAsync("setConnectionId", Context.ConnectionId);
        }

        public override Task OnDisconnectedAsync(Exception e)
        {
            System.Console.WriteLine($"{Context.ConnectionId} {Context.User.Identity.Name} DISCONNECTED");
            
            _callServices.RemoveConnectionFromCall(Context.ConnectionId);

            return Task.CompletedTask;
        }

        public async Task Send(string message)
        {
            System.Console.WriteLine("PRIMIO SAM PORUKU*********\n *********** " + message);
            
            var e = new NotificationModel{
                type = "error",
                title = "Title",
                subtitle = "Subtitle"
            };             
            await Clients.User(_auth.GetUserId().ToString()).InvokeAsync("notify", e);
        }

        public async Task StartP2pCall(int p2pId)
        {
            Guid callerId = _auth.GetUserId();

            var p2p = await _p2pRepo.GetP2PTemp(p2pId);

            //Is he related to this p2p
            if(!p2p.CreatedById.Equals(callerId) && !p2p.ScheduledWithId.GetValueOrDefault().Equals(callerId))
                return;

            var p2pCallModel = new P2PCallModel(p2p, callerId, Context.ConnectionId);

            _callServices.StartCall(p2pCallModel);
            //TODO: Start a 1 minute timer, that will send CallCanceled to both if call didn't start
        }

        public Task CallRespond(Guid callModelId, bool accepted)
        {
            var callModel = _callServices.GetCall(callModelId);
            var peerInfoModel = _callServices.GetPeer(callModel, _auth.GetUserId());

            if(peerInfoModel == null)
                return Task.CompletedTask;

            peerInfoModel.ConnectionId = Context.ConnectionId;
            peerInfoModel.UpdateStatus(accepted);

            _callServices.CallModelUpdate(callModel, false);
            
            return Task.CompletedTask;
        }

        public Task AddSDP(Guid callModelId, String sdp)
        {
            var callModel = _callServices.GetCall(callModelId);
            var peerInfoModel = _callServices.GetPeer(callModel, _auth.GetUserId());

            if(peerInfoModel == null)
                return Task.CompletedTask;

            peerInfoModel.sdps.Add(sdp);
            _callServices.CallModelUpdate(callModel, false);
            
            return Task.CompletedTask;
        }

        public Task ResetCall(Guid callModelId)
        {
            var callModel = _callServices.GetCall(callModelId);
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
            _callServices.CallModelUpdate(callModel, true);
            
            return Task.CompletedTask;
        }

        public String GetCallModel(Guid callModelId) //Is this used?
        {
            var callModel = _callServices.GetCall(callModelId);

            var json = JsonConvert.SerializeObject(callModel, new JsonSerializerSettings 
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver() 
            });
            
            return json;
        }
    }

    public class NotificationModel
    {
        public string type {get; set;}
        public string title {get; set;}
        public string subtitle {get; set;}
    }
}