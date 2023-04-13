
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DomainLayer.BusinessLogic
{
    public class TextProcessor
    {
        CancellationTokenSource cancellationTokenSource;
        CancellationToken cancellationToken;

        public void StopProcess()
        {
            cancellationTokenSource.Cancel();

            Debug.WriteLine("Process stoped!");
        }

        public async void ProcessAsync()
        {
            Debug.WriteLine("Process starts!");

            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            for (int i = 0; i < 1000; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                Debug.WriteLine(i);

                await Task.Delay(1000);
            }
        }
    }
}
