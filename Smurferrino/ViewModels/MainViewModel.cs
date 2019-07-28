using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Helpers;
using Smurferrino.Models;

namespace Smurferrino.ViewModels
{
    public class MainViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;

        #region Models
        public TriggerModel Trigger { get; private set; }
        public BunnyModel Bunny { get; private set; }
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
            Global.ProcessState = ProcessState.Detached;
        }
    }
}
