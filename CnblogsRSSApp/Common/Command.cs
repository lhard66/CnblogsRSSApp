using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CnblogsRSSApp.Common
{
    /// <summary>
    /// 对Command事件进行封装
    /// </summary>
    public class Command : ICommand
    {
        private Func<object, bool> _canExecute;
        private Action<object> _execute;

        public event EventHandler CanExecuteChanged;

        public Command(Func<object, bool> canExecute, Action<object> execute)
        {
            _canExecute = canExecute;
            _execute = execute;

        }
        public Command(Action<object> execute) : this(p => true, execute)
        {

        }

        public void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
