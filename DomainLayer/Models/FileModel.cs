

using DomainLayer.BusinessLogic;
using System;
using System.Diagnostics;

namespace DomainLayer.Models
{
    public class FileModel : BaseModel
    {
        private TextProcessor _textProcessor;

        public event EventHandler CanExecuteChanged;

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
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private double _progress;
        public double Progress { get => _progress; set { _progress = value; OnPropertyChanged(); } }

        public FileModel()
        {
            _textProcessor = new TextProcessor(active => IsProcessActive = active);
        }
        public void ProcessFile()
        {
            Progress = 0;

            _textProcessor.ProcessAsync(FileName, progres => { Progress += progres; });
        }
        public void StopProcess()
        {
            _textProcessor.StopProcess();
        }
    }
}
