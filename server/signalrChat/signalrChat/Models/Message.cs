using System;

namespace signalrChat.Models
{
    /// <summary>
    /// Модель сообщения
    /// </summary>
    public class Message
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

    public enum MessageStates: int
    {
        /// <summary>
        /// Отправлена
        /// </summary>
        Sent = 0,

        /// <summary>
        /// Получена
        /// </summary>
        Received = 1,

        /// <summary>
        /// Ошибка
        /// </summary>
        Error = 255,
    }
}
