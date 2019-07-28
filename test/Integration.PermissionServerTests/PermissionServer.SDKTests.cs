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
            DiSetup.Tests(false, this.getHttpClientHandler());
            DiSetup.Initialize();

            try
            { 
                PermissionServer.SDK.Client client = DiHelper.GetService<PermissionServer.SDK.Client>();
            }
            catch (Exception ex)
            {

            }

            //TODO: continuie
        }
        #endregion ConnectTest

        #endregion Methods
    }
}
