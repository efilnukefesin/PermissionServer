using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Helpers;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WPF.Shared.NavigationPresenter;

namespace AdminApp.Classes
{
    public class WpfNavigationPresenter : BaseWpfNavigationPresenter
    {
        #region Construction

        public WpfNavigationPresenter(ILogger Logger)
            : base("pack://application:,,,/AdminApp;component/Views/", "AdminApp.Views.", Logger)
        {

        }
        #endregion Construction
    }
}
