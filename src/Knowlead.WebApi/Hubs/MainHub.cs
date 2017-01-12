using System;
using System.Linq;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.HttpRequestItems;
using Knowlead.DTO.CallModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
 
            await Clients.User(Context.User.Identity.Name).InvokeAsync("setConnectionId", Context.ConnectionId);
        }

        // public override Task OnDisconnectedAsync()
        // {
        //     System.Console.WriteLine($"{Context.ConnectionId} {Context.User.Identity.Name} DISCONNECTED");
            
        //     return Task.CompletedTask;
        // }

        public async Task Send(string message)
        {
            System.Console.WriteLine("PRIMIO SAM PORUKU*********\n *********** " + message);
            
            var e = new NotificationModel{
                type = "error",
                title = "Title",
                subtitle = "Subtitle"
            };             
            await Clients.All.InvokeAsync("notify", e);
        }

        public async Task CallP2p(int p2pId)
        {
            Guid callerId = _auth.GetUserId();

            var p2p = await _p2pRepo.GetP2PTemp(p2pId);

            var p2pCallModel = new P2PCallModel(p2pId, callerId, Context.ConnectionId);
            p2pCallModel.Peers.Add(new PeerInfoModel(p2p.ScheduledWithId.GetValueOrDefault()));

            _callServices.AddCall(p2pCallModel);

            var json = JsonConvert.SerializeObject(p2pCallModel, new JsonSerializerSettings 
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver() 
            });

            //TODO: make a function which will call all peers in call
            await Clients.User(p2p.ScheduledWith.UserName).InvokeAsync("receiveCall",json);
            await Clients.Client(Context.ConnectionId).InvokeAsync("receiveCall",json);

            //Start a 1 minute timer, that will send Call Rejected to both if call didn't start
        }

        private void GetPeerInfoModel(Guid callId, out PeerInfoModel peerInfoModel, out _CallModel callModel)
        {
            callModel = _callServices.GetCall(callId);

            if(callModel == null)
            {
                peerInfoModel = null;
                return;
            }

            peerInfoModel = callModel.Peers.Where(x => x.PeerId == _auth.GetUserId()).FirstOrDefault();

            if(peerInfoModel == null)
                return;
        }

        public Task CallRespond(Guid callId, bool accepted) //callId -> callModelId
        {
            PeerInfoModel peerInfoModel;
            _CallModel callModel;
            GetPeerInfoModel(callId, out peerInfoModel, out callModel);
            if (peerInfoModel == null || callModel == null)
                return Task.CompletedTask;
            peerInfoModel.ConnectionId = Context.ConnectionId;
            peerInfoModel.UpdateStatus(accepted);
            _callServices.CallModelUpdate(callModel);
            
            return Task.CompletedTask;
        }

        public Task AddSDP(Guid callId, String sdp) //callId -> callModelId
        {
            PeerInfoModel peerInfoModel;
            _CallModel callModel;
            GetPeerInfoModel(callId, out peerInfoModel, out callModel);
            if (peerInfoModel == null || callModel == null)
                return Task.CompletedTask;

            peerInfoModel.sdps.Add(sdp);
            _callServices.CallModelUpdate(callModel);
            
            return Task.CompletedTask;
        }

        public Task ClearSDP(Guid callId) //callId -> callModelId
        {
            PeerInfoModel peerInfoModel;
            _CallModel callModel;
            GetPeerInfoModel(callId, out peerInfoModel, out callModel);
            if (peerInfoModel == null || callModel == null)
                return Task.CompletedTask;

            peerInfoModel.sdps.Clear();
            _callServices.CallModelUpdate(callModel);
            
            return Task.CompletedTask;
        }

        public String GetCallModel(Guid callId)
        {
            var callModel = _callServices.GetCall(callId);

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