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
            DiSetup.@base();
            DiManager.GetInstance().RegisterType<IClientRestService, ClientRestService>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);
        }
        #endregion ConsoleApp

        #region Tests
        public static void Tests()
        {
            DiSetup.@base();
        }
        #endregion Tests

        #region Server
        public static void Server()
        {
            DiSetup.@base();
        }
        #endregion Server

        #region ClientServer
        public static void ClientServer()
        {
            DiManager.GetInstance().RegisterType<IConfigurationService, StaticConfigurationService>();
            DiManager.GetInstance().RegisterType<IRestService, RestService>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);
            DiManager.GetInstance().RegisterType<IPermissionClientService, PermissionClientService>();
            DiManager.GetInstance().RegisterType<ISessionService, SessionService>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);
        }
        #endregion ClientServer

        #region base
        private static void @base()
        {
            DiManager.GetInstance().RegisterType<IConfigurationService, StaticConfigurationService>();
            DiManager.GetInstance().RegisterType<IIdentityService, IdentityService>();
            DiManager.GetInstance().RegisterType<IRestService, RestService>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);
            DiManager.GetInstance().RegisterType<IPermissionClientService, PermissionClientService>();
            DiManager.GetInstance().RegisterType<IUserService, UserService>();
            DiManager.GetInstance().RegisterType<ISessionService, SessionService>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);
        }
        #endregion base

        #endregion Methods
    }
}
