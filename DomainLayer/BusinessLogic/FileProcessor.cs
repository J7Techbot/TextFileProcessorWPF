using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace DomainLayer.BusinessLogic
{
    public class FileProcessor : INotifyPropertyChanged
    {
        CancellationTokenSource _cancellationTokenSource;
        CancellationToken _cancellationToken;

        private float progress;
        public float Progress { get => progress; set { progress = value; OnPropertyChanged(); } }

        public Action<bool> ProcessActiveStateChanged;

        public void StopProcess()
        {
            _cancellationTokenSource.Cancel();

            ProcessActiveStateChanged.Invoke(false);

            Debug.WriteLine("Process stoped!");
        }

        public async Task<Dictionary<string, int>> ProcessAsync(string fileName)
        {            
            ProcessActiveStateChanged.Invoke(true);

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            var wordsCount = await Task.Run(() => Parse(fileName));

            ProcessActiveStateChanged.Invoke(false);

            return wordsCount.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
        public ConcurrentDictionary<string, int> Parse(string fileName)
        {
            Progress = 0;

            string[] words;

            try
            {
                words = File.ReadAllText(fileName).Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch
            {
                return null;
            }

            var wordsCount = new ConcurrentDictionary<string, int>();

            ParallelLoopResult loopResult = Parallel.ForEach(words, word =>
            {
                if (!_cancellationToken.IsCancellationRequested)
                {
                    wordsCount.AddOrUpdate(word, 1, (key, oldValue) => oldValue + 1);

                    Progress += (float)100 / words.Length;
                }
            });

            var or = words.Length;
            var v = wordsCount.Values.Sum();

            return wordsCount;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
