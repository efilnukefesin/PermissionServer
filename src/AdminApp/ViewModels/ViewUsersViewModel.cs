using Interfaces;
using NET.efilnukefesin.Extensions.Wpf.Commands;
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
    [Locator("ViewUsersViewModel")]
    internal class ViewUsersViewModel : BaseViewAndEditViewModel
    {
        #region Properties

        public ObservableCollection<User> Users { get; set; }

        private PermissionServer.SDK.Client client;

        #endregion Properties

        #region Construction

        public ViewUsersViewModel(IMessageBroker MessageBroker, PermissionServer.SDK.Client client, BaseViewModel Parent = null)
            : base(MessageBroker, Parent)
        {
            this.client = client;
            this.Users = new ObservableCollection<User>();
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
            this.renewUsers();
            this.MayEdit = this.client.May("EditUsers");
        }
        #endregion loadedCommandExecute

        #region renewUsers: fetches the User list from the server and copies it to the local version
        /// <summary>
        /// fetches the User list from the server and copies it to the local version
        /// </summary>
        private async void renewUsers()
        {
            //TODO: generalize, Move to base class?
            IEnumerable<User> users = await this.client.GetAllUsersAsync();
            if (users != null)
            {
                this.Users = new ObservableCollection<User>(users);
            }
        }
        #endregion renewUsers

        #region dispose
        protected override void dispose()
        {
            this.Users.Clear();
            this.Users = null;
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
