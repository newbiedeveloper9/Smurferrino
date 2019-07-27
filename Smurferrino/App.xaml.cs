namespace Smurferrino
{
    using System.Windows;

    public partial class App : Application
    {
        public App()
        {
            new ExecuteOnStart();
            InitializeComponent();
        }
    }

}
