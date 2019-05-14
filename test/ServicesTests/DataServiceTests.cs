using BootStrapper;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using PermissionServer.Client.Services;
using Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesTests
{
    [TestClass]
    public class DataServiceTests : BaseSimpleTest
    {
        #region DataServiceProperties
        [TestClass]
        public class DataServiceProperties : DataServiceTests
        {

        }
        #endregion DataServiceProperties

        #region DataServiceConstruction
        [TestClass]
        public class DataServiceConstruction : DataServiceTests
        {
            #region IsNotNull
            [TestMethod]
            public void IsNotNull()
            {
                DiSetup.Tests();

                IDataService dataService = DiHelper.GetService<IDataService>();

                Assert.IsNotNull(dataService);
            }
            #endregion IsNotNull
        }
        #endregion DataServiceConstruction

        #region DataServiceMethods
        [TestClass]
        public class DataServiceMethods : DataServiceTests
        {
            #region GetAsync
            [TestMethod]
            public void GetAsync()
            {
                // https://gingter.org/2018/07/26/how-to-mock-httpclient-in-your-net-c-unit-tests/

            }
            #endregion GetAsync
        }
        #endregion DataServiceMethods
    }
}
