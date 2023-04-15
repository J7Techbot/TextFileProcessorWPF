
using DomainLayer.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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

            FileModel.CanExecuteChanged += OnCanExecuteChanged;

            SelectFileCommand = new RelayCommand((param) =>
            {
                FileModel.FileName = FileSystemHelper.GetFileName("Text file(*.txt)|*.txt");
                Debug.WriteLine(FileModel?.Words?.Count);
            },param => !FileModel.IsProcessActive);

            StartProcessCommand = new RelayCommand((param) =>
            {
               FileModel.ProcessFile();
            }, param => !string.IsNullOrEmpty(FileModel.FileName) && !FileModel.IsProcessActive);

            CancelProcessCommand = new RelayCommand((param) =>
            {
                FileModel.StopProcess();
            }, param => !string.IsNullOrEmpty(FileModel.FileName) && FileModel.IsProcessActive);
        }

        private void OnCanExecuteChanged(object sender, EventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
