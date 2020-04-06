using AzureADPrototype.Delegates;
using System;

namespace AzureADPrototype.StateProviders.Notifications
{
    public class NotificationRecievedEventArgs : EventArgs
    {
        public TestMessage Message { get; }
        public NotificationRecievedEventArgs(TestMessage Message)
        {
            this.Message = Message;
        }
    }
}
