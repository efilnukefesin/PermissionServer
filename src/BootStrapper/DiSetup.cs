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
using System.Net.Http;

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
            DiManager.GetInstance().AddTypeTranslation("HttpMessageHandlerProxy", typeof(HttpMessageHandler));
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
            DiManager.GetInstance().RegisterType<IEndpointRegister, EndpointRegister>();  //where is all the data coming from?
            DiManager.GetInstance().RegisterType<IDataService, RestDataService>();  //where is all the data coming from?
            DiManager.GetInstance().RegisterType<ILogger, SerilogLogger>();
            DiManager.GetInstance().RegisterType<IConfigurationService, StaticConfigurationService>();
            DiManager.GetInstance().RegisterType<IIdentityService, IdentityService>();
            DiManager.GetInstance().RegisterType<IRoleService, RoleService>();
            DiManager.GetInstance().RegisterType<IPermissionService, PermissionService>();
            DiManager.GetInstance().RegisterType<IUserService, UserService>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);
            DiManager.GetInstance().RegisterType<ISessionService, SessionService>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);

            //DiManager.GetInstance().RegisterTarget<PermissionServer.SDK.Client>(new List<ParameterInfoObject>() { new TypeInstanceParameterInfoObject(typeof(IDataService), DiHelper.GetService<RestDataService>(DiHelper.GetService<IConfigurationService>().PermissionServerEndpoint)) });
            //DiManager.GetInstance().RegisterTarget<SuperHotFeatureServer.SDK.Client>(new List<ParameterInfoObject>() { new TypeInstanceParameterInfoObject(typeof(IDataService), DiHelper.GetService<RestDataService>(DiHelper.GetService<IConfigurationService>().SuperHotFeatureServerEndpoint)) });
            //DiManager.GetInstance().RegisterTarget<SuperHotOtherFeatureServer.SDK.Client>(new List<ParameterInfoObject>() { new TypeInstanceParameterInfoObject(typeof(IDataService), DiHelper.GetService<RestDataService>(DiHelper.GetService<IConfigurationService>().SuperHotOtherFeatureServerEndpoint)) });

            DiManager.GetInstance().RegisterTarget<PermissionServer.SDK.Client>(new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), new Uri("http://localhost:6008")) });
            DiManager.GetInstance().RegisterTarget<SuperHotFeatureServer.SDK.Client>(new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), new Uri("http://localhost:6010")) });
            DiManager.GetInstance().RegisterTarget<SuperHotOtherFeatureServer.SDK.Client>(new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), new Uri("http://localhost:6012")) });
            //TODO: use config values


            //TODO: Resolve resolce - register - paradox
            //***

            //DiManager.GetInstance().RegisterTarget<PermissionServer.SDK.Client>(new List<TypeInstanceParameterInfoObject>() { new TypeInstanceParameterInfoObject(typeof(IDataService), new RestDataService(new Uri("http://localhost:6008"), new EndpointRegister())) });
            //DiManager.GetInstance().RegisterTarget<SuperHotFeatureServer.SDK.Client>(new List<TypeInstanceParameterInfoObject>() { new TypeInstanceParameterInfoObject(typeof(IDataService), new RestDataService(new Uri("http://localhost:6010"), new EndpointRegister())) });
            //DiManager.GetInstance().RegisterTarget<SuperHotOtherFeatureServer.SDK.Client>(new List<TypeInstanceParameterInfoObject>() { new TypeInstanceParameterInfoObject(typeof(IDataService), new RestDataService(new Uri("http://localhost:6012"), new EndpointRegister())) });
        }
        #endregion base

        #region Initialize
        //TODO: migrate later on somewhere else, when making a generic Bootstrapper
        public static void Initialize()
        {
            IEndpointRegister endpointRegister = DiManager.GetInstance().Resolve<IEndpointRegister>();
            if (endpointRegister != null)
            {
                endpointRegister.AddEndpoint("SuperHotFeatureServer.SDK.Client.GetValueAsync", "api/values");
                endpointRegister.AddEndpoint("SuperHotOtherFeatureServer.SDK.Client.GetValueAsync", "api/values");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.AddUserAsync", "api/adduser");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.GetUserAsync", "api/permissions");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.", "");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.", "");
            }
        }
        #endregion Initialize

        #endregion Methods
    }
}
