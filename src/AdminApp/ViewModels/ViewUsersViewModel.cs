using Interfaces;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using WPF.Shared.ViewModels;

namespace AdminApp.ViewModels
{
    [Locator("ViewUsersViewModel")]
    internal class ViewUsersViewModel : BaseViewModel
    {
        #region Properties

        #endregion Properties

        #region Construction

        public ViewUsersViewModel(IMessageBroker MessageBroker, BaseViewModel Parent = null)
            : base(MessageBroker, Parent)
        {

        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            
        }
        #endregion dispose

        #region receiveMessage
        protected override bool receiveMessage(string Text, object Data)
        {
            return false;
        }
        #endregion receiveMessage

        #endregion Methods
    }
}
