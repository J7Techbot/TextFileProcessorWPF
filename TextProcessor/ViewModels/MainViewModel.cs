
using DomainLayer.Models;
using ViewLayer.Helpers;
using ViewLayer.Shared;

namespace ViewLayer.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private FileModel _fileModel;
        public FileModel FileModel { get => _fileModel; set { _fileModel = value; OnPropertyChanged(); } }
        public RelayCommand SelectFileCommand { get; set; }
        public RelayCommand StartProcessCommand { get; set; }
        public RelayCommand CancelProcessCommand { get; set; }

        public MainViewModel(FileModel fileModel)
        {
            this.FileModel = fileModel;

            SelectFileCommand = new RelayCommand((param) =>
            {
                FileModel.FileName = FileSystemHelper.GetFileName();
            });

            StartProcessCommand = new RelayCommand((param) =>
            {
                FileModel.ProcessFile();
            }, param => !string.IsNullOrEmpty(FileModel.FileName) && !FileModel.IsProcessActive);

            CancelProcessCommand = new RelayCommand((param) =>
            {
                FileModel.StopProcess();
            }, param => !string.IsNullOrEmpty(FileModel.FileName) && FileModel.IsProcessActive);
        }
    }
}
