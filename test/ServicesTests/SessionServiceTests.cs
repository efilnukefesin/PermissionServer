using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesTests
{
    [TestClass]
    public class SessionServiceTests : BaseSimpleTest
    {
        #region SessionServiceProperties
        [TestClass]
        public class SessionServiceProperties : SessionServiceTests
        {

        }
        #endregion SessionServiceProperties

        #region SessionServiceConstruction
        [TestClass]
        public class SessionServiceConstruction : SessionServiceTests
        {
            #region IsNotNull
            [TestMethod]
            public void IsNotNull()
            {
                SessionService sessionService = new SessionService();

                Assert.IsNotNull(sessionService);
            }
            #endregion IsNotNull
        }
        #endregion SessionServiceConstruction

        #region SessionServiceMethods
        [TestClass]
        public class SessionServiceMethods : SessionServiceTests
        {
            #region SetAccessToken
            [DataTestMethod]
            [DataRow("Hello World", "Hello World", true)]
            [DataRow("Hello World", "Hello World2", false)]
            public void SetAccessToken(string Value, string CompareValue, bool IsEqualityExpected)
            {
                SessionService sessionService = new SessionService();

                sessionService.SetAccessToken(Value);

                if (IsEqualityExpected)
                {
                    Assert.AreEqual(CompareValue, Value);
                }
                else
                {
                    Assert.AreNotEqual(CompareValue, Value);
                }
            }
            #endregion SetAccessToken
        }
        #endregion SessionServiceMethods
    }

}
