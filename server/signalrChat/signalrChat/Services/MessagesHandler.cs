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
    public class MessagesHandler : IMessagesHandler
    {
        private readonly List<Message> messages = new List<Message>();

        /// <summary>
        /// Добавить сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        public Message Add(Message message)
        {
            messages.Add(message);

            return message;
        }

        /// <summary>
        /// Получить все сообщения для пользователя с ником
        /// </summary>
        /// <param name="nickName">Никнейм пользователя</param>
        public IEnumerable<Message> GetAll(string nickName)
        {
            return messages.Where(x => !string.IsNullOrEmpty(x.DestNickName) && string.Equals(x.SourceNickName, nickName, StringComparison.OrdinalIgnoreCase) || 
                                       !string.IsNullOrEmpty(x.DestNickName) && string.Equals(x.DestNickName, nickName, StringComparison.OrdinalIgnoreCase) ||
                                       string.IsNullOrEmpty(x.DestNickName));
        }

        /// <summary>
        /// Получить переписку пользователей
        /// </summary>
        /// <param name="sourceName">отправитель</param>
        /// <param name="destName">получатель</param>
        public IEnumerable<Message> GetDialog(string sourceName, string destName)
        {
            return messages.Where(x => string.Equals(x.DestNickName, destName, StringComparison.OrdinalIgnoreCase) && string.Equals(x.SourceNickName, sourceName, StringComparison.OrdinalIgnoreCase) ||
                                       string.Equals(x.DestNickName, sourceName, StringComparison.OrdinalIgnoreCase) && string.Equals(x.SourceNickName, destName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Удалить сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        public Message Remove(Message message)
        {
            throw new NotImplementedException();
        }
    }

    public interface IMessagesHandler
    {
        /// <summary>
        /// Добавить сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        Message Add(Message message);

        /// <summary>
        /// Удалить сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        Message Remove(Message message);

        /// <summary>
        /// Получить все сообщения для пользователя с ником
        /// </summary>
        /// <param name="nickName">Никнейм пользователя</param>
        IEnumerable<Message> GetAll(string nickName);

        /// <summary>
        /// Получить переписку пользователей
        /// </summary>
        /// <param name="sourceName">отправитель</param>
        /// <param name="destName">получатель</param>
        IEnumerable<Message> GetDialog(string sourceName, string destName);
    }
}
