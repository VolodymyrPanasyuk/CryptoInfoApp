using System.Globalization;
using System.Threading;
using System.Windows;

namespace CryptoInfoApp.Core
{
    public static class SetupWindow
    {
        public static void Setup(Window window)
        {
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            window.MinHeight = SystemParameters.WorkArea.Height / 2.5;
            window.MinWidth = SystemParameters.WorkArea.Width / 2.5;
            window.MaxHeight = SystemParameters.WorkArea.Height + 7;
            ThemeSwitcher.SetDefault(window);
        }
    }
}