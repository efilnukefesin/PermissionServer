using AdminApp.Classes;
using AdminApp.ViewModels;
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

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DiSetup.AdminApp();
            DiHelper.Register<INavigationPresenter, WpfNavigationPresenter>(Lifetime.Singleton);
            DiSetup.Initialize();

            //this is only to load the assembly
            MainViewModel test = new MainViewModel(null, null);
            test = null;
        }
    }
}
