using Interfaces;
using NET.efilnukefesin.Contracts.DependencyInjection.Classes;
using NET.efilnukefesin.Contracts.DependencyInjection.Enums;
using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.DependencyInjection;
using NET.efilnukefesin.Implementations.Logger.SerilogLogger;
using NET.efilnukefesin.Implementations.Mvvm;
using NET.efilnukefesin.Implementations.Services.DataService.EndpointRegister;
using NET.efilnukefesin.Implementations.Services.DataService.FileDataService;
using NET.efilnukefesin.Implementations.Services.DataService.RestDataService;
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

        #region AdminApp
        public static void AdminApp()
        {
            DiSetup.@base();
            DiManager.GetInstance().RegisterType<IViewModelLocator, ViewModelLocator>(Lifetime.Singleton);
            DiManager.GetInstance().RegisterType<INavigationService, NavigationService>(Lifetime.Singleton);
        }
        #endregion AdminApp

        #region Tests
        public static void Tests()
        {
            DiSetup.@base();
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
            DiSetup.@base();
        }
        #endregion ClientServer

        #region PermissionServer
        public static void PermissionServer()
        {
            DiSetup.@base();
        }
        #endregion PermissionServer

        #region base
        private static void @base()
        {
            DiManager.GetInstance().RegisterType<IEndpointRegister, EndpointRegister>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);  //where is all the data coming from?
            DiManager.GetInstance().RegisterType<IDataService, RestDataService>();  //where is all the data coming from?
            DiManager.GetInstance().RegisterType<IDataService, FileDataService>();  //where is all the data coming from?
            DiManager.GetInstance().RegisterType<ILogger, SerilogLogger>();
            DiManager.GetInstance().RegisterType<IConfigurationService, StaticConfigurationService>();
            DiManager.GetInstance().RegisterType<IIdentityService, IdentityService>();
            DiManager.GetInstance().RegisterType<IRoleService, RoleService>();
            DiManager.GetInstance().RegisterType<IPermissionService, PermissionService>();
            DiManager.GetInstance().RegisterType<IMessageBroker, SimpleMessageBroker>(Lifetime.Singleton);
            DiManager.GetInstance().RegisterType<IUserService, UserService>(Lifetime.Singleton);
            DiManager.GetInstance().RegisterType<ISessionService, SessionService>(Lifetime.Singleton);

            DiManager.GetInstance().RegisterTarget<PermissionServer.SDK.Client>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(RestDataService), new Uri("http://localhost:6008")) });
            DiManager.GetInstance().RegisterTarget<SuperHotFeatureServer.SDK.Client>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(RestDataService), new Uri("http://localhost:6010")) });
            DiManager.GetInstance().RegisterTarget<SuperHotOtherFeatureServer.SDK.Client>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(RestDataService), new Uri("http://localhost:6012")) });

            DiManager.GetInstance().RegisterTarget<IUserService, UserService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(FileDataService), "Data") });
            DiManager.GetInstance().RegisterTarget<IRoleService, RoleService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(FileDataService), "Data") });
            DiManager.GetInstance().RegisterTarget<IPermissionService, PermissionService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(FileDataService), "Data") });
            DiManager.GetInstance().RegisterTarget<AuthenticationService>(Lifetime.Singleton);
            //***
            //TODO: use config values
        }
        #endregion base

        #region Initialize
        //TODO: migrate later on somewhere else, when making a generic Bootstrapper
        public static void Initialize()
        {
            IEndpointRegister endpointRegister = DiHelper.GetService<IEndpointRegister>();
            if (endpointRegister != null)
            {
                endpointRegister.AddEndpoint("SuperHotFeatureServer.SDK.Client.GetValueAsync", "api/values");
                endpointRegister.AddEndpoint("SuperHotOtherFeatureServer.SDK.Client.GetValueAsync", "api/values");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.AddUserAsync", "api/adduser");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.GetUserAsync", "api/permissions");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.CheckPermissionAsync", "api/permissions/check");
                //endpointRegister.AddEndpoint("PermissionServer.Client.BaseClient.fetchPermissions", "api/permissions/givenpermissions");
                endpointRegister.AddEndpoint("PermissionServer.Client.BaseClient.fetchPermissions", "api/permissions/userpermissions");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.fetchUserValues", "api/permissions/uservalues");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.GetAllPermissionsAsync", "api/permissions/getpermissions");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.GetAllUserPermissionsAsync", "api/permissions/userpermissions");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.AddPermissionAsync", "api/permissions/addpermission");

                endpointRegister.AddEndpoint("PermissionServer.Core.Services.PermissionService.CreateTestData", "permissions.json");
            }
        }
        #endregion Initialize

        #endregion Methods
    }
}
