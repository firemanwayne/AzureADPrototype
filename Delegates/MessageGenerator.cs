using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureADPrototype.Delegates
{
    public class MessageGenerator
    {
        private event EventHandler OnStopCalled;
        private event EventHandler OnStartCalled;

        public event EventHandler<MessageGeneratedEventArgs> OnMessageGenerated;

        public bool IsGeneratingMessages { get; private set; }
        public CancellationToken Token { get; private set; }

        public MessageGenerator()
        {
            OnStartCalled += MessageGenerator_OnStartCalled;
            OnStopCalled += MessageGenerator_OnStopCalled;
        }

        public void Start() => OnStartCalled?.Invoke(this, new EventArgs());
        public void Stop() => OnStopCalled?.Invoke(this, new EventArgs());

        private void MessageGenerator_OnStopCalled(object sender, EventArgs e)
        {
            IsGeneratingMessages = false;

            var message = new TestMessage()
            {
                Content = "Stop Message",
                Subject = "Messages have been stopped"
            };

            OnMessageGenerated?.Invoke(this, new MessageGeneratedEventArgs(message));
        }

        private async void MessageGenerator_OnStartCalled(object sender, EventArgs e)
        {
            if (!IsGeneratingMessages)
            {
                Token = new CancellationToken();
                Token.Register(Stop);

                await Generate();

                IsGeneratingMessages = true;
            }
        }

        private async Task Generate()
        {
            while (!Token.IsCancellationRequested)
            {
                var message = new TestMessage()
                {
                    Content = "Test message generated",
                    Subject = "Test Message"
                };

                OnMessageGenerated?.Invoke(this, new MessageGeneratedEventArgs(message));

                await Task.Delay(2000);
            }
        }                  
    }
}
