using System;
using System.Windows.Input;

namespace UserControlBaseWindow.ViewModels
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<object> _execute;

        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> execute) : this(execute, null) { }

        public RelayCommand(Action<object> execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
