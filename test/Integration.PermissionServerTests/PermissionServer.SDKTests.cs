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

        #region FetchPermissions
        [TestMethod]
        public async Task FetchPermissions()
        {
            this.startLocalServer(this.idIdentityServer);
            var identityHandler = this.getHttpClientHandler(this.idIdentityServer);
            PermissionServer.Startup.OverrideJwtBackChannelHandler = identityHandler;
            this.startLocalServer(this.idPermissionServer);
            var permissionHandler = this.getHttpClientHandler(this.idPermissionServer);
            
            DiSetup.Tests(false, permissionHandler);
            DiSetup.Initialize();

            IIdentityService identityService = DiHelper.GetService<IIdentityService>(identityHandler);
            ISessionService sessionService = DiHelper.GetService<ISessionService>();
            bool couldFetchIdentity = await identityService.FetchIdentity("admin", "admin");
            PermissionServer.SDK.Client client = DiHelper.GetService<PermissionServer.SDK.Client>();
            //get token manually from other test
            client.AddAuthenticationHeader(sessionService.AccessToken);
            var permissionsFetchedSuccessfully = await client.FetchPermissions();

            /*
             * at System.Net.Http.HttpClient.FinishSendAsyncBuffered(Task`1 sendTask, HttpRequestMessage request, CancellationTokenSource cts, Boolean disposeCts)
   at Microsoft.IdentityModel.Protocols.HttpDocumentRetriever.GetDocumentAsync(String address, CancellationToken cancel)
             * */
            // https://github.com/IdentityServer/IdentityServer3.AccessTokenValidation/issues/126
            // https://stackoverflow.com/questions/54145950/azure-web-api-jwt-unable-to-obtain-configuration-socket-forbidden
            // https://github.com/IdentityServer/IdentityServer4/issues/2186
            // https://github.com/IdentityServer/IdentityServer4/issues/2877 ***
            // https://github.com/fuzzzerd/IdentityServerAndApi/commit/b306799eb16aa77ad04b848c86ab6e8f2f2014d0 <- FIX

            Assert.AreEqual(true, couldFetchIdentity);
            Assert.AreEqual(true, permissionsFetchedSuccessfully);

    }
        #endregion FetchPermissions

        #endregion Methods
    }
}
