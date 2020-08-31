using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using signalrChat.Models;


namespace signalrChat.Services
{
    /// <summary>
    /// Класс для работы с подключенными пользователями
    /// </summary>
    public class UsersHandler : IUsersHandler
    {
        private readonly List<User> connectedUsers = new List<User>();

        private User GetUserByNickName(string nickName)
        {
            foreach (var user in connectedUsers)
            {
                if (string.Equals(user.NickName, nickName, StringComparison.OrdinalIgnoreCase))
                {
                    return user;
                }
            }
            
            return null;
        }

        private User GetUserByConnectionID(string connectionID)
        {
            foreach(var user in connectedUsers)
            {
                string _id;

                if(user.IDs != null && user.IDs.Count > 0 && user.IDs.TryGetValue(connectionID, out _id))
                {
                    return user;
                }
            }

            return null;
        }

        private User Remove(User user, string connectionId)
        {
            if (user == null)
                return null;

            lock (user)
            {
                user.IDs.Remove(connectionId);

                if (user.IDs.Count == 0)
                {
                    user.State = UserStates.Offline;
                    user.LastVizit = DateTimeOffset.Now;
                    //connectedUsers.Remove(user);
                }

                return user;
            }
        }

        public int Count
        {
            get
            {
                return connectedUsers.Count;
            }
        }

        public User Add(string nickName, string connectionId)
        {
            lock (connectedUsers)
            {
                User user = GetUserByNickName(nickName);

                if(user == null)
                {
                    Random r = new Random();
                    user = new User(nickName, connectionId, 1 + r.Next(5));
                    connectedUsers.Add(user);
                }
                    
                lock (user)
                {
                    if (UserStates.Offline == user.State)
                    {
                        user.State = UserStates.Online;
                    }

                    user.IDs.Add(connectionId);

                    return user;
                }
            }
        }

        public IEnumerable<string> GetConnections(string nickName)
        {
            var user = GetUserByNickName(nickName);

            if (user.IDs.Count() > 0)
                return user.IDs;

            if (user == null)
                return Enumerable.Empty<string>();

            return Enumerable.Empty<string>();
        }

        public User Remove(string nickName, string connectionId)
        {
            lock (connectedUsers)
            {
                return Remove(GetUserByNickName(nickName), connectionId);
            }
        }

        public User Remove(string connectionId)
        {
            lock (connectedUsers)
            {
                return Remove(GetUserByConnectionID(connectionId), connectionId);
            }
        }

        public IEnumerable<User> GetUsers()
        {
            return connectedUsers;
        }

        public User GetUser(string connectionID)
        {
            return GetUserByConnectionID(connectionID);
        }
    }

    public interface IUsersHandler
    {
        /// <summary>
        /// Добавить пользователя в список подключенных
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="connectionId"></param>
        User Add(string nickName, string connectionId);

        /// <summary>
        /// Убрать подлючение у пользователя
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="connectionId"></param>
        User Remove(string nickName, string connectionId);

        /// <summary>
        /// Убрать подлючение у пользователя
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="connectionId"></param>
        User Remove(string connectionId);

        /// <summary>
        /// Получить 
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="connectionId"></param>
        IEnumerable<string> GetConnections(string nickName);

        /// <summary>
        /// Получить 
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="connectionId"></param>
        IEnumerable<User> GetUsers();

        /// <summary>
        /// Получить пользователя по соединению
        /// </summary>
        /// <param name="connectionID"></param>
        /// <returns></returns>
        User GetUser(string connectionID);
    }
}
