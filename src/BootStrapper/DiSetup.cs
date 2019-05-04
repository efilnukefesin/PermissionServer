using Interfaces;
using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Implementations.DependencyInjection;
using NET.efilnukefesin.Implementations.Logger.SerilogLogger;
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
            DiManager.GetInstance().RegisterType<ISessionService, SessionService>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);
        }
        #endregion ClientServer

        #region base
        private static void @base()
        {
            DiManager.GetInstance().RegisterType<ILogger, SerilogLogger>();
            DiManager.GetInstance().RegisterType<IConfigurationService, StaticConfigurationService>();
            DiManager.GetInstance().RegisterType<IIdentityService, IdentityService>();
            DiManager.GetInstance().RegisterType<IUserService, UserService>();
            DiManager.GetInstance().RegisterType<ISessionService, SessionService>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);
        }
        #endregion base

        #endregion Methods
    }
}
