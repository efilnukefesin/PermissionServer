using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTests
{
    [TestClass]
    public class SerializationTests : BaseSimpleTest
    {
        #region SerializationProperties
        [TestClass]
        public class SerializationProperties : SerializationTests
        {

        }
        #endregion SerializationProperties

        #region SerializationConstruction
        [TestClass]
        public class SerializationConstruction : SerializationTests
        {

        }
        #endregion SerializationConstruction

        #region SerializationMethods
        [TestClass]
        public class SerializationMethods : SerializationTests
        {
            #region DateTimeFromString
            [DataTestMethod]
            //[DataRow("2019-07-01T14:51:0009180616+02:00", true)]
            [DataRow("2019-07-01T14:51+02:00", true)]
            [DataRow("2019-07-01T14:51.101+02:00", true)]
            public void DateTimeFromString(string Input, bool IsSuccessExpected)
            {
                DateTimeOffset result;
                bool parsingSuccessful = DateTimeOffset.TryParse(Input, out result);

                Assert.AreEqual(IsSuccessExpected, parsingSuccessful);
            }
            #endregion DateTimeFromString
        }
        #endregion SerializationMethods
    }

}
