using System;
using System.Collections.Generic;
using System.Linq;
using Knowlead.DTO.CallModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

        public void AddCall(_CallModel callModel)
        {
            Calls.Add(callModel);
        }

        public _CallModel GetCall(Guid callModelId)
        {
            return Calls.Where(x => x.CallId == callModelId).FirstOrDefault();
        }

        public PeerInfoModel GetPeer(Guid callModelId, Guid userId)
        {
            return GetCall(callModelId).Peers.Where(x => x.PeerId == userId).FirstOrDefault();
        }

        public async void CallModelUpdate(_CallModel callModel)
        {
            var json = JsonConvert.SerializeObject(callModel, new JsonSerializerSettings 
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver() 
            });

            foreach (var peer in callModel.Peers)
            {
                if(peer.Status == PeerInfoModel.PeerStatus.Accepted ||
                    peer.Status == PeerInfoModel.PeerStatus.Rejected)
                    await _hubContext.Clients.Client(peer.ConnectionId).InvokeAsync("callModelUpdate", json);
            }
        }
    }
}