using Interfaces;
using NET.efilnukefesin.Extensions.Wpf.Commands;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WPF.Shared.ViewModels;

namespace AdminApp.ViewModels
{
    [Locator("AddRoleToUserViewModel")]
    internal class AddRoleToUserViewModel : BaseWindowViewModel
    {
        #region Properties

        public User SelectedUser { get; set; }
        public ICommand LoadedCommand { get; set; }

        #endregion Properties

        #region Construction

        public AddRoleToUserViewModel(IMessageBroker MessageBroker, BaseViewModel Parent = null) : base(MessageBroker, Parent)
        {
            this.WindowTitle = "Add Role to User";
            this.setupCommands();
        }

        #endregion Construction

        #region Methods

        #region setupCommands
        protected void setupCommands()
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
        private void loadedCommandExecute()
        {
            //TODO: load stuff
        }
        #endregion loadedCommandExecute

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
