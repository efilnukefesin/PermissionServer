using Interfaces;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminApp.ViewModels
{
    [Locator("UserInfoViewModel")]
    internal class UserInfoViewModel : BaseViewModel
    {
        #region Properties

        #endregion Properties

        #region Construction

        public UserInfoViewModel(IMessageBroker MessageBroker, BaseViewModel Parent = null)
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
        protected override bool receiveMessage(string Text)
        {
            return false;
        }
        #endregion receiveMessage

        #endregion Methods
    }
}
