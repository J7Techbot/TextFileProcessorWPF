
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

        private bool _isProcessRunning;
        public bool IsProcessRunning { get => _isProcessRunning; set { _isProcessRunning = value; } }

        public void StopProcess()
        {
            cancellationTokenSource.Cancel();

            Debug.WriteLine("Process stoped!");
        }

        public async void ProcessAsync(string fileName, Action<int> progresAction)
        {
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            string[] words = File.ReadAllText(fileName).Split(' ');

            Dictionary<string, int> wordCounts = new Dictionary<string, int>();
            Debug.WriteLine(words.Length + "words");
            for (int i = 0; i < words.Length; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                if (wordCounts.ContainsKey(words[i]))
                {
                    wordCounts[words[i]]++;
                }
                else
                {
                    wordCounts.Add(words[i], 1);
                }

                Debug.WriteLine(i);

                await Task.Delay(100);

                int progress = (int)100 / words.Length;
                //Debug.WriteLine(progress);

                //float progress = ((float)i / words.Length * 100);
                progresAction.Invoke(progress);
            }

            //int progress = (int)((double)i / words.Length * 100);
            //progresAction.Invoke(progress);
            //progressReporter.Report(progress);
        }
    }
}
