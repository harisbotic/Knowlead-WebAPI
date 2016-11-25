using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Knowlead.WebApi.Attributes;

namespace Knowlead.WebApi.Hubs
{
    [ReallyAuthorize]
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                Context.Connection.Channel.Dispose();
            }

            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync()
        {
            return Task.CompletedTask;
        }

        public async Task Send(string message)
        {
            await Clients.All.InvokeAsync("Send", $"{Context.User.Identity.Name}: {message}");
        }
    }
}