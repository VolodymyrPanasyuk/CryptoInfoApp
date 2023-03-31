using CryptoInfoApp.Core;
using System.Windows;

namespace CryptoInfoApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DispatcherHelper.Dispatcher = Current.Dispatcher;
        }
    }
}