﻿using Interfaces;
using NET.efilnukefesin.Extensions.Wpf.Commands;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using WPF.Shared.ViewModels;

namespace AdminApp.ViewModels
{
    [Locator("ViewPermissionsViewModel")]
    internal class ViewPermissionsViewModel : BaseViewAndEditViewModel
    {
        #region Properties

        public string PermissionName { get; set; }
        public ObservableCollection<Permission> Permissions { get; set; }

        public ICommand AddCommand { get; set; }

        private PermissionServer.SDK.Client client;

        #endregion Properties

        #region Construction

        public ViewPermissionsViewModel(IMessageBroker MessageBroker, PermissionServer.SDK.Client client, BaseViewModel Parent = null)
            : base(MessageBroker, Parent)
        {
            this.client = client;
            this.Permissions = new ObservableCollection<Permission>();
        }

        #endregion Construction

        #region Methods

        #region setupCommands
        protected override void setupCommands()
        {
            this.LoadedCommand = new RelayCommand(this.loadedCommandExecute, this.loadedCommandCanExecute);
            this.AddCommand = new ParameterRelayCommand<string>(this.addCommandExecute, this.addCommandCanExecute);
        }
        #endregion setupCommands

        #region addCommandCanExecute
        private bool addCommandCanExecute(string obj)
        {
            return this.client.May("EditPermissions"); ;
        }
        #endregion addCommandCanExecute

        #region addCommandExecute
        private async void addCommandExecute(string newPermissionName)
        {
            Permission newPermission = new Permission(newPermissionName);
            bool hasSuccessfullyAdded = await this.client.AddPermissionAsync(newPermission);
            if (hasSuccessfullyAdded)
            {
                //Yay!
                this.SendMessage("Message", new SimpleResult<string>("Added Permission successfully"));
                //Update List
                this.renewPermissions();
                //delete TextBox text
                this.PermissionName = string.Empty;
            }
            else
            {
                //Naye!
                this.SendMessage("Message", new SimpleResult<string>(new ErrorInfo(1, "Could not add Permission!", "perhaps you are using an already existing name?")));
            }
        }
        #endregion addCommandExecute

        #region loadedCommandCanExecute
        private bool loadedCommandCanExecute()
        {
            return true;
        }
        #endregion loadedCommandCanExecute

        #region loadedCommandExecute
        private async void loadedCommandExecute()
        {
            this.renewPermissions();
            this.MayEdit = this.client.May("EditPermissions");
        }
        #endregion loadedCommandExecute

        #region renewPermissions: fetches the Permission list from the server and copies it to the local version
        /// <summary>
        /// fetches the Permission list from the server and copies it to the local version
        /// </summary>
        private async void renewPermissions()
        {
            IEnumerable<Permission> permissions = await this.client.GetAllPermissionsAsync();
            if (permissions != null)
            {
                this.Permissions = new ObservableCollection<Permission>(permissions);
            }
        }
        #endregion renewPermissions

        #region dispose
        protected override void dispose()
        {
            this.Permissions.Clear();
            this.Permissions = null;
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
