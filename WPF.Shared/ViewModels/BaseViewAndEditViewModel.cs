using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WPF.Shared.ViewModels
{
    public abstract class BaseViewAndEditViewModel : BaseViewModel
    {
        #region Properties

        public bool IsIdle { get; set; } = true;
        public bool MayEdit { get; set; } = false;

        public ICommand LoadedCommand { get; set; }

        #endregion Properties

        #region Construction

        public BaseViewAndEditViewModel(IMessageBroker MessageBroker, BaseViewModel Parent = null)
            : base(MessageBroker, Parent)
        {
            this.setupCommands();
        }

        #endregion Construction

        #region Methods

        protected abstract void setupCommands();

        #endregion Methods

        #region Events

        #endregion Events
    }
}
