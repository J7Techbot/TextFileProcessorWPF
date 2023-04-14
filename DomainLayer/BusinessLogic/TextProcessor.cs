using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DomainLayer.BusinessLogic
{
    public class TextProcessor
    {
        CancellationTokenSource cancellationTokenSource;
        CancellationToken cancellationToken;

        public Action<bool> _processActiveStateChanged;

        public TextProcessor(Action<bool> processActiveStateChanged)
        {
            _processActiveStateChanged = processActiveStateChanged;
        }

        public void StopProcess()
        {
            cancellationTokenSource.Cancel();

            _processActiveStateChanged.Invoke(false);

            Debug.WriteLine("Process stoped!");
        }

        public async Task ProcessAsync(string fileName, Action<double> progressAction, Action<string> wordsOutput)
        {
            _processActiveStateChanged.Invoke(true);

            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            string[] words = File.ReadAllText(fileName).Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> wordCounts = new Dictionary<string, int>();

            for (int i = 0; i < words.Length; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                wordsOutput.Invoke(words[i]);

                Debug.WriteLine(i);

                await Task.Delay(10);

                double progress = (double)100 / words.Length;
                progressAction.Invoke(progress);
            }

            _processActiveStateChanged.Invoke(false);

            return;
        }
    }
}
