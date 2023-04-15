﻿using DomainLayer.BusinessLogic;
using DomainLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace DomainLayer.Models
{
    public class MainModel : BaseModel
    {
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
        public MainModel(IFileProcessor fileProcessor)
        {
            FileProcessor = fileProcessor; 

            FileProcessor.ProcessActiveStateChanged += (active) => IsProcessActive = active;
        }

        /// <summary>
        /// Starts the file parsing process.
        /// </summary>
        public async void ProcessFile()
        {
            Data = await FileProcessor.ProcessFileAsync(FileName, new char[] { ' ', '\r', '\n' });
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