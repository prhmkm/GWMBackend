using GWMBackend.Data.Base;
using GWMBackend.Domain.Models;
using Microsoft.AspNetCore.SignalR;
using static GWMBackend.Domain.DTOs.ChatDTO;

namespace GWMBackend.Api.Hubs
{
    public class ChatHub : Hub
    {
        GWM_DBContext _repositoryContext;
        public ChatHub(GWM_DBContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public async Task JoinChat(UserConnection conn)
        {
            await Clients.All
                .SendAsync("ReceiveMessage", "admin", $"{conn.UserName} has joined");
        }
        public async Task JoinToChatRoom(UserConnection conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);

            var _user = _repositoryContext.HubConnections.FirstOrDefault(s => (s.ConnectionId == Context.ConnectionId || s.Username == conn.UserName));

            if (_user == null)
            {
                _repositoryContext.HubConnections.Add(new HubConnection
                {
                    ConnectionId = Context.ConnectionId,
                    Username = conn.UserName,
                    ChatRoom = conn.ChatRoom
                });
                _repositoryContext.SaveChanges();
            }
            else
            {
                _user.Username = conn.UserName;
                _user.ChatRoom = conn.ChatRoom;
                _user.ConnectionId = Context.ConnectionId;
                _repositoryContext.HubConnections.Update(_user);
                _repositoryContext.SaveChanges();
            }

            await Clients.Group(conn.ChatRoom)
                .SendAsync("JoinToChatRoom", "admin", $"{conn.UserName} has joined {conn.ChatRoom} chatroom");
        }
        public async Task SendMessage(string msg)
        {
            var _user = _repositoryContext.HubConnections.FirstOrDefault(s => s.ConnectionId == Context.ConnectionId);

            if (_user != null)
            {
                await Clients.Group(_user.ChatRoom)
            .SendAsync("SendMessage", _user.Username, msg);

            }
        }
    }
}
