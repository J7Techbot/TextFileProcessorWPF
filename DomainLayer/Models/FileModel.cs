

using DomainLayer.BusinessLogic;
using System.Diagnostics;

namespace DomainLayer.Models
{
    public class FileModel : BaseModel
    {
        private TextProcessor _textProcessor;

        private string _fileName = "";
        public string FileName { get => _fileName; set { _fileName = value; OnPropertyChanged(); } }

        private bool _isProcessActive = false;        
        public bool IsProcessActive { get => _isProcessActive; set { _isProcessActive = value; OnPropertyChanged(); } }

        private int _progress;
        public int Progress { get => _progress; set { _progress = value; OnPropertyChanged(); } }

        public FileModel()
        {
            _textProcessor = new TextProcessor();
        }
        public void ProcessFile()
        {
            Progress = 0;

            IsProcessActive = true;

            _textProcessor.ProcessAsync(FileName, progres => { Progress += progres; Debug.WriteLine(Progress); });
        }
        public void StopProcess()
        {
            IsProcessActive = false;

            _textProcessor.StopProcess();
        }
    }
}
