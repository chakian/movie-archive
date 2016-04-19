using System.Collections.Generic;
using System.Linq;

namespace MArchive.Domain.Message
{
    public class BaseResponseMessage
    {
        public List<Message> Messages { get; set; }
        public bool IsSuccessful { get { return Messages.Any(q => q.Type == MessageType.Error) == false; } }

        private void Add(Message message)
        {
            if (Messages == null) Messages = new List<Message>();
            Messages.Add(message);
        }
        public void AddError(MessageCodes messageCode)
        {
            Message message = new Message
            {
                Code = messageCode,
                Type = MessageType.Error
            };
            Add(message);
        }
        public void AddInformation(MessageCodes messageCode)
        {
            Message message = new Message
            {
                Code = messageCode,
                Type = MessageType.Information
            };
            Add(message);
        }
    }

    public class Message
    {
        public MessageCodes Code { get; set; }
        public MessageType Type { get; set; }
        public string Data { get; set; }
    }

    public enum MessageType
    {
        Information,
        Error
    }
}
