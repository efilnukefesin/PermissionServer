using BootStrapper;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using NET.efilnukefesin.BaseClasses.Test;
using PermissionServer.Client.Services;
using Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
                DiSetup.Tests();

                // https://gingter.org/2018/07/26/how-to-mock-httpclient-in-your-net-c-unit-tests/
                // ARRANGE
                var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
                handlerMock
                   .Protected()
                   // Setup the PROTECTED method to mock
                   .Setup<Task<HttpResponseMessage>>(
                      "SendAsync",
                      ItExpr.IsAny<HttpRequestMessage>(),
                      ItExpr.IsAny<CancellationToken>()
                   )
                   // prepare the expected response of the mocked http call
                   .ReturnsAsync(new HttpResponseMessage()
                   {
                       StatusCode = HttpStatusCode.OK,
                       Content = new StringContent("[{'id':1,'value':'1'}]"),  //TODO: insert SimpleResult<bool> with true
                   })
                   .Verifiable();

                IDataService dataService = DiHelper.GetService<IDataService>(new Uri("http://baseUri"), "someToken", handlerMock.Object);

                bool result = dataService.GetAsync<bool>().GetAwaiter().GetResult();
                //***
            }
            #endregion GetAsync
        }
        #endregion DataServiceMethods
    }
}
