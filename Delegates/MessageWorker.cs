using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureADPrototype.Delegates
{
    public class MessageWorker : BackgroundService
    {
        private readonly MessageGenerator Generator;

        public MessageWorker(MessageGenerator Generator)
        {
            this.Generator = Generator;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Start generating messages");

            Generator.Start();

            return Task.CompletedTask;
        }
    }        
}
