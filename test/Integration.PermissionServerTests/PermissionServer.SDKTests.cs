using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootStrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test.Http;

namespace Integration.PermissionServerTests
{
	[TestClass]
    public class PermissionServerSDKTests : BaseHttpTest<PermissionServer.Startup>
    {
        #region Construction

        public PermissionServerSDKTests()
            :base(@".\src\PermissionServer\", null, null)
        {

        }

        #endregion Construction

        #region Methods

        #region FetchPermissions
        [TestMethod]
        public async Task FetchPermissions()
        {
            try
            {
                this.startLocalServer();
            }
            catch (Exception ex)
            {

            }
            var handler = this.getHttpClientHandler();
            DiSetup.Tests(false, handler);
            DiSetup.Initialize();

            //IIdentityService identityService = DiHelper.GetService<IIdentityService>();
            PermissionServer.SDK.Client client = DiHelper.GetService<PermissionServer.SDK.Client>();
            //get token manually from other test
            client.AddAuthenticationHeader("eyJhbGciOiJSUzI1NiIsImtpZCI6ImY0MjEyZjZkZjBkZTU1ZmY1ZmQ4YTZmMTM4YzBiNWI0IiwidHlwIjoiSldUIn0.eyJuYmYiOjE1NjQ1MTczNzUsImV4cCI6MTgyMzcxNzM3NSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC9yZXNvdXJjZXMiLCJhcGkxIl0sImNsaWVudF9pZCI6InJvLmNsaWVudCIsInN1YiI6IjEyMyIsImF1dGhfdGltZSI6MTU2NDUxNzM3NSwiaWRwIjoibG9jYWwiLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiYXBpMSJdLCJhbXIiOlsicHdkIl19.jtQNk4Zez9qgA8TvZfQx_1Uh1-YHq2AmkSoB1a0r64x-snAChfTaLKBgq_UTZtjDC7wpM8A6-H4Ao2JIJnsXWRYpbsgPiM7jd3gCs0cjitzJD5ara6gho9ebwdnWhJwZUmJVLPieo-d3TfMmkZhRkiMjC_MuaBSZvhe7ElvmFUydwtsRu5SBZ7-zUbvNf_AIz7Jyfck6zeZCWKrCODQd7G0YT-rjx3m7kASXKmt4e80-JVXxAckOJXlzudgfxPZtD-x3IM5cD8SU5ZANadxp658FfGlAqbQQaNQcmL1sX39dg7pLo7qPeE2vD1B2fMkOt-Ulwd18F0ZEt-yXnlogiw");  //valid until 2027
            var permissionsFetchedSuccessfully = await client.FetchPermissions();
            // Method not found: 'System.String NET.efilnukefesin.Implementations.Base.BaseObject.get_Id()'.
            // [00:15:54 ERR] RestDataService.getClient(): Client creation failed with Exception: 'Object reference not set to an instance of an object.'
            //TODO: continuie

//            [10:49:58 INF] RestDataService.getClient(): whole Uri is 'http://localhost:6008/api/ownpermissions/'
//Ausnahme ausgelöst: "System.InvalidOperationException" in System.Private.CoreLib.dll
//Ausnahme ausgelöst: "System.InvalidOperationException" in System.Private.CoreLib.dll
    }
        #endregion FetchPermissions

        #endregion Methods
    }
}
