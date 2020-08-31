using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace signalrChat.Models
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class User
    {
        public User()
        {
        }

        public User(string nickName)
        {
            NickName = nickName;
        }

        public User(string nickName, string connectionID)
        {
            NickName = nickName;
            IDs = new HashSet<string>();
            IDs.Add(connectionID);
        }

        public User(string nickName, string connectionID, int avatarID)
        {
            NickName = nickName;
            IDs = new HashSet<string>();
            IDs.Add(connectionID);
            AvatarID = avatarID;
        }

        public User(string nickName, UserStates state, DateTimeOffset lastVizit, int avatarID)
        {
            NickName = nickName;
            State = state;
            LastVizit = lastVizit;
            AvatarID = avatarID;
        }

        /// <summary>
        /// Ник
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Идентификаторы подключения SignalR
        /// </summary>
        public HashSet<string> IDs { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public UserStates State { get; set; }

        /// <summary>
        /// Дата дисконекта последнего
        /// </summary>
        public DateTimeOffset LastVizit { get; set; }

        /// <summary>
        /// ID аватара
        /// </summary>
        public int AvatarID { get; set; }
    }

    public enum UserStates: int
    {
        /// <summary>
        /// В сети
        /// </summary>
        Online = 0,

        /// <summary>
        /// Не в сети
        /// </summary>
        Offline = 1
    }
}
