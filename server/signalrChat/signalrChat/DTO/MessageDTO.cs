using signalrChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace signalrChat.DTO
{
    public class MessageDTO
    {
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Когда отправлена
        /// </summary>
        public DateTimeOffset SendDate { get; set; }

        /// <summary>
        /// ID отправителя
        /// </summary>
        public string SourceNickName { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public MessageStates State { get; set; }

        /// <summary>
        /// Ник получателя(если нет то в общий чат)
        /// </summary>
        public string DestNickName { get; set; }
    }
}
