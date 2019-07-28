﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.Win32;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Players;
using Smurferrino.Models;
using Smurferrino.PublishSub;
using Smurferrino.Serialize;

namespace Smurferrino.ViewModels
{
    public class MainViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;

        #region Properties
        private TriggerModel _trigger;
        public TriggerModel Trigger
        {
            get => _trigger;
            set
            {
                if (_trigger == value) return;
                _trigger = value;
                NotifyOfPropertyChange(() => Trigger);
            }
        }

        private BunnyModel _bunny;
        public BunnyModel Bunny
        {
            get => _bunny;
            set
            {
                if (_bunny == value) return;
                _bunny = value;
                NotifyOfPropertyChange(() => Bunny);
            }
        }


        #endregion

        #region Properties
        private ProcessState _gameState;
        public ProcessState GameState
        {
            get => _gameState;
            set
            {
                if (_gameState == value) return;
                _gameState = value;
                NotifyOfPropertyChange(() => GameState);

                if (value == ProcessState.Attached || value == ProcessState.Null)
                {
                    CanClientReload = true;
                }
                else
                {
                    CanClientReload = false;
                }

                NotifyOfPropertyChange(() => CanClientReload);
            }
        }

        private bool _canClientReload;
        public bool CanClientReload
        {
            get => _canClientReload;
            set
            {
                if (_canClientReload == value) return;
                _canClientReload = value;
                NotifyOfPropertyChange(() => CanClientReload);
            }
        }

        #endregion

        #region .ctor
        public MainViewModel()
        {
            Initialize();
        }

        public MainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            Initialize();
        }
        #endregion

        private void Initialize()
        {
            Trigger = (TriggerModel)FunctionModelSingleton.Instance.FunctionModels.GetByFunctionName("Trigger").Model;
            Bunny = (BunnyModel)FunctionModelSingleton.Instance.FunctionModels.GetByFunctionName("Bunny").Model;

            ProcessStateClass.ProcessStateChanged += (sender, e) => GameState = Global.ProcessState;
        }

        public void LoadConfig()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Config|*.json";
            openFileDialog.InitialDirectory = FilePaths.JsonDirectoryPath;

            if (openFileDialog.ShowDialog() == true)
            {
                if (openFileDialog.CheckFileExists)
                {
                    var fileName = Path.GetFileName(openFileDialog.FileName);

                    Trigger = (TriggerModel) Trigger.LoadModel(fileName);
                    Bunny = (BunnyModel) Bunny.LoadModel(fileName);
                }
            }
        }

        public void SaveConfig()
        {
            Trigger.SaveModelRAM();
            Bunny.SaveModelRAM();

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Config|*.json";
            saveFileDialog.InitialDirectory = FilePaths.JsonDirectoryPath;

            if (saveFileDialog.ShowDialog() == true)
            {
                var fileName = Path.GetFileName(saveFileDialog.FileName);
                if (!string.IsNullOrWhiteSpace(fileName))
                    FunctionModelSingleton.Instance.FunctionModels.SaveToFileByRAM(fileName);
            }
        }

        public void ClientReload()
        {
            _eventAggregator.PublishOnUIThread(
                new ClientReloadPub());
        }
    }
}
