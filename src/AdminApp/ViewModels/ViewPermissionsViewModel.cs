﻿using Interfaces;
using NET.efilnukefesin.Extensions.Wpf.Commands;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace AdminApp.ViewModels
{
    [Locator("ViewPermissionsViewModel")]
    internal class ViewPermissionsViewModel : BaseViewModel
    {
        #region Properties

        public bool IsIdle { get; set; } = true;
        public bool MayEdit { get; set; } = false;
        public ObservableCollection<Permission> Permissions { get; set; }

        public ICommand LoadedCommand { get; set; }
        public ICommand AddCommand { get; set; }

        private PermissionServer.SDK.Client client;

        #endregion Properties

        #region Construction

        public ViewPermissionsViewModel(IMessageBroker MessageBroker, PermissionServer.SDK.Client client, BaseViewModel Parent = null)
            : base(MessageBroker, Parent)
        {
            this.client = client;
            this.Permissions = new ObservableCollection<Permission>();
            this.setupCommands();
        }

        #endregion Construction

        #region Methods

        #region setupCommands
        //TODO: move to abstract base class?
        protected void setupCommands()
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
                //TODO: Yay!
                //TODO: Update List
                //TODO: delete TextBox text
            }
            else
            {
                //TODO: Naye!
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
            IEnumerable<Permission> permissions = await this.client.GetAllPermissionsAsync();
            if (permissions != null)
            {
                this.Permissions = new ObservableCollection<Permission>(permissions);
            }

            this.MayEdit = this.client.May("EditPermissions");
        }
        #endregion loadedCommandExecute

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
