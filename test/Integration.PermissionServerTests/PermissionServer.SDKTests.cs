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
            client.AddAuthenticationHeader("eyJhbGciOiJSUzI1NiIsImtpZCI6ImY0MjEyZjZkZjBkZTU1ZmY1ZmQ4YTZmMTM4YzBiNWI0IiwidHlwIjoiSldUIn0.eyJuYmYiOjE1NjQ1MTczNzUsImV4cCI6MTgyMzcxNzM3NSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC9yZXNvdXJjZXMiLCJhcGkxIl0sImNsaWVudF9pZCI6InJvLmNsaWVudCIsInN1YiI6IjEyMyIsImF1dGhfdGltZSI6MTU2NDUxNzM3NSwiaWRwIjoibG9jYWwiLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiYXBpMSJdLCJhbXIiOlsicHdkIl19.jtQNk4Zez9qgA8TvZfQx_1Uh1-YHq2AmkSoB1a0r64x-snAChfTaLKBgq_UTZtjDC7wpM8A6-H4Ao2JIJnsXWRYpbsgPiM7jd3gCs0cjitzJD5ara6gho9ebwdnWhJwZUmJVLPieo-d3TfMmkZhRkiMjC_MuaBSZvhe7ElvmFUydwtsRu5SBZ7-zUbvNf_AIz7Jyfck6zeZCWKrCODQd7G0YT-rjx3m7kASXKmt4e80-JVXxAckOJXlzudgfxPZtD-x3IM5cD8SU5ZANadxp658FfGlAqbQQaNQcmL1sX39dg7pLo7qPeE2vD1B2fMkOt-Ulwd18F0ZEt-yXnlogiw");  //valid until 2027
            var permissionsFetchedSuccessfully = await client.FetchPermissions();

            Assert.AreEqual(true, couldFetchIdentity);
            Assert.AreEqual(true, permissionsFetchedSuccessfully);

    }
        #endregion FetchPermissions

        #endregion Methods
    }
}
