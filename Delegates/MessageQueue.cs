using System;
using System.Collections.Concurrent;

namespace AzureADPrototype.Delegates
{
    public class MessageQueue : IMessageQueue
    {
        private static ConcurrentDictionary<string, MessageBase> ReceivedMessages = new ConcurrentDictionary<string, MessageBase>();

        public MessageQueue()
        {

        }

        public event EventHandler<MessageReadEventArgs> OnMessageRead;
        public event EventHandler<MessageReceivedEventArgs> OnMessageReceived;        
        public event EventHandler<MessageExceptionEventArgs> OnExceptionThrown;

        public void MessageRead(string MessageId)
        {
            ReceivedMessages.TryRemove(MessageId, out var Result);

            if (Result == null)
                throw new MessageNotFoundException(MessageId);
        }
        private void MessageReceived()
        {

        }

    }

    public class MessageReceivedEventArgs : EventArgs
    {

    }
    public class MessageReadEventArgs : EventArgs
    {

    }
    public class MessageExceptionEventArgs : EventArgs
    {

    }

    public interface IMessageQueue
    {
        event EventHandler<MessageReadEventArgs> OnMessageRead;
        event EventHandler<MessageReceivedEventArgs> OnMessageReceived;        
        event EventHandler<MessageExceptionEventArgs> OnExceptionThrown;
    }

    public class MessageNotFoundException : Exception
    {
        public MessageNotFoundException(string MessageId) : base($"Message Id: {MessageId}. No message found with message Id") { }
    }
}
