using AzureADPrototype.Delegates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AzureADPrototype.StateProviders.Notifications
{
    public class NotificationState : INotifyPropertyChanged
    {
        private int messageCount;
        private bool recieveMessages = true;
        public bool RecieveMessages
        {
            get => recieveMessages;
            private set 
            {
                recieveMessages = value;
                OnPropertyChanged();
            }
        }

        public void StopMessages() => RecieveMessages = false;
        public void StartMessages() => RecieveMessages = true;

        public event EventHandler<NotificationRecievedEventArgs> OnMessageRecieved;

        public event PropertyChangedEventHandler PropertyChanged;

        public int MessageCount 
        {
            get => messageCount;
            
            private set
            {
                messageCount = value;
                OnPropertyChanged();
            }
        }

        public IList<TestMessage> Messages { get; } = new List<TestMessage>();

        public void AddMessage(TestMessage message)
        {
            if (RecieveMessages)
            {
                Messages.Add(message);
                OnMessageRecieved?.Invoke(this, new NotificationRecievedEventArgs(message));

                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = "")
        {
            messageCount = Messages.Count();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
