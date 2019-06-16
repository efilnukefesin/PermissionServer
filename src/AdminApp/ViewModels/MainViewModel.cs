using Interfaces;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Extensions.Wpf.Commands;
using NET.efilnukefesin.Implementations.Base;
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

        public string WindowTitle { get; set; } = "AdminApp";
        public bool IsMenubarVisible { get; set; } = false;
        public SimpleResult<string> Message { get; set; } = new SimpleResult<string>("Welcome!");

        private INavigationService navigationService;

        public ICommand UserInfoCommand { get; set; }
        public ICommand ViewUsersCommand { get; set; }
        public ICommand ViewRolesCommand { get; set; }
        public ICommand ViewPermissionsCommand { get; set; }

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
            this.ViewUsersCommand = new RelayCommand(this.viewUsersCommandExecute, this.viewUsersCommandCanExecute);
            this.ViewRolesCommand = new RelayCommand(this.viewRolesCommandExecute, this.viewRolesCommandCanExecute);
            this.ViewPermissionsCommand = new RelayCommand(this.viewPermissionsCommandExecute, this.viewPermissionsCommandCanExecute);
        }
        #endregion setupCommands

        #region viewPermissionsCommandCanExecute
        private bool viewPermissionsCommandCanExecute()
        {
            return this.permissionServerClient.May("GetPermissions");
        }
        #endregion viewPermissionsCommandCanExecute

        #region viewPermissionsCommandExecute
        private void viewPermissionsCommandExecute()
        {
            bool? hasNavigated = this.navigationService?.Navigate("ViewPermissionsViewModel");
        }
        #endregion viewPermissionsCommandExecute

        #region viewRolesCommandCanExecute
        private bool viewRolesCommandCanExecute()
        {
            return this.permissionServerClient.May("GetRoles");
        }
        #endregion viewRolesCommandCanExecute

        #region viewRolesCommandExecute
        private void viewRolesCommandExecute()
        {
            bool? hasNavigated = this.navigationService?.Navigate("ViewRolesViewModel");
        }
        #endregion viewRolesCommandExecute

        #region viewUsersCommandCanExecute
        private bool viewUsersCommandCanExecute()
        {
            return this.permissionServerClient.May("GetUsers");
        }
        #endregion viewUsersCommandCanExecute

        #region viewUsersCommandExecute
        private void viewUsersCommandExecute()
        {
            bool? hasNavigated = this.navigationService?.Navigate("ViewUsersViewModel");
        }
        #endregion viewUsersCommandExecute

        #region userInfoCommandCanExecute
        private bool userInfoCommandCanExecute()
        {
            return this.permissionServerClient.May("User");
        }
        #endregion userInfoCommandCanExecute

        #region userInfoCommandExecute
        private void userInfoCommandExecute()
        {
            bool? hasNavigated = this.navigationService?.Navigate("UserInfoViewModel");
        }
        #endregion userInfoCommandExecute

        #region dispose
        protected override void dispose()
        {
            this.navigationService = null;
        }
        #endregion dispose

        #region receiveMessage
        protected override bool receiveMessage(string Text, object Data)
        {
            bool result = false;
            if (Text.Equals("ShowMenu"))
            {
                this.IsMenubarVisible = true;
                result = true;
            }
            else if (Text.Equals("HideMenu"))
            {
                this.IsMenubarVisible = false;
                result = true;
            }
            else if (Text.Equals("Message"))
            {
                this.Message = (SimpleResult<string>)Data;
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
