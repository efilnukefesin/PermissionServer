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

        #region ConnectTest
        [TestMethod]
        public async Task ConnectTest()
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
            client.AddAuthenticationHeader("eyJhbGciOiJSUzI1NiIsImtpZCI6IjQ2YzM0OGRlZDE1NmRlZDkzMGJkNGVjOTBlY2NhNThkIiwidHlwIjoiSldUIn0.eyJuYmYiOjE1NjQ0Mzg0MDMsImV4cCI6MTU2NDQ0MjAwMywiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC9yZXNvdXJjZXMiLCJhcGkxIl0sImNsaWVudF9pZCI6InJvLmNsaWVudCIsInN1YiI6IjEyMyIsImF1dGhfdGltZSI6MTU2NDQzODQwMywiaWRwIjoibG9jYWwiLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiYXBpMSJdLCJhbXIiOlsicHdkIl19.cxvwBJBxO4imYEIsZOLeqFSer060Ta8ODf5hJ3e8AAMJ8oMSvSTX7A5zBgcxfiapOsobN0EfoY2dKI-KWMYMDs9DQQ4-gnQQkwYD4cYhNF7ZcAWgnkUk1wtxPozEmlTp_krQ86XxXZz08KGgXWfiAEKYKiBkEqVfWJGP_33aHgzRJFDcQLhOjH0eO5RMoOSNRKeDKtFRnz_oFmfD6n9WshG6eyzZwsox4Mb_ikZXwx_IvWZIxeKumsN5_ECWjlcZenxGeVo7jqzGfKa5lwCafrqaerCmR6savRJnu6ue0XGWJkpHbvYbM3Af-S4t_XeC8Ph4j211dIB-4vP4yX3rcg");  
            var permissionsFetchedSuccessfully = await client.FetchPermissions();
            // [00:15:54 ERR] RestDataService.getClient(): Client creation failed with Exception: 'Object reference not set to an instance of an object.'
            //TODO: continuie
        }
        #endregion ConnectTest

        #endregion Methods
    }
}
