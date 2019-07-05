using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Mvvm.WPF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace ClientApp.Classes
{
    public class WpfNavigationPresenter : BaseWpfNavigationPresenter
    {
        #region Construction

        public WpfNavigationPresenter(ILogger Logger)
            : base("pack://application:,,,/ClientApp;component/Views/", "ClientApp.Views.", Logger)
        {

        }
        #endregion Construction
    }
}
