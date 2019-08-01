using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootStrapper;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test.Http;
using PermissionServer.Client.Interfaces;

namespace Integration.PermissionServerTests
{
	[TestClass]
    public class PermissionServerSDKTests : BaseHttpMultipleServersTest
    {
        #region Properties

        private Guid idIdentityServer;
        private Guid idPermissionServer;

        #endregion Properties

        #region Construction

        public PermissionServerSDKTests()
            :base()
        {
            this.idIdentityServer = this.AddServer<IdentityServer.Startup>(new HttpTestConfiguration(@".\src\IdentityServer\"));
            this.idPermissionServer = this.AddServer<IdentityServer.Startup>(new HttpTestConfiguration(@".\src\PermissionServer\"));
        }

        #endregion Construction

        #region Methods

        #region FetchPermissions
        [TestMethod]
        public async Task FetchPermissions()
        {
            var permissionHandler = this.getHttpClientHandler(this.idPermissionServer);
            var identityHandler = this.getHttpClientHandler(this.idIdentityServer);
            DiSetup.Tests(false, permissionHandler);
            DiSetup.Initialize();

            IIdentityService identityService = DiHelper.GetService<IIdentityService>(identityHandler);
            ISessionService sessionService = DiHelper.GetService<ISessionService>();
            bool couldFetchIdentity = await identityService.FetchIdentity("admin", "admin");
            PermissionServer.SDK.Client client = DiHelper.GetService<PermissionServer.SDK.Client>();
            //get token manually from other test
            client.AddAuthenticationHeader(sessionService.AccessToken);
            var permissionsFetchedSuccessfully = await client.FetchPermissions();

            Assert.AreEqual(true, couldFetchIdentity);
            Assert.AreEqual(true, permissionsFetchedSuccessfully);

    }
        #endregion FetchPermissions

        #endregion Methods
    }
}
