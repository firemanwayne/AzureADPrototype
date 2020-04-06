using System;

namespace AzureADPrototype.Delegates
{
    public class MessageGeneratedEventArgs : EventArgs
    {
        public TestMessage Message { get; }

        public MessageGeneratedEventArgs(TestMessage Message)
        {
            this.Message = Message;
        }
    }
}
