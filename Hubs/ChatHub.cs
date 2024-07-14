using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ChatApp.Hubs
{
    public class ChatHub: Hub
    {
        private static readonly Dictionary<string, string> ConnectedUsers = new Dictionary<string, string>();

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", new { user, message });
        }

        public async Task SendPrivateMessage(string user, string receiver, string message)
        {
            var connectionId = ConnectedUsers[receiver];
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", new { user, message });
        }


        [Authorize]
        public override async Task OnConnectedAsync()
        {

            var userId = Context.UserIdentifier; // Get user ID from authentication
            await Clients.All.SendAsync("ReceiveSystemMessage", $"{userId} joined.");
            await base.OnConnectedAsync();
        }

        [Authorize]
        public override async Task OnDisconnectedAsync(Exception exception)
        {

            var userId = Context.UserIdentifier; // Get user ID from authentication
            await Clients.All.SendAsync("ReceiveSystemMessage", $"{userId} left.");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
