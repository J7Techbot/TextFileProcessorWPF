using DomainLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainLayer.Models
{
    public class FileModel : BaseModel
    {
        private readonly ILocalizationService _localizationService;

        /// <value>Contains parse logic.</value>
        public IFileProcessor FileProcessor { get; set; }
        
        /// <value>Informs viewModel that commands needs to be updated.</value>
        public event EventHandler CanExecuteChanged;

        /// <value>Item source of datagrid view.Contains words of parsed file.</value>
        public Dictionary<string, int> Data { get => _data; set { _data = value; OnPropertyChanged(); } }
        private Dictionary<string, int> _data;

        /// <value>Path to file.</value>
        public string FileName { get => _fileName; set { _fileName = value; OnPropertyChanged(); } }
        private string _fileName;

        /// <value>Propagate info massage to user.</value>
        public string InfoPanel { get => _infoPanel; set { _infoPanel = value; OnPropertyChanged(); } }
        private string _infoPanel;

        /// <value>Represents state of parsing process. If the process is running, it returns true.</value>
        public bool IsProcessActive
        {
            get => _isProcessActive;
            set
            {
                _isProcessActive = value;
                OnPropertyChanged();
                CanExecuteChanged(this, new EventArgs());
            }
        }
        private bool _isProcessActive;

        /// <summary>
        /// Initialize model
        /// </summary>
        /// <param name="fileProcessor"></param>
        public FileModel(IFileProcessor fileProcessor, ILocalizationService localizationService)
        {
            FileProcessor = fileProcessor;

            FileProcessor.ProcessActiveStateChanged += (active) => IsProcessActive = active;

            _localizationService = localizationService;
        }

        /// <summary>
        /// Starts the file parsing process.
        /// </summary>
        public async void ProcessFile()
        {
            InfoPanel = FileName;

            Data = await FileProcessor.ProcessFileAsync(FileName, new char[] { ' ', '\r', '\n' });
            
            Data = Data.OrderBy(kvp => kvp.Value).ThenBy(kvp=>kvp.Key).ToDictionary(kvp => kvp.Key, kvp=> kvp.Value);

            if (Data.Count == 0)
                InfoPanel = _localizationService.GetValue("FileReadFailed"); 
        }

        /// <summary>
        /// Terminate the file parsing process.
        /// </summary>
        public void StopProcess()
        {
            FileProcessor.StopProcess();
        }
    }
}
