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

        #endregion Methods
    }
}
