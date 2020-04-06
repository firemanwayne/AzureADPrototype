using System.Threading.Tasks;

namespace AzureADPrototype.Delegates
{
    public interface IMessageSender
    {
        Task SendAsync(MessageBase Message);
    }

    public class MessageSender : IMessageSender
    {

        public MessageSender()
        {

        }

        public Task SendAsync(MessageBase Message)
        {
            throw new System.NotImplementedException();
        }
    }
}
