using Interfaces;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using WPF.Shared.ViewModels;

namespace AdminApp.ViewModels
{
    [Locator("AddOwnedRoleToUserViewModel")]
    internal class AddOwnedRoleToUserViewModel : BaseViewModel
    {
        #region Properties

        public User SelectedUser { get; set; }

        #endregion Properties

        #region Construction

        public AddOwnedRoleToUserViewModel(IMessageBroker MessageBroker, BaseViewModel Parent = null) : base(MessageBroker, Parent)
        {
        }

        #endregion Construction

        #region Methods

        #region receiveMessage
        protected override bool receiveMessage(string Text, object Data)
        {
            bool result = false;
            if (Text.Equals("SelectedUser"))
            {
                this.SelectedUser = (User)Data;
                result = true;
            }
            return result;
        }
        #endregion receiveMessage

        #endregion Methods
    }
}
