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

        public MainViewModel(INavigationService NavigationService, BaseViewModel Parent = null)
            : base(Parent)
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

        #endregion Methods

        #region Events

        #endregion Events
    }
}
