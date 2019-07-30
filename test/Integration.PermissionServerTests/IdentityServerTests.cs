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
        [DataTestMethod]
        [DataRow("admin", "admin", true)]
        [DataRow("bob", "bob", true)]
        [DataRow("bob", "notBobsPassword", false)]
        public async Task FetchIdentity(string Username, string Password, bool IsExpectedToBeSuccessful)
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
            bool couldFetchIdentity = await identityService.FetchIdentity(Username, Password);

            Assert.AreEqual(IsExpectedToBeSuccessful, couldFetchIdentity);
            if (IsExpectedToBeSuccessful)
            {
                Assert.IsNotNull(sessionService.AccessToken);
            }
            else
            {
                Assert.IsNull(sessionService.AccessToken);
            }
        }
        #endregion FetchIdentity

        #endregion Methods
    }
}
