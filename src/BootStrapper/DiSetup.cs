using Interfaces;
using NET.efilnukefesin.Contracts.DependencyInjection.Classes;
using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Implementations.DependencyInjection;
using NET.efilnukefesin.Implementations.Logger.SerilogLogger;
using PermissionServer.Client.Interfaces;
using PermissionServer.Client.Services;
using PermissionServer.Core.Interfaces;
using PermissionServer.Core.Services;
using Services;
using System;
using System.Collections.Generic;

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
            DiManager.GetInstance().RegisterType<IPermissionService, PermissionService>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);
            DiManager.GetInstance().RegisterType<IRoleService, RoleService>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);
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

        #region PermissionServer
        public static void PermissionServer()
        {
            DiSetup.@base();
            DiManager.GetInstance().RegisterType<IPermissionService, PermissionService>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);
            DiManager.GetInstance().RegisterType<IRoleService, RoleService>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);
        }
        #endregion PermissionServer

        #region base
        private static void @base()
        {
            DiManager.GetInstance().RegisterType<IDataService, RestDataService>();  //where is all the data coming from?
            DiManager.GetInstance().RegisterType<ILogger, SerilogLogger>();
            DiManager.GetInstance().RegisterType<IConfigurationService, StaticConfigurationService>();
            DiManager.GetInstance().RegisterType<IIdentityService, IdentityService>();
            DiManager.GetInstance().RegisterType<IUserService, UserService>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);
            DiManager.GetInstance().RegisterType<ISessionService, SessionService>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);

            DiManager.GetInstance().RegisterTarget<PermissionServer.SDK.Client>(new List<TypeInstanceParameterInfoObject>() { new TypeInstanceParameterInfoObject(typeof(IDataService), new RestDataService(DiHelper.GetService<IConfigurationService>().PermissionServerEndpoint)) });
            DiManager.GetInstance().RegisterTarget<SuperHotFeatureServer.SDK.Client>(new List<TypeInstanceParameterInfoObject>() { new TypeInstanceParameterInfoObject(typeof(IDataService), new RestDataService(DiHelper.GetService<IConfigurationService>().SuperHotFeatureServerEndpoint)) });
            DiManager.GetInstance().RegisterTarget<SuperHotOtherFeatureServer.SDK.Client>(new List<TypeInstanceParameterInfoObject>() { new TypeInstanceParameterInfoObject(typeof(IDataService), new RestDataService(DiHelper.GetService<IConfigurationService>().SuperHotOtherFeatureServerEndpoint)) });
        }
        #endregion base

        #endregion Methods
    }
}
