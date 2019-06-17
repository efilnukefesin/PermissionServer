using Interfaces;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Extensions.Wpf.Commands;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using PermissionServer.Client.Interfaces;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Shared.ViewModels;

namespace ClientApp.ViewModels
{
    [Locator("LoginViewModel")]
    internal class LoginViewModel : BaseViewModel
    {
        #region Properties

        public string Hint { get; set; } = "Please enter your Username and Password to Log in.";
        public SecureString SecurePassword { private get; set; }
        public string Username { get; set; }
        public bool IsProgressbarVisible { get; set; } = false;
        public bool IsIdle { get; set; } = true;

        public ICommand OkCommand { get; set; }

        private INavigationService navigationService;
        private SuperHotFeatureServer.SDK.Client client;
        private IIdentityService identityService;
        private ISessionService sessionService;

        #endregion Properties

        #region Construction

        public LoginViewModel(IMessageBroker MessageBroker, INavigationService NavigationService, SuperHotFeatureServer.SDK.Client Client, IIdentityService identityService, ISessionService sessionService, BaseViewModel Parent = null)
            : base(MessageBroker, Parent)
        {
            this.client = Client;
            this.identityService = identityService;
            this.sessionService = sessionService;
            this.navigationService = NavigationService;
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
        private async void okCommandExecute()
        {
            this.IsProgressbarVisible = true;
            this.IsIdle = false;
            this.Hint = "Attempting to identify user and fetch permissions...";
            await this.loginAndFetchPermissions();
            this.IsIdle = true;
            this.IsProgressbarVisible = false;
        }
        #endregion okCommandExecute

        #region loginAndFetchPermissions
        private async Task loginAndFetchPermissions()
        {
            bool couldFetchIdentity = await this.identityService.FetchIdentity(this.Username, this.SecurePassword);
            if (couldFetchIdentity)
            {
                bool hasFetchedAllSuccessfully = await this.client.FetchPermissionsAndUserValues();
                if (hasFetchedAllSuccessfully)
                {
                    bool? hasNavigated = this.navigationService?.Navigate("SuperHotFeatureViewModel");
                    if (hasNavigated == true)
                    {
                        //send a signal to MainViewModel to show menu bar
                        this.SendMessage("ShowMenu");
                    }
                    else if (hasNavigated == false)
                    {
                        this.Hint = "Unfortunately, your user has been verified, but this app is not able to bring you to the next page. Please try again.";
                    }
                    else
                    {
                        this.Hint = "Unfortunately, your user has been verified, but this app is not able to bring you to the next page as something weird has happened. Please try again.";
                    }
                }
                else
                {
                    this.Hint = "Unfortunately, you are not authorized to progress.";
                }
            }
            else
            {
                this.Hint = "Unfortunately, the identity of the user could not be fetched.";
            }
        }
        #endregion loginAndFetchPermissions

        #region dispose
        protected override void dispose()
        {
            this.navigationService = null;
            this.OkCommand = null;
        }
        #endregion dispose

        #region receiveMessage
        protected override bool receiveMessage(string Text, object Data)
        {
            return false;
        }
        #endregion receiveMessage

        #endregion Methods

        #region Events

        #endregion Events
    }
}
