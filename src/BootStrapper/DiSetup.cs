using Interfaces;
using NET.efilnukefesin.Implementations.DependencyInjection;
using PermissionServer.Client.Interfaces;
using PermissionServer.Client.Services;
using PermissionServer.Core.Interfaces;
using PermissionServer.Core.Services;
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
            DiSetup.Base();
        }
        #endregion ConsoleApp

        #region Tests
        public static void Tests()
        {
            DiSetup.Base();
        }
        #endregion Tests

        #region Base
        public static void Base()
        {
            DiManager.GetInstance().RegisterType<IConfigurationService, StaticConfigurationService>();
            DiManager.GetInstance().RegisterType<IIdentityService, IdentityService>();
            DiManager.GetInstance().RegisterType<IRestService, RestService>();
            DiManager.GetInstance().RegisterType<IPermissionClientService, PermissionClientService>();
            DiManager.GetInstance().RegisterType<IUserService, UserService>();
            DiManager.GetInstance().RegisterType<ISessionService, SessionService>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);
        }
        #endregion Base

        #endregion Methods
    }
}
