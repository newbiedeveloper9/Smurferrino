using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Smurferrino.Models;

namespace Smurferrino.ViewModels
{
    public class MainViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;
        public TriggerModel Trigger { get; private set; }
        public BunnyModel Bunny { get; private set; }

        public MainViewModel()
        {
            Initialize();
        }

        public MainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            Initialize();

            Trigger.Enabled = true;
        }

        private void Initialize()
        {
            Trigger = (TriggerModel)FunctionModelSingleton.Instance.FunctionModels.GetByFunctionName("Trigger").Model;
            Bunny = (BunnyModel)FunctionModelSingleton.Instance.FunctionModels.GetByFunctionName("Bunny").Model;
        }
    }
}
