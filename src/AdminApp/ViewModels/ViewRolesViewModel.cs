using Interfaces;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminApp.ViewModels
{
    [Locator("ViewRolesViewModel")]
    internal class ViewRolesViewModel : BaseViewModel
    {
        #region Properties

        #endregion Properties

        #region Construction

        public ViewRolesViewModel(IMessageBroker MessageBroker, BaseViewModel Parent = null)
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
