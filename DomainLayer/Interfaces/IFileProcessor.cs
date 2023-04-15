using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interfaces
{
    public interface IFileProcessor
    {
        /// <value>Represent progres of parce process.</value>
        public float Progress { get; set; }

        /// <value>Invoked when state of proces changed.</value>
        public Action<bool> ProcessActiveStateChanged { get; set; }

        /// <summary>
        /// Process the file and return all words contained in it as a dictionary, where the key is the word and the value is the count of occurrences in the file.
        /// </summary>
        /// <param name="fileName">Path to file</param>
        /// <param name="delimiters">Defines the criterion by which words will be divided.</param>
        /// <returns>Dictionary with words and occurrences.If file canot be readed, return null.</returns>
        public Task<Dictionary<string, int>> ProcessFileAsync(string fileName, char[] delimiters);

        /// <summary>
        /// Terminate the process.
        /// </summary>
        public void StopProcess();        
    }
}
