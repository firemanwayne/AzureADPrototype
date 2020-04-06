using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureADPrototype.Delegates
{
    public interface IMessageReceiver
    {
        void RegisterHandler(Func<MessageBase, CancellationToken, Task> Handler);        
    }

    public class MessageReceiver : IMessageReceiver
    {
        private readonly IMessageSender Sender;
        private Func<MessageBase, CancellationToken, Task> Handler;

        public MessageReceiver(IMessageSender Sender)
        {
            this.Sender = Sender;
        }

        public void RegisterHandler(Func<MessageBase, CancellationToken, Task> Handler)
        {
            this.Handler = Handler;
        }

        private Task MessageReceived(MessageBase Message, CancellationToken Token) 
            => Task.FromResult(Handler.Invoke(Message, Token));        
    }
}
