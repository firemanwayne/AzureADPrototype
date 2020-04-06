using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureADPrototype.Delegates
{
    public class TestClient : ITestClient
    {
        private readonly MessageGenerator Generator;
        private Func<TestMessage, CancellationToken, Task> Handler;

        public TestClient(MessageGenerator Generator)
        {
            this.Generator = Generator;
            Generator.OnMessageGenerated += Generator_OnMessageGenerated;           
        }        

        private void Generator_OnMessageGenerated(object sender, MessageGeneratedEventArgs e)
        {
            try
            {
                MessageRecieved(e.Message);
            }
            catch (Exception)
            {
                Cancel();
            }
        }

        public void RegisterHandler(Func<TestMessage, CancellationToken, Task> Handler)
        {
            this.Handler = Handler;
        }

        public void Restart() => Generator.OnMessageGenerated += Generator_OnMessageGenerated;
        public void Cancel() => Generator.OnMessageGenerated -= Generator_OnMessageGenerated;        

        public void MessageRecieved(TestMessage message)
        {
            Handler?.Invoke(message, new CancellationToken());
        }               
    }
}
