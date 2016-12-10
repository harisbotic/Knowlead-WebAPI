using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.HttpRequestItems;
using Knowlead.DTO.CallModels;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Knowlead.WebApi.Hubs
{
    public class MainHub : Hub
    {
        private readonly IP2PRepository _p2pRepo;
        private readonly Auth _auth;
        
        public MainHub(IP2PRepository p2pRepo,
                    Auth auth)
        {
            _p2pRepo = p2pRepo;
            _auth = auth;
        }
        public override Task OnConnectedAsync()
        {
            System.Console.WriteLine("SOMEONE CONNECTED");
            Context.Connection.Metadata.GetOrAdd("das", _ => "123");
            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync()
        {
            System.Console.WriteLine("SOMEONE DISCONNECTED");
            
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
            await Clients.All.InvokeAsync("notify", e);
        }

        public async Task CallP2p(int p2pId)
        {
            Guid callerId = _auth.GetUserId().GetValueOrDefault();

            var p2p = await _p2pRepo.GetP2PTemp(p2pId);

            var participants = new Dictionary<Guid, string>();
            participants.Add(callerId, "");
            participants.Add(p2p.ScheduledWithId.GetValueOrDefault(), "");

            var p2pCallModel = new P2PCallModel {
                CallerId = callerId,
                Participants = participants

            };
            var json = JsonConvert.SerializeObject(p2pCallModel, new JsonSerializerSettings 
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver() 
            });

            await Clients.User(p2p.ScheduledWith.UserName).InvokeAsync("receiveCall",json);
            await Clients.Client(Context.ConnectionId).InvokeAsync("receiveCall",json);

            return;
        }
        public Task CallAnswer(string code)
        {
            return Task.CompletedTask;
        }
    }

    public class NotificationModel
    {
        public string type {get; set;}
        public string title {get; set;}
        public string subtitle {get; set;}
    }
}