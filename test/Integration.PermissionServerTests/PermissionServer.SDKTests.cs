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
            this.idIdentityServer = this.AddServer<IdentityServer.Startup>(new HttpTestConfiguration(@".\src\IdentityServer\"), false);  //Q: why does it work with this line commented out?
            this.idPermissionServer = this.AddServer<PermissionServer.Startup>(new HttpTestConfiguration(@".\src\PermissionServer\"), false);
        }

        #endregion Construction

        #region Methods

        #region startIdentityAndPermissionServer: Helper Method to start the Identity and Permission Server
        /// <summary>
        /// Helper Method to start the Identity and Permission Server
        /// </summary>
        /// <returns>true, if no error occured</returns>
        private bool startIdentityAndPermissionServer()
        {
            bool result = false;

            try
            {
                this.startLocalServer(this.idIdentityServer);
                var identityHandler = this.getHttpClientHandler(this.idIdentityServer);
                PermissionServer.Startup.OverrideJwtBackChannelHandler = identityHandler;  //needed to change also the backchannelhandler, https://github.com/fuzzzerd/IdentityServerAndApi/commit/b306799eb16aa77ad04b848c86ab6e8f2f2014d0
                this.startLocalServer(this.idPermissionServer);
                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion startIdentityAndPermissionServer

        #region initDi: Helper Method to do the Di related stuff
        /// <summary>
        /// Helper Method to do the Di related stuff
        /// </summary>
        /// <returns>true, if no error occured</returns>
        private bool initDi()
        {
            bool result = false;

            try
            {
                var permissionHandler = this.getHttpClientHandler(this.idPermissionServer);
                DiSetup.Tests(false, permissionHandler);
                DiSetup.Initialize();
                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion initDi

        #region FetchPermissionsAndUserValues
        [TestMethod]
        public async Task FetchPermissionsAndUserValues()
        {
            bool startedSuccessfully = this.startIdentityAndPermissionServer();
            bool initializedSuccessfully = this.initDi();

            IIdentityService identityService = DiHelper.GetService<IIdentityService>(this.getHttpClientHandler(this.idIdentityServer));
            ISessionService sessionService = DiHelper.GetService<ISessionService>();
            bool couldFetchIdentity = await identityService.FetchIdentity("admin", "admin");
            PermissionServer.SDK.Client client = DiHelper.GetService<PermissionServer.SDK.Client>();
            //get token manually from other test
            client.AddAuthenticationHeader(sessionService.AccessToken);
            var permissionsFetchedSuccessfully = await client.FetchPermissions();

            Assert.AreEqual(true, startedSuccessfully);
            Assert.AreEqual(true, initializedSuccessfully);
            Assert.AreEqual(true, couldFetchIdentity);
            Assert.AreEqual(true, permissionsFetchedSuccessfully);
            Assert.AreEqual(true, client.HasUserValues());
        }
        #endregion FetchPermissionsAndUserValues

        #endregion Methods
    }
}
