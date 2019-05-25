using Interfaces;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Extensions.Wpf.Commands;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AdminApp.ViewModels
{
    [Locator("MainViewModel")]
    internal class MainViewModel : BaseViewModel
    {
        #region Properties

        public bool IsMenubarVisible { get; set; } = false;

        private INavigationService navigationService;

        public ICommand UserInfoCommand { get; set; }

        private PermissionServer.SDK.Client permissionServerClient;

        #endregion Properties

        #region Construction

        public MainViewModel(IMessageBroker MessageBroker, PermissionServer.SDK.Client PermissionServerClient, INavigationService NavigationService, BaseViewModel Parent = null)
            : base(MessageBroker, Parent)
        {
            this.navigationService = NavigationService;
            this.permissionServerClient = PermissionServerClient;
            this.setupCommands();
        }

        #endregion Construction

        #region Methods

        #region setupCommands
        private void setupCommands()
        {
            this.UserInfoCommand = new RelayCommand(this.userInfoCommandExecute, this.userInfoCommandCanExecute);
        }
        #endregion setupCommands

        #region userInfoCommandCanExecute
        private bool userInfoCommandCanExecute()
        {
            return this.permissionServerClient.May("User");
        }
        #endregion userInfoCommandCanExecute

        #region userInfoCommandExecute
        private void userInfoCommandExecute()
        {
            throw new NotImplementedException();
        }
        #endregion userInfoCommandExecute

        #region dispose
        protected override void dispose()
        {
            this.navigationService = null;
        }
        #endregion dispose

        #region receiveMessage
        protected override bool receiveMessage(string Text)
        {
            bool result = false;
            if (Text.Equals("ShowMenu"))
            {
                this.IsMenubarVisible = true;
                //this.UserInfoCommand.RaiseCanExecuteChanged();
                result = true;
            }
            else if (Text.Equals("HideMenu"))
            {
                this.IsMenubarVisible = false;
                result = true;
            }
            return result;
        }
        #endregion receiveMessage

        #endregion Methods

        #region Events

        #endregion Events
    }
}
