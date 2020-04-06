using System;

namespace AzureADPrototype.Delegates
{
    public class TestMessage : MessageBase
    {
        public TestMessage() : base()
        {
            
        }

        public string Content { get; set; }
        public string Subject { get; set; }
    }
}
