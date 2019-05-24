using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Extensions.Wpf.Commands;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Text;
using System.Windows.Input;

namespace AdminApp.ViewModels
{
    [Locator("LoginViewModel")]
    internal class LoginViewModel : BaseViewModel
    {
        #region Properties

        public string Hint { get; set; }
        public SecureString SecurePassword { private get; set; }
        public string Password { get; set; }  //just for lookup reasons, delete in a productive app
        public string Username { get; set; }
        public bool IsProgressbarVisible { get; set; }

        public ICommand OkCommand { get; set; }

        private INavigationService navigationService;
        private PermissionServer.SDK.Client permissionServerClient;

        #endregion Properties

        #region Construction

        public LoginViewModel(INavigationService NavigationService, PermissionServer.SDK.Client PermissionServerClient, BaseViewModel Parent = null)
            : base(Parent)
        {
            this.permissionServerClient = PermissionServerClient;
            this.navigationService = NavigationService;
            this.IsProgressbarVisible = false;
            this.Hint = "Please enter your Username and Password to Log in.";
            this.setupCommands();
        }

        #endregion Construction

        #region Methods

        #region setupCommands
        //TODO: move to abstract base class?
        protected void setupCommands()
        {
            this.OkCommand = new RelayCommand(this.okCommandExecute, this.okCommandCanExecute);
        }
        #endregion setupCommands

        #region okCommandCanExecute
        private bool okCommandCanExecute()
        {
            bool result = false;
            result = !string.IsNullOrWhiteSpace(this.Username) && this.SecurePassword?.Length > 0;
            return result;
        }
        #endregion okCommandCanExecute

        #region okCommandExecute
        private void okCommandExecute()
        {
            this.IsProgressbarVisible = true;

            bool isAuthorized = false;
            //TODO: check permission
            if (isAuthorized)
            {
                bool? hasNavigated = this.navigationService?.Navigate("AppViewModel");
                if (hasNavigated != true)
                {
                    //TODO: figure out,what to do
                }
            }
            else
            {
                this.Hint = "Unfortunately, you are not authorized to progress.";
            }

            this.IsProgressbarVisible = false;
        }
        #endregion okCommandExecute

        #region dispose
        protected override void dispose()
        {
            this.navigationService = null;
            this.OkCommand = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events
    }
}
