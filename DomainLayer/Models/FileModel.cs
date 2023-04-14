

using DomainLayer.BusinessLogic;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class FileModel : BaseModel
    {
        private TextProcessor _textProcessor;

        public event EventHandler CanExecuteChanged;
       
        private ObservableCollection<WordWrapperModel> words = new ObservableCollection<WordWrapperModel>();
        public ObservableCollection<WordWrapperModel> Words { get => words; set { words = value; OnPropertyChanged(); } }

        private string _fileName;
        public string FileName { get => _fileName; set { _fileName = value; OnPropertyChanged(); } }

        private double _progress;
        public double Progress { get => _progress; set { _progress = value; OnPropertyChanged(); } }

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

        public FileModel()
        {
            _textProcessor = new TextProcessor(active => IsProcessActive = active);
        }
        public async void ProcessFile()
        {
            Progress = 0;

            await _textProcessor.ProcessAsync(FileName, progres => { Progress += progres; }, word => Words.Add(new WordWrapperModel() { Word = word }));
        }
        public void StopProcess()
        {
            _textProcessor.StopProcess();
        }
    }
}
