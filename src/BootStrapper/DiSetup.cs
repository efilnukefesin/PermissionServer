using Interfaces;
using NET.efilnukefesin.Contracts.DependencyInjection.Classes;
using NET.efilnukefesin.Contracts.DependencyInjection.Enums;
using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Contracts.FeatureToggling;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.DependencyInjection;
using NET.efilnukefesin.Implementations.Logger.SerilogLogger;
using NET.efilnukefesin.Implementations.Mvvm;
using NET.efilnukefesin.Implementations.FeatureToggling;
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
using NET.efilnukefesin.Implementations.Services.DataService.InMemoryDataService;
using Microsoft.Extensions.DependencyInjection;

namespace BootStrapper
{
    public static class DiSetup
    {
        #region Properties

        private static bool isTest = true;

        #endregion Properties

        #region Methods

        public static void AddToAspNetCore(IServiceCollection services)
        {
            services.AddTransient<IDataService>(s => DiHelper.GetService<FileDataService>("Data"));
            if (DiSetup.isTest)
            {
                services.AddSingleton<IConfigurationService>(DiHelper.GetService<StaticTestConfigurationService>());
            }
            else
            {
                services.AddSingleton<IConfigurationService>(DiHelper.GetService<StaticConfigurationService>());
            }

            DiSetup.Initialize();
        }

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
        }
        #endregion AdminApp

        #region ClientApp
        public static void ClientApp()
        {
            DiSetup.@base();
        }
        #endregion ClientApp

        #region Tests
        public static void Tests(bool isInMemory = true, HttpMessageHandler overrideHttpMessageHandler = null)
        {
            DiManager.GetInstance().Reset();
            DiManager.GetInstance().AddTypeTranslation("HttpMessageHandlerProxy", typeof(HttpMessageHandler));
            DiManager.GetInstance().AddTypeTranslation("Microsoft.AspNetCore.Mvc.Testing.Handlers.RedirectHandler", typeof(HttpMessageHandler));
            DiManager.GetInstance().AddTypeTranslation("RedirectHandler", typeof(HttpMessageHandler));
            DiSetup.baseTest(isInMemory, overrideHttpMessageHandler);
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

        #region level1_common
        private static void level1_common()
        {
            DiManager.GetInstance().RegisterType<IEndpointRegister, EndpointRegister>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);  //where is all the data coming from?
            DiManager.GetInstance().RegisterType<ILogger, SerilogLogger>(); 
            DiManager.GetInstance().RegisterType<IIdentityService, IdentityService>();
            DiManager.GetInstance().RegisterType<IRoleService, RoleService>();
            DiManager.GetInstance().RegisterType<IPermissionService, PermissionService>();
            DiManager.GetInstance().RegisterType<IMessageBroker, SimpleMessageBroker>(Lifetime.Singleton);
            DiManager.GetInstance().RegisterType<IUserService, UserService>(Lifetime.Singleton);
            DiManager.GetInstance().RegisterType<ISessionService, SessionService>(Lifetime.Singleton);
            DiManager.GetInstance().RegisterType<IFeatureToggleManager, FeatureToggleManager>(Lifetime.Singleton);
        }
        #endregion level1_common

        #region level1_test
        private static void level1_test()
        {
            DiSetup.level1_common();
            DiManager.GetInstance().RegisterType<IConfigurationService, StaticTestConfigurationService>();
        }
        #endregion level1_test

        #region level1_prod
        private static void level1_prod()
        {
            DiSetup.level1_common();
            DiManager.GetInstance().RegisterType<IConfigurationService, StaticConfigurationService>();
        }
        #endregion level1_prod

        #region level2
        private static void level2(bool isInMemory = false, HttpMessageHandler overrideHttpMessageHandler = null)
        {
            if (isInMemory)
            {
                DiManager.GetInstance().RegisterType<IDataService, InMemoryDataService>();  //where is all the data coming from?
            }
            else
            {
                DiManager.GetInstance().RegisterType<IDataService, RestDataService>();  //where is all the data coming from?
            }
            DiManager.GetInstance().RegisterType<IDataService, FileDataService>();  //where is all the data coming from?

            if (isInMemory)
            {
                DiManager.GetInstance().RegisterTarget<PermissionServer.SDK.Client>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(InMemoryDataService)) });
                DiManager.GetInstance().RegisterTarget<SuperHotFeatureServer.SDK.Client>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(InMemoryDataService)) });
                DiManager.GetInstance().RegisterTarget<SuperHotOtherFeatureServer.SDK.Client>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(InMemoryDataService)) });
            }
            else
            {
                // TODO: use config values, add a test config class
                DiManager.GetInstance().RegisterTarget<PermissionServer.SDK.Client>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(RestDataService), new Uri("http://localhost:6008"), overrideHttpMessageHandler) });
                DiManager.GetInstance().RegisterTarget<SuperHotFeatureServer.SDK.Client>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(RestDataService), new Uri("http://localhost:6010"), overrideHttpMessageHandler) });
                DiManager.GetInstance().RegisterTarget<SuperHotOtherFeatureServer.SDK.Client>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(RestDataService), new Uri("http://localhost:6012"), overrideHttpMessageHandler) });
            }

            DiManager.GetInstance().RegisterTarget<IUserService, UserService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(FileDataService), "Data") });
            DiManager.GetInstance().RegisterTarget<IRoleService, RoleService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(FileDataService), "Data") });
            DiManager.GetInstance().RegisterTarget<IPermissionService, PermissionService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(FileDataService), "Data") });
            DiManager.GetInstance().RegisterTarget<AuthenticationService>(Lifetime.Singleton);
        }
        #endregion level2

        #region level2_test
        private static void level2_test(bool isInMemory = false, HttpMessageHandler overrideHttpMessageHandler = null)
        {
            if (isInMemory)
            {
                DiManager.GetInstance().RegisterType<IDataService, InMemoryDataService>();  //where is all the data coming from?
            }
            else
            {
                DiManager.GetInstance().RegisterType<IDataService, RestDataService>();  //where is all the data coming from?
            }
            DiManager.GetInstance().RegisterType<IDataService, FileDataService>();  //where is all the data coming from?

            if (isInMemory)
            {
                DiManager.GetInstance().RegisterTarget<PermissionServer.SDK.Client>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(InMemoryDataService)) });
                DiManager.GetInstance().RegisterTarget<SuperHotFeatureServer.SDK.Client>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(InMemoryDataService)) });
                DiManager.GetInstance().RegisterTarget<SuperHotOtherFeatureServer.SDK.Client>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(InMemoryDataService)) });
            }
            else
            {
                // TODO: use config values, add a test config class
                DiManager.GetInstance().RegisterTarget<PermissionServer.SDK.Client>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(RestDataService), new Uri("http://localhost"), overrideHttpMessageHandler) });  //TODO: get from config / only localhost for test
                DiManager.GetInstance().RegisterTarget<SuperHotFeatureServer.SDK.Client>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(RestDataService), new Uri("http://localhost:6010"), overrideHttpMessageHandler) });
                DiManager.GetInstance().RegisterTarget<SuperHotOtherFeatureServer.SDK.Client>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(RestDataService), new Uri("http://localhost:6012"), overrideHttpMessageHandler) });
            }

            DiManager.GetInstance().RegisterTarget<IUserService, UserService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(FileDataService), "Data") });
            DiManager.GetInstance().RegisterTarget<IRoleService, RoleService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(FileDataService), "Data") });
            DiManager.GetInstance().RegisterTarget<IPermissionService, PermissionService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(FileDataService), "Data") });
            DiManager.GetInstance().RegisterTarget<AuthenticationService>(Lifetime.Singleton);
        }
        #endregion level2_test

        #region level3
        private static void level3()
        {
            DiManager.GetInstance().RegisterType<IViewModelLocator, ViewModelLocator>(Lifetime.Singleton);
            DiManager.GetInstance().RegisterType<INavigationService, NavigationService>(Lifetime.Singleton);
        }
        #endregion level3

        #region base
        private static void @base(bool isInMemory = false, HttpMessageHandler overrideHttpMessageHandler = null)
        {
            DiSetup.level1_prod();
            DiSetup.level2(isInMemory, overrideHttpMessageHandler);
            DiSetup.level3();
        }
        #endregion base

        #region baseTest
        private static void baseTest(bool isInMemory = false, HttpMessageHandler overrideHttpMessageHandler = null)
        {
            DiSetup.level1_test();
            DiSetup.level2_test(isInMemory, overrideHttpMessageHandler);
            DiSetup.level3();
        }
        #endregion baseTest

        #region Initialize
        //TODO: migrate later on somewhere else, when making a generic Bootstrapper
        public static void Initialize()
        {
            IEndpointRegister endpointRegister = DiHelper.GetService<IEndpointRegister>();
            if (endpointRegister != null)
            {
                endpointRegister.AddEndpoint("SuperHotFeatureServer.SDK.Client.GetValueAsync", "api/values");
                endpointRegister.AddEndpoint("SuperHotOtherFeatureServer.SDK.Client.GetValueAsync", "api/values");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.AddUserAsync", "api/permissions/adduser");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.GetUserAsync", "api/permissions");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.CheckPermissionAsync", "api/permissions/check");
                //endpointRegister.AddEndpoint("PermissionServer.Client.BaseClient.fetchPermissions", "api/permissions/givenpermissions");
                endpointRegister.AddEndpoint("PermissionServer.Client.BaseClient.fetchPermissions", "api/permissions/userpermissions");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.fetchUserValues", "api/permissions/uservalues");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.GetAllPermissionsAsync", "api/permissions/getpermissions");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.GetAllUserPermissionsAsync", "api/permissions/userpermissions");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.AddPermissionAsync", "api/permissions/addpermission");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.GetAllUsersAsync", "api/permissions/getusers");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.GetAllRolesAsync", "api/permissions/getroles");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.GetUnknownLoginsAsync", "api/permissions/getunknownlogins");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.AddUnkownLoginsAsync", "api/permissions/unknownlogins");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.DeleteUnkownLoginAsync", "api/permissions/unknownlogins");

                endpointRegister.AddEndpoint("PermissionServer.Core.Services.PermissionService.Store", "permissions.json");
                endpointRegister.AddEndpoint("PermissionServer.Core.Services.RoleService.Store", "roles.json");
                endpointRegister.AddEndpoint("PermissionServer.Core.Services.UserService.Store", "users.json");
                endpointRegister.AddEndpoint("PermissionServer.Core.Services.UnkownLogins.Store", "unknownlogins.json");

                endpointRegister.AddEndpoint("OwnPermissions.Store", "api/ownpermissions/");
                endpointRegister.AddEndpoint("OwnUserValues.Store", "api/ownuservalues/");
            }

            IFeatureToggleManager featureToggleManager = DiHelper.GetService<IFeatureToggleManager>();
            if (featureToggleManager != null)
            {
                //TODO. set up feature toggles
            }
        }
        #endregion Initialize

        #region InitializeTests
        //TODO: migrate later on somewhere else, when making a generic Bootstrapper
        public static void InitializeTests()
        {
            IEndpointRegister endpointRegister = DiHelper.GetService<IEndpointRegister>();
            if (endpointRegister != null)
            {
                endpointRegister.AddEndpoint("SuperHotFeatureServer.SDK.Client.GetValueAsync", "api/values");
                endpointRegister.AddEndpoint("SuperHotOtherFeatureServer.SDK.Client.GetValueAsync", "api/values");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.AddUserAsync", "api/permissions/adduser");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.GetUserAsync", "api/permissions");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.CheckPermissionAsync", "api/permissions/check");
                //endpointRegister.AddEndpoint("PermissionServer.Client.BaseClient.fetchPermissions", "api/permissions/givenpermissions");
                endpointRegister.AddEndpoint("PermissionServer.Client.BaseClient.fetchPermissions", "api/permissions/userpermissions");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.fetchUserValues", "api/permissions/uservalues");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.GetAllPermissionsAsync", "api/permissions/getpermissions");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.GetAllUserPermissionsAsync", "api/permissions/userpermissions");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.AddPermissionAsync", "api/permissions/addpermission");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.GetAllUsersAsync", "api/permissions/getusers");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.GetAllRolesAsync", "api/permissions/getroles");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.GetUnknownLoginsAsync", "api/permissions/unknownlogins");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.AddUnkownLoginsAsync", "api/permissions/unknownlogins");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.DeleteUnkownLoginAsync", "api/permissions/unknownlogins");

                endpointRegister.AddEndpoint("PermissionServer.Core.Services.PermissionService.Store", "permissions.json");
                endpointRegister.AddEndpoint("PermissionServer.Core.Services.RoleService.Store", "roles.json");
                endpointRegister.AddEndpoint("PermissionServer.Core.Services.UserService.Store", "users.json");
                endpointRegister.AddEndpoint("PermissionServer.Core.Services.UnkownLogins.Store", "unknownlogins.json");

                endpointRegister.AddEndpoint("OwnPermissions.Store", "api/ownpermissions/");
                endpointRegister.AddEndpoint("OwnUserValues.Store", "api/ownuservalues/");
            }

            IFeatureToggleManager featureToggleManager = DiHelper.GetService<IFeatureToggleManager>();
            if (featureToggleManager != null)
            {
                //TODO. set up feature toggles
            }
        }
        #endregion InitializeTests

        #endregion Methods
    }
}
