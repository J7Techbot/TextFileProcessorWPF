﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using DomainLayer.Interfaces;

namespace DomainLayer.BusinessLogic
{
    public class FileProcessor : IFileProcessor, INotifyPropertyChanged
    {
        CancellationTokenSource _cancellationTokenSource;
        CancellationToken _cancellationToken;

        /// <value>Invoked when state of proces changed.</value>
        public Action<bool> ProcessActiveStateChanged { get; set; }

        /// <value>Represent progres of parse process.</value>
        public int Progress { get => _progress; private set => _progress = value; }
        private int _progress;

        /// <value>Represent count of words in processed file.</value>
        public int TotalWords { get => _totalWords; private set { _totalWords = value; OnPropertyChanged(); } }
        private int _totalWords = 1;

        /// <summary>
        /// Process the file and return all words contained in it as a dictionary, where the key is the word and the value is the count of occurrences in the file.
        /// </summary>
        /// <param name="fileName">Path to file</param>
        /// <param name="delimiters">Defines the criterion by which words will be divided.</param>
        /// <returns>Dictionary with words and occurrences.If file canot be readed, return null.</returns>
        public async Task<Dictionary<string, int>> ProcessFileAsync(string fileName, char[] delimiters)
        {
            ProcessActiveStateChanged.Invoke(true);

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            var wordsCount = await Task.Run(() => ParseFile(fileName, delimiters));

            ProcessActiveStateChanged.Invoke(false);

            return wordsCount;
        }
        
        /// <summary>
        /// Terminate the process.
        /// </summary>
        public void StopProcess()
        {
            _cancellationTokenSource.Cancel();            

            ProcessActiveStateChanged.Invoke(false);
        }

        /// <summary>
        /// Reads a file, splits it by a <paramref name="delimiters"/> into individual words, and returns them as a dictionary along with the count of each word found in the file.
        /// </summary>
        /// <param name="fileName">Path to file</param>
        /// <param name="delimiters">Defines the criterion by which words will be divided</param>
        /// <returns>Dictionary with words and occurrences. If file canot be readed, return empty Dictionary. </returns>
        private Dictionary<string, int> ParseFile(string fileName, char[] delimiters)
        {
            Progress = 0;

            string[] words;

            ConcurrentDictionary<string, int> wordsCount = new ConcurrentDictionary<string, int>();

            try
            {
                words = File.ReadAllText(fileName).Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            }
            catch
            {
                return new Dictionary<string, int>();
            }
           
            TotalWords = words.Count();

            Parallel.ForEach(words, word =>
            {
                if (!_cancellationToken.IsCancellationRequested)
                {
                    wordsCount.AddOrUpdate(word, 1, (key, oldValue) => oldValue + 1);

                    Interlocked.Increment(ref _progress);
                    OnPropertyChanged(nameof(Progress));
                }
                else
                    Progress = 0;
            });

            return wordsCount.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }



        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
