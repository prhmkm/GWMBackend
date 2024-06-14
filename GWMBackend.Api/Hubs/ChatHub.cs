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
        public async Task JoinToChatRoom(UserConnection conn)
        {
            if (conn.UserName != null && conn.UserName != "")
            {


                if (conn.ChatRoom != null && conn.ChatRoom != "")
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
                        .SendAsync("JoinToChatRoom", "main-admin", $"{conn.UserName} has joined {conn.ChatRoom} at {DateTime.Now.ToString("hh:mm tt")}");
                }
                else
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, conn.UserName + "-chatroom");

                    var _user = _repositoryContext.HubConnections.FirstOrDefault(s => (s.ConnectionId == Context.ConnectionId || s.Username == conn.UserName));

                    if (_user == null)
                    {
                        _repositoryContext.HubConnections.Add(new HubConnection
                        {
                            ConnectionId = Context.ConnectionId,
                            Username = conn.UserName,
                            ChatRoom = conn.UserName + "-chatroom"
                        });
                        _repositoryContext.SaveChanges();
                    }
                    else
                    {
                        _user.Username = conn.UserName;
                        _user.ChatRoom = conn.UserName + "-chatroom";
                        _user.ConnectionId = Context.ConnectionId;
                        _repositoryContext.HubConnections.Update(_user);
                        _repositoryContext.SaveChanges();
                    }

                    await Clients.Group(conn.UserName + "-chatroom")
                        .SendAsync("JoinToChatRoom", "main-admin", $"{conn.UserName} has joined {conn.UserName + "-chatroom"}  at {DateTime.Now.ToString("hh:mm tt")}");
                }
            }
            
        }
        public async Task SendMessage(string msg)
        {
            if (msg != "")
            {
                var _user = _repositoryContext.HubConnections.FirstOrDefault(s => s.ConnectionId == Context.ConnectionId);


                if (_user != null)
                {
                    var _chatroomUsers = _repositoryContext.HubConnections.Count(s => s.ChatRoom == _user.ChatRoom);

                    if (_chatroomUsers > 1)
                    {
                        await Clients.Group(_user.ChatRoom)
                            .SendAsync("SendMessage", _user.Username, msg, _user.ChatRoom, DateTime.Now.ToString("hh:mm tt"));
                    }
                    else
                    {
                        await Clients.Groups("admins", _user.ChatRoom)
                            .SendAsync("SendMessage", _user.Username, msg, _user.ChatRoom, DateTime.Now.ToString("hh:mm tt"));
                    }

                    //save it to database

                    var data = new ChatLog()
                    {
                        SenderUsername = _user.Username,
                        ChatRoom = _user.ChatRoom,
                        MessageContent = msg
                    };

                    _repositoryContext.ChatLogs.Add(data);
                    _repositoryContext.SaveChanges();

                }
            }
        }
    }
}
