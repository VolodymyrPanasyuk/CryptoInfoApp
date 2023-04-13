using CryptoInfoApp.Core;
using CryptoInfoApp.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace CryptoInfoApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupWindow.Setup(this);
        }

        private void SwitchThemeButton_Click(object sender, RoutedEventArgs e) => ThemeSwitcher.Switch(this);

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();

            if (e.ClickCount == 2 && e.ChangedButton == MouseButton.Left)
                WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

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

        public void SetRadioButtonBasedOnCurrentVM(Type currentViewModelType)
        {
            if (currentViewModelType == typeof(HomeViewModel))
            {
                HomeRadioButton.IsChecked = true;
            }
            else if (currentViewModelType == typeof(CoinViewModel))
            {
                CoinRadioButton.IsChecked = true;
            }
            else if (currentViewModelType == typeof(SearchViewModel))
            {
                SearchRadioButton.IsChecked = true;
            }
            else if (currentViewModelType == typeof(ConvertViewModel))
            {
                ConvertRadioButton.IsChecked = true;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text != string.Empty && SearchTextBox.Text != "Search...")
            {
                var mainViewModel = DataContext as MainViewModel;
                mainViewModel.SearchViewWithInputCommand.Execute(SearchTextBox.Text);
                SetRadioButtonBasedOnCurrentVM(typeof(SearchViewModel));
                SearchTextBox.Text = "Search...";
            }
            else
            {
                Storyboard storyboard = new Storyboard();
                DoubleAnimationUsingKeyFrames animationX = new DoubleAnimationUsingKeyFrames();
                animationX.Duration = new Duration(TimeSpan.FromSeconds(1));

                animationX.KeyFrames.Add(new LinearDoubleKeyFrame(5, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
                animationX.KeyFrames.Add(new LinearDoubleKeyFrame(-10, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.2))));
                animationX.KeyFrames.Add(new LinearDoubleKeyFrame(10, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4))));
                animationX.KeyFrames.Add(new LinearDoubleKeyFrame(-10, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));
                animationX.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8))));

                Storyboard.SetTargetProperty(animationX, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
                Storyboard.SetTarget(animationX, SearchTextBox);

                storyboard.Children.Add(animationX);
                storyboard.Begin();
            }
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "Search...")
                SearchTextBox.Text = string.Empty;
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == string.Empty)
                SearchTextBox.Text = "Search...";
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Keyboard.ClearFocus();
                SearchButton_Click(sender, e);
                SearchButton.Focus();
            }
        }
    }
}