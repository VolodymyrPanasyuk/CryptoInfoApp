using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoInfoApp.Core
{
    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;
        private readonly Action<Exception> _onException;

        public event EventHandler CanExecuteChanged;

        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute = null, Action<Exception> onException = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            _onException = onException;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public async void Execute(object parameter)
        {
            try
            {
                await _execute();
            }
            catch (Exception ex)
            {
                _onException?.Invoke(ex);
            }
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}