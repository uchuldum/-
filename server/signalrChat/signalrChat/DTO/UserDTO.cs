using signalrChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace signalrChat.DTO
{
    public class UserDTO
    {
        /// <summary>
        /// Ник
        /// </summary>
        public string NickName { get; set; }

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
}
