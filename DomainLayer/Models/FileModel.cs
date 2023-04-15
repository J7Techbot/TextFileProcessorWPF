using DomainLayer.BusinessLogic;
using System;
using System.Collections.Generic;

namespace DomainLayer.Models
{
    public class FileModel : BaseModel
    {
        public FileProcessor FileProcessor { get; set; }

        public event EventHandler CanExecuteChanged;

        private Dictionary<string,int> _words;
        public Dictionary<string, int> Words { get => _words; set { _words = value; OnPropertyChanged(); } }

        private string _fileName;
        public string FileName { get => _fileName; set { _fileName = value; OnPropertyChanged(); } }


        private bool _isProcessActive;
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

        public FileModel(FileProcessor fileProcessor)
        {
            FileProcessor = fileProcessor; 

            FileProcessor.ProcessActiveStateChanged += (active) => IsProcessActive = active;
        }

        public async void ProcessFile()
        {
            Words = await FileProcessor.ProcessAsync(FileName);
        }

        public void StopProcess()
        {
            FileProcessor.StopProcess();
        }
    }
}
