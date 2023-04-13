
using DomainLayer.Models;
using ViewLayer.Helpers;
using ViewLayer.Shared;

namespace ViewLayer.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private FileModel fileModel;
        public FileModel FileModel { get => fileModel; set { fileModel = value; OnPropertyChanged(); } }
        public RelayCommand SelectFileCommand { get; set; }
        public RelayCommand StartProcessCommand { get; set; }
        public RelayCommand CancelProcessCommand { get; set; }

        public MainViewModel(DomainLayer.BusinessLogic.TextProcessor textProcessor)
        {            
            SelectFileCommand = new RelayCommand((param) =>
            {
                FileModel = new FileModel() { FileName = FileSystemHelper.GetFileName() };
            });

            StartProcessCommand = new RelayCommand((param) =>
            {
               textProcessor.ProcessAsync();
            }, param => FileModel != null);

            CancelProcessCommand = new RelayCommand((param) =>
            {
                textProcessor.StopProcess();
            });
        }
    }
}
