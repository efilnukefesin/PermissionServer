using ClientApp.Classes;
using BootStrapper;
using NET.efilnukefesin.Contracts.DependencyInjection.Enums;
using NET.efilnukefesin.Contracts.Mvvm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DiSetup.ClientApp();
            DiHelper.Register<INavigationPresenter, WpfNavigationPresenter>(Lifetime.Singleton);
            DiSetup.Initialize();
        }
    }
}
