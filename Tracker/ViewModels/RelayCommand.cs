using System.Windows.Input;

namespace Tracker.ViewModels
{
    internal sealed class RelayCommand : ICommand
    {
        public static readonly RelayCommand Empty = new(e => { }, e => true);

        public event EventHandler? CanExecuteChanged;
        public Action<object?> Code { get; set; }
        public Func<object?, bool> CanExecuteCode { get; set; }

        public RelayCommand(Action<object?> code, Func<object?, bool> canExecuteCode)
        {
            Code = code;
            CanExecuteCode = canExecuteCode;
        }

        public bool CanExecute(object? parameter)
        {
            return CanExecuteCode(parameter);
        }

        public void Execute(object? parameter)
        {
            Code(parameter);
        }
    }
}
