using Caliburn.Micro;

namespace Smurferrino.ViewModels {
    public class ShellViewModel : PropertyChangedBase, IShell
    {
        private IEventAggregator _eventAggregator;

        public MainViewModel MainViewModel { get; private set; }

        public ShellViewModel()
        {
            _eventAggregator = new EventAggregator();
            _eventAggregator.Subscribe(this);

            MainViewModel = new MainViewModel(_eventAggregator);
        }
    }
}