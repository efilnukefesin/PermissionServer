using Interfaces;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Extensions.Wpf.Commands;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WPF.Shared.ViewModels;

namespace ClientApp.ViewModels
{
    [Locator("MainViewModel")]
    internal class MainViewModel : BaseViewModel
    {
        #region Properties

        public string WindowTitle { get; set; } = "ClientApp";
        public bool IsMenubarVisible { get; set; } = false;
        public SimpleResult<string> Message { get; set; } = new SimpleResult<string>("Welcome!");

        private INavigationService navigationService;
        private PermissionServer.SDK.Client permissionServerClient;

        public ICommand SuperHotFeature01Command { get; set; }
        public ICommand SuperHotOtherFeature01Command { get; set; }

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
            this.SuperHotFeature01Command = new RelayCommand(this.superHotFeature01CommandExecute, this.superHotFeature01CommandCanExecute);
            this.SuperHotOtherFeature01Command = new RelayCommand(this.superHotOtherFeature01CommandExecute, this.superHotOtherFeature01CommandCanExecute);
        }
        #endregion setupCommands

        #region superHotFeature01CommandCanExecute
        private bool superHotFeature01CommandCanExecute()
        {
            return this.permissionServerClient.May("SuperHotFeature01");
        }
        #endregion superHotFeature01CommandCanExecute

        #region superHotFeature01CommandExecute
        private void superHotFeature01CommandExecute()
        {
            bool? hasNavigated = this.navigationService?.Navigate("SuperHotFeatureViewModel");
        }
        #endregion superHotFeature01CommandExecute

        #region superHotOtherFeature01CommandCanExecute
        private bool superHotOtherFeature01CommandCanExecute()
        {
            return this.permissionServerClient.May("SuperHotOtherFeature01");
        }
        #endregion superHotOtherFeature01CommandCanExecute

        #region superHotOtherFeature01CommandExecute
        private void superHotOtherFeature01CommandExecute()
        {
            bool? hasNavigated = this.navigationService?.Navigate("SuperHotOtherFeatureViewModel");
        }
        #endregion superHotOtherFeature01CommandExecute

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

        #region dispose
        protected override void dispose()
        {
            //TODO: implement
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
