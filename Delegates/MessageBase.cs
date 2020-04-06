using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureADPrototype.Delegates
{
    public abstract class MessageBase
    {
        protected MessageBase()
        {
            MessageId = Guid.NewGuid().ToString();
            Created = DateTime.UtcNow;
        }

        public DateTime Created { get; }
        public string MessageId { get; }
    }
}
