using System;

namespace signalrChat.Models
{
    /// <summary>
    /// ������ ���������
    /// </summary>
    public class Message
    {
        /// <summary>
        /// ����� ���������
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// ����� ����������
        /// </summary>
        public DateTimeOffset SendDate { get; set; }

        /// <summary>
        /// ID �����������
        /// </summary>
        public string SourceNickName { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public MessageStates State { get; set; }

        /// <summary>
        /// ��� ����������(���� ��� �� � ����� ���)
        /// </summary>
        public string DestNickName { get; set; }
    }

    public enum MessageStates: int
    {
        /// <summary>
        /// ����������
        /// </summary>
        Sent = 0,

        /// <summary>
        /// ��������
        /// </summary>
        Received = 1,

        /// <summary>
        /// ������
        /// </summary>
        Error = 255,
    }
}
