using Microsoft.AspNetCore.SignalR;
using static GWMBackend.Domain.DTOs.ChatDTO;

namespace GWMBackend.Api.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinChat(UserConnection conn)
        {
            await Clients.All
                .SendAsync("ReceiveMessage", "admin", $"{conn.UserName} has joined");
        }
        public async Task JoinToChatRoom(UserConnection conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);

            await Clients.Group(conn.ChatRoom)
                .SendAsync("JoinToChatRoom", "admin", $"{conn.UserName} has joined {conn.ChatRoom} chatroom");
        }
        public async Task SendMessageToGroup(UserSendMessage conn)
        {
            await Clients.Group(conn.ChatRoom)
                .SendAsync("SendMessageToGroup", "admin", $"{conn.Messgae}");
        }
    }
}
