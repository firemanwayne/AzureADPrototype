using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureADPrototype.Delegates
{
    public interface ITestClient
    {
        void RegisterHandler(Func<TestMessage, CancellationToken, Task> Handler);

        void Cancel();
        void Restart();
    }
}
