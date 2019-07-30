using BootStrapper;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test.Http;
using PermissionServer.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        #region FetchIdentity
        [TestMethod]
        public async Task FetchIdentity()
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

            IIdentityService identityService = DiHelper.GetService<IIdentityService>(handler);
            ISessionService sessionService = DiHelper.GetService<ISessionService>();
            bool couldFetchIdentity = await identityService.FetchIdentity("admin", "admin");

            Assert.AreEqual(true, couldFetchIdentity);
            Assert.IsNotNull(sessionService.AccessToken);
        }
        #endregion FetchIdentity

        #endregion Methods
    }
}
