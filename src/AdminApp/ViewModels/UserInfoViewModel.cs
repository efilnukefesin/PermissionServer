﻿using Interfaces;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WPF.Shared.ViewModels;

namespace AdminApp.ViewModels
{
    [Locator("UserInfoViewModel")]
    internal class UserInfoViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<Permission> Permissions { get; set; }
        public ObservableCollection<UserValue> UserValues { get; set; }

        private PermissionServer.SDK.Client client;

        #endregion Properties

        #region Construction

        public UserInfoViewModel(IMessageBroker MessageBroker, PermissionServer.SDK.Client client, BaseViewModel Parent = null)
            : base(MessageBroker, Parent)
        {
            this.client = client;
            this.Permissions = new ObservableCollection<Permission>();
            this.UserValues = new ObservableCollection<UserValue>();
            this.client.PermissionsUpdated += client_PermissionsUpdated;
            this.client.UserValuesUpdated += client_UserValuesUpdated;
        }
        #endregion Construction

        #region Methods

        #region client_PermissionsUpdated
        private async void client_PermissionsUpdated(object sender, EventArgs e)
        {
            if (this.client.HasPermissions())
            {
                this.Permissions = new ObservableCollection<Permission>(await this.client.GetGivenPermissionsAsync());
            }
        }
        #endregion client_PermissionsUpdated

        #region client_UserValuesUpdated
        private async void client_UserValuesUpdated(object sender, EventArgs e)
        {
            if (this.client.HasUserValues())
            {
                this.UserValues = new ObservableCollection<UserValue>(await this.client.GetUserValuesAsync());
            }
        }
        #endregion client_UserValuesUpdated

        #region dispose
        protected override void dispose()
        {
            this.client.PermissionsUpdated -= client_PermissionsUpdated;
            this.client.UserValuesUpdated -= client_UserValuesUpdated;
            this.client = null;
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
