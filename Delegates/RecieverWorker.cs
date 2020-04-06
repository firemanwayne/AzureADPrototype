using AzureADPrototype.StateProviders.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureADPrototype.Delegates
{
    public class RecieverWorker : BackgroundService
    {
        private ITestClient Client;
        private readonly NotificationState State;
        private readonly IServiceScopeFactory Factory;

        public RecieverWorker(
            NotificationState State,
            IServiceScopeFactory Factory)
        {
            this.State = State;
            this.Factory = Factory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var Scope = Factory.CreateScope();
            Client = Scope.ServiceProvider.GetRequiredService<ITestClient>();

            Console.WriteLine("Start Listening for Messages");

            Client.RegisterHandler(
                (message, token) =>
                {                    
                    State.AddMessage(message);

                    return Task.CompletedTask;
                });

            return Task.CompletedTask;
        }
    }
}
