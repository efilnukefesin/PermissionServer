using System;
using System.Collections.Generic;
using System.Text;
using Interfaces;

namespace WPF.Shared.ViewModels
{
    public abstract class BaseWindowViewModel : BaseViewModel
    {
        #region Properties

        public string WindowTitle { get; set; }

        #endregion Properties

        #region Construction

        public BaseWindowViewModel(IMessageBroker MessageBroker, BaseViewModel Parent = null) : base(MessageBroker, Parent)
        {
        }

        #endregion Construction

        #region Methods

        #region ExecuteOnUIThread
        public void ExecuteOnUIThread(Action action)
        {
            if (System.Windows.Application.Current != null)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(action);
            }
            else
            {
                action.Invoke();
            }
        }
        #endregion ExecuteOnUIThread

        #endregion Methods
    }
}
