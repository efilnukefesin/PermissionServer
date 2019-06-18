using Interfaces;
using NET.efilnukefesin.Extensions.Wpf.Commands;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WPF.Shared.ViewModels;

namespace AdminApp.ViewModels
{
    [Locator("ViewRolesViewModel")]
    internal class ViewRolesViewModel : BaseViewAndEditViewModel
    {
        #region Properties

        public ObservableCollection<Role> Roles { get; set; }
        private PermissionServer.SDK.Client client;

        #endregion Properties

        #region Construction

        public ViewRolesViewModel(IMessageBroker MessageBroker, PermissionServer.SDK.Client client, BaseViewModel Parent = null)
            : base(MessageBroker, Parent)
        {
            this.client = client;
            this.Roles = new ObservableCollection<Role>();
        }

        #endregion Construction

        #region Methods

        #region setupCommands
        protected override void setupCommands()
        {
            this.LoadedCommand = new RelayCommand(this.loadedCommandExecute, this.loadedCommandCanExecute);
        }
        #endregion setupCommands

        #region loadedCommandCanExecute
        private bool loadedCommandCanExecute()
        {
            return true;
        }
        #endregion loadedCommandCanExecute

        #region loadedCommandExecute
        private async void loadedCommandExecute()
        {
            this.renewRoles();
            this.MayEdit = this.client.May("EditRoles");
        }
        #endregion loadedCommandExecute

        #region renewRoles: fetches the Role list from the server and copies it to the local version
        /// <summary>
        /// fetches the Role list from the server and copies it to the local version
        /// </summary>
        private async void renewRoles()
        {
            IEnumerable<Role> roles = await this.client.GetAllRolesAsync();
            if (roles != null)
            {
                this.Roles = new ObservableCollection<Role>(roles);
            }
        }
        #endregion renewRoles

        #region dispose
        protected override void dispose()
        {
            this.Roles.Clear();
            this.Roles = null;
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
