
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
        /// <value>Holds properties and logic for view</value>
        public MainModel MainModel { get => _fileModel; set { _fileModel = value; OnPropertyChanged(); } }
        private MainModel _fileModel;

        /// <value>Open a dialog for selecting a file and save the selected path to the FilePath property.</value>
        public RelayCommand SelectFileCommand { get; set; }
        /// <value>Executes the file parsing operation and stores the parsed data in the model .</value>
        public RelayCommand StartProcessCommand { get; set; }
        /// <value>Terminate file parsing operation.</value>
        public RelayCommand CancelProcessCommand { get; set; }

        /// <summary>
        /// Initialize viewmodel
        /// </summary>
        /// <param name="mainModel"></param>
        public MainViewModel(MainModel mainModel)
        {
            this.MainModel = mainModel;

            MainModel.CanExecuteChanged += OnCanExecuteChanged;

            SelectFileCommand = new RelayCommand((param) =>
            {
                MainModel.FileName = FileSystemHelper.GetFileName("Text file(*.txt)|*.txt");

            },param => !MainModel.IsProcessActive);

            StartProcessCommand = new RelayCommand((param) =>
            {
               MainModel.ProcessFile();

            }, param => !string.IsNullOrEmpty(MainModel.FileName) && !MainModel.IsProcessActive);

            CancelProcessCommand = new RelayCommand((param) =>
            {
                MainModel.StopProcess();

            }, param => !string.IsNullOrEmpty(MainModel.FileName) && MainModel.IsProcessActive);
        }

        /// <summary>
        /// Resets CanExecute at commands.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCanExecuteChanged(object sender, EventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
