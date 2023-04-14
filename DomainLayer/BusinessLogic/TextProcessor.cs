
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DomainLayer.BusinessLogic
{
    public class TextProcessor
    {
        CancellationTokenSource cancellationTokenSource;
        CancellationToken cancellationToken;

        public Action<bool> _processStateActive;

        public TextProcessor(Action<bool> processStateChanged)
        {
            _processStateActive = processStateChanged;
        }

        public void StopProcess()
        {
            cancellationTokenSource.Cancel();

            Debug.WriteLine("Process stoped!");
        }

        public async void ProcessAsync(string fileName, Action<double> progressAction)
        {
            _processStateActive.Invoke(true);

            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            string[] words = File.ReadAllText(fileName).Split(' ');

            Dictionary<string, int> wordCounts = new Dictionary<string, int>();

            for (int i = 0; i < words.Length; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                if (wordCounts.ContainsKey(words[i]))                
                    wordCounts[words[i]]++;                
                else                
                    wordCounts.Add(words[i], 1);
                

                Debug.WriteLine(i);

                await Task.Delay(10);

                double progress = (double)100 / words.Length ;
                progressAction.Invoke(progress);
            }

            _processStateActive.Invoke(false);
        }
    }
}
