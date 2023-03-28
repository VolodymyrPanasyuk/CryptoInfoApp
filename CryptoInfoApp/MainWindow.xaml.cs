using CryptoInfoApp.Core;
using System.Windows;
using System.Windows.Input;

namespace CryptoInfoApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupWindow.Setup(this);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void SwitchThemeButton_Click(object sender, RoutedEventArgs e) => ThemeSwitcher.Switch(this);

        private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();

        private void MinimizeButton_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void MaximizeButton_Click(object sender, RoutedEventArgs e) => WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;

        private void Window_StateChanged(object sender, System.EventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                MainWindowBorder.CornerRadius = new CornerRadius(20);
                SwitchThemeBorder.CornerRadius = new CornerRadius(0, 0, 0, 20);
                MaximizeButton.Content = '\uE922';
            }
            else if (WindowState == WindowState.Maximized)
            {
                MainWindowBorder.CornerRadius = new CornerRadius(0);
                SwitchThemeBorder.CornerRadius = new CornerRadius(0);
                MaximizeButton.Content = '\uE923';
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }
    }
}