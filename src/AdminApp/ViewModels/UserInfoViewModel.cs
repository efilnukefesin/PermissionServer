using Interfaces;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AdminApp.ViewModels
{
    [Locator("UserInfoViewModel")]
    internal class UserInfoViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<Permission> Permissions { get; set; }

        private PermissionServer.SDK.Client client;

        #endregion Properties

        #region Construction

        public UserInfoViewModel(IMessageBroker MessageBroker, PermissionServer.SDK.Client client, BaseViewModel Parent = null)
            : base(MessageBroker, Parent)
        {
            this.client = client;
            //this.Permissions = new ObservableCollection<Permission>(this.client.GetGivenPermissionsAsync().GetAwaiter().GetResult());
            this.Permissions = new ObservableCollection<Permission>();
            this.client.PermissionsUpdated += client_PermissionsUpdated;

        }
        #endregion Construction

        #region Methods

        #region client_PermissionsUpdated
        private void client_PermissionsUpdated(object sender, EventArgs e)
        {
            this.Permissions = new ObservableCollection<Permission>(this.client.GetGivenPermissionsAsync().GetAwaiter().GetResult());
        }
        #endregion client_PermissionsUpdated

        #region dispose
        protected override void dispose()
        {
            this.client = null;
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
