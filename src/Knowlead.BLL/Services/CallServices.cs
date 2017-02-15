using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Knowlead.DTO.CallModels;
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

        private void CheckCall(_CallModel call)
        {
            if (call.Peers.Where(p => p.Status == PeerStatus.Waiting || p.Status == PeerStatus.Accepted).Count() == 0)
            {
                this.CloseCall(call, CallEndReasons.Inactive);
            }
        }

        public PeerInfoModel GetPeer(_CallModel callModel, Guid userId)
        {
            return callModel.Peers.Where(x => x.PeerId == userId).FirstOrDefault();
        }

        public void CloseCall(_CallModel callModel, string reason)
        {
            // TODO: DO SOMETHING WITH THIS REASON
            Calls.Remove(callModel);
            foreach (var peer in callModel.Peers)
            {
                try {
                    _hubContext.Clients.Client(peer.ConnectionId).InvokeAsync("stopCall", reason);
                } catch (Exception)
                {
                    Console.WriteLine("Tried to hang up call with disconnected peer");
                }
            }
        }

        public void CallModelUpdate(_CallModel callModelReceived, bool reset)
        {
            _CallModel callModel = Mapper.Map<_CallModel>(callModelReceived);
            var json = JsonConvert.SerializeObject(callModel, new JsonSerializerSettings 
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver() 
            });

            foreach (var peer in callModel.Peers)
            {
                if(peer.Status == PeerStatus.Accepted ||
                    peer.Status == PeerStatus.Rejected)
                {
                    try
                    {
                        _hubContext.Clients.Client(peer.ConnectionId).InvokeAsync((reset) ? "callReset" : "callModelUpdate", json);
                    } catch (Exception)
                    {
                        Console.WriteLine("Tried to reset disconnected peer");
                    }
                }
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
                        this.CheckCall(call);
                        return true;
                    }
            
            return false;
        }
    }
}