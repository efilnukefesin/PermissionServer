using Interfaces;
using NET.efilnukefesin.Implementations.DependencyInjection;
using Services;
using System;

namespace BootStrapper
{
    public static class DiSetup
    {
        #region Methods

        #region ConsoleApp
        public static void ConsoleApp()
        {
            DiManager.GetInstance().RegisterType<IIdentityService, IdentityService>();
        }
        #endregion ConsoleApp

        #endregion Methods
    }
}
