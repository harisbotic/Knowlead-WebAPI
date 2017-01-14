using System;
using System.Collections.Generic;
using System.Linq;
using Knowlead.DTO.CallModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using static Knowlead.Common.Constants.EnumStatuses;

namespace Knowlead.Services
{
    public class CallServices<THub> : ICallServices where THub : Hub
    {
        private static List<_CallModel> Calls { get; set; } = new List<_CallModel>();
        private readonly IHubContext<THub> _hubContext;

        public CallServices(IHubContext<THub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async void StartCall(_CallModel callModel)
        {
            Calls.Add(callModel);

            var json = JsonConvert.SerializeObject(callModel, new JsonSerializerSettings 
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver() 
            });

            await _hubContext.Clients.Client(callModel.Peers.FirstOrDefault().ConnectionId).InvokeAsync("startCall", json);

            foreach (var peer in callModel.Peers)
            {
                if(peer.PeerId != callModel.Caller.PeerId)
                    await _hubContext.Clients.User(peer.PeerId.ToString()).InvokeAsync("callInvitation", json);
            }
        }

        public _CallModel GetCall(Guid callModelId)
        {
            return Calls.Where(x => x.CallId == callModelId).FirstOrDefault();
        }

        public PeerInfoModel GetPeer(_CallModel callModel, Guid userId)
        {
            return callModel.Peers.Where(x => x.PeerId == userId).FirstOrDefault();
        }

        public void CloseCall(_CallModel callModel)
        {
            Calls.Remove(callModel);
        }

        public async void CallModelUpdate(_CallModel callModel)
        {
            var json = JsonConvert.SerializeObject(callModel, new JsonSerializerSettings 
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver() 
            });

            foreach (var peer in callModel.Peers)
            {
                if(peer.Status == PeerStatus.Accepted ||
                    peer.Status == PeerStatus.Rejected)
                    await _hubContext.Clients.Client(peer.ConnectionId).InvokeAsync("callModelUpdate", json);
            }
        }

        public bool RemoveConnectionFromCall(string connectionId)
        {
            foreach (var call in Calls)
                foreach (var peer in call.Peers)
                    if(peer.ConnectionId == connectionId)
                    {
                        peer.UpdateStatus(PeerStatus.Disconnected);
                        peer.ConnectionId = null;
                        peer.sdps.Clear();

                        if(call.Peers.Count == 0)
                            CloseCall(call);

                        return true;
                    }
            
            return false;
        }
    }
}