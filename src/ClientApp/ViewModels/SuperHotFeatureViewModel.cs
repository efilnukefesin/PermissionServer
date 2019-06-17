using Interfaces;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using WPF.Shared.ViewModels;

namespace ClientApp.ViewModels
{
    [Locator("SuperHotFeatureViewModel")]
    internal class SuperHotFeatureViewModel : BaseViewModel
    {
        #region Properties

        private INavigationService navigationService;
        private SuperHotFeatureServer.SDK.Client superHotFeatureClient;

        #endregion Properties

        #region Construction

        public SuperHotFeatureViewModel(IMessageBroker MessageBroker, SuperHotFeatureServer.SDK.Client SuperHotFeatureClient, INavigationService NavigationService, BaseViewModel Parent = null)
            : base(MessageBroker, Parent)
        {
            this.navigationService = NavigationService;
            this.superHotFeatureClient = SuperHotFeatureClient;
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

        #region receiveMessage
        protected override bool receiveMessage(string Text, object Data)
        {
            return false;
        }
        #endregion receiveMessage

        #region dispose
        protected override void dispose()
        {
            this.superHotFeatureClient = null;
            this.navigationService = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
