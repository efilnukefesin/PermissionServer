using Interfaces;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminApp.ViewModels
{
    [Locator("MainViewModel")]
    internal class MainViewModel : BaseViewModel
    {
        #region Properties

        public bool IsMenubarVisible { get; set; } = false;

        private INavigationService navigationService;

        #endregion Properties

        #region Construction

        public MainViewModel(IMessageBroker MessageBroker, INavigationService NavigationService, BaseViewModel Parent = null)
            : base(MessageBroker, Parent)
        {
            this.navigationService = NavigationService;
        }

        #endregion Construction

        #region Methods

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
