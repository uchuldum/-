using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using signalrChat.Models;
using signalrChat.Services;
using AutoMapper;
using signalrChat.DTO;

namespace signalrChat.Hubs
{
    /// <summary>
    /// Хаб для чата
    /// </summary>
    public class ChatHub : Hub
    {
        private readonly IUsersHandler _usersHandler;
        private readonly IMessagesHandler _messagesHandler;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="usersHandler">Сервис для хранения и  работы с подключенными пользователями</param>
        /// <param name="messagesHandler">Сервис для хранения и работы с сообщениями</param>
        /// <param name="mapper">Маппер</param>
        public ChatHub(IUsersHandler usersHandler, IMessagesHandler messagesHandler, IMapper mapper)
        {
            _usersHandler = usersHandler;
            _messagesHandler = messagesHandler;
            _mapper = mapper;
        }

        private IEnumerable<User> GetClients(string groupId = null)
        {
            var data = (from user in _usersHandler.GetUsers()
                        select new User(user.NickName, user.State, user.LastVizit, user.AvatarID));

            return data;
        }

        public async Task NewMessage(MessageDTO data)
        {
            var message = _mapper.Map<Message>(data);

            _messagesHandler.Add(message);
            
            if(!string.IsNullOrEmpty(message.DestNickName))
            {
                foreach (var connectionId in _usersHandler.GetConnections(message.DestNickName))
                {
                    await Clients.Client(connectionId).SendAsync("MessageReceivedToClient", _mapper.Map<MessageDTO>(message));
                }
                await Clients.Client(Context.ConnectionId).SendAsync("MessageReceivedToClient", _mapper.Map<MessageDTO>(message));
            }
            else
            {
                await Clients.All.SendAsync("MessageReceivedAll", _mapper.Map<MessageDTO>(message));
            }
        }

        public async Task GetDialog(string destNickName)
        {
            var connectionID = Context.ConnectionId;

            var user = _usersHandler.GetUser(connectionID);

            var messages = _messagesHandler.GetDialog(user.NickName, destNickName);

            await Clients.Client(connectionID).SendAsync("GetDialog", _mapper.Map<IEnumerable<MessageDTO>>(messages));
        }

        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            var name = httpContext.Request.Query["nickName"];

            var user = _usersHandler.Add(name, Context.ConnectionId);

            if(user != null && user.State == UserStates.Online)
                Clients.All.SendAsync("Connected", _mapper.Map<UserDTO>(user));

            Clients.Client(Context.ConnectionId).SendAsync("UrMessages", _mapper.Map<IEnumerable<MessageDTO>>(_messagesHandler.GetAll(user.NickName)));

            Clients.Client(Context.ConnectionId).SendAsync("Clients", _mapper.Map<IEnumerable<UserDTO>>(GetClients()));

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var user = _usersHandler.Remove(Context.ConnectionId);

            if (user != null && user.State == UserStates.Offline)
                Clients.All.SendAsync("Disconnected", _mapper.Map<UserDTO>(user));

            return base.OnDisconnectedAsync(exception);
        }
    }
}
