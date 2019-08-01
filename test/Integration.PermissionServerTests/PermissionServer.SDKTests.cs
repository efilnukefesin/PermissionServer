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
    public class PermissionServerSDKTests : TwoServerTest<PermissionServer.Startup, IdentityServer.Startup>
    {
        #region Construction

        public PermissionServerSDKTests()
            :base(new HttpTestConfiguration(@".\src\PermissionServer\"), new HttpTestConfiguration(@".\src\IdentityServer\"))
        {

        }

        #endregion Construction

        #region Methods

        #region FetchPermissions
        [TestMethod]
        public async Task FetchPermissions()
        {
            //current test setup: start to first debug point, then debug identity server, then continue. Identityserver has to be only for the trick!
            //TODO: automate, also for the sake of ticket creation
            try
            {
                this.startLocalServers();
            }
            catch (Exception ex)
            {

            }
            var permissionHandler = this.getHttpClientHandler1();
            var identityHandler = this.getHttpClientHandler2();
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
