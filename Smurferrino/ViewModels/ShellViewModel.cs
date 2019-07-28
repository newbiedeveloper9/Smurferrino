using System.Threading.Tasks;
using Caliburn.Micro;
using Smurferrino.Business;
using Smurferrino.PublishSub;

namespace Smurferrino.ViewModels {
    public class ShellViewModel : PropertyChangedBase, IShell,
        IHandle<IClientReloadPub>
    {
        private readonly IEventAggregator _eventAggregator;
        private Core _businessLibrary;

        public MainViewModel MainViewModel { get; private set; }

        public ShellViewModel()
        {
            _eventAggregator = new EventAggregator();
            _eventAggregator.Subscribe(this);

            Task.Factory.StartNew(() => 
                _businessLibrary = Core.Initialize());

            MainViewModel = new MainViewModel(_eventAggregator);
        }

        public void Handle(IClientReloadPub message)
        {
            Task.Factory.StartNew(() =>
                _businessLibrary = Core.Initialize());
        }
    }
}