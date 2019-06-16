using Interfaces;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using WPF.Shared.ViewModels;

namespace ClientApp.ViewModels
{
    [Locator("MainViewModel")]
    internal class MainViewModel : BaseViewModel
    {
        #region Properties

        public string WindowTitle { get; set; } = "ClientApp";

        private INavigationService navigationService;
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
            //TODO: implement
        }
        #endregion setupCommands

        #region dispose
        protected override void dispose()
        {
            //TODO: implement
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
