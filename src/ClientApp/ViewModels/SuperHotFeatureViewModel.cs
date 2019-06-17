using Interfaces;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Extensions.Wpf.Commands;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using PermissionServer.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WPF.Shared.ViewModels;

namespace ClientApp.ViewModels
{
    [Locator("SuperHotFeatureViewModel")]
    internal class SuperHotFeatureViewModel : BaseViewModel
    {
        #region Properties

        public string Value { get; set; } = "-";

        private INavigationService navigationService;
        private SuperHotFeatureServer.SDK.Client superHotFeatureClient;
        public ICommand LoadedCommand { get; set; }

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
        //TODO: move to abstract base class?
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
        private async void loadedCommandExecute()
        {
            this.Value = await this.superHotFeatureClient.GetValueAsync();
        }
        #endregion loadedCommandExecute

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
