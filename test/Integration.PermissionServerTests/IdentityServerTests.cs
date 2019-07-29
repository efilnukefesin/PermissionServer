using BootStrapper;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.PermissionServerTests
{
    [TestClass]
    public class IdentityServerTests : BaseHttpTest<IdentityServer.Startup>
    {
        #region Construction

        public IdentityServerTests()
            : base(@".\src\IdentityServer\", null, null)
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

            IIdentityService identityService = DiHelper.GetService<IIdentityService>();
            bool couldFetchIdentity = await identityService.FetchIdentity("admin", "admin");

            //TODO: continuie
        }
        #endregion ConnectTest

        #endregion Methods
    }
}
