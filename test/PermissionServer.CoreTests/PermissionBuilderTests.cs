using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using PermissionServer.Core.Factories;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.CoreTests
{
    [TestClass]
    public class PermissionBuilderTests : BaseSimpleTest
    {
        #region PermissionBuilderProperties
        [TestClass]
        public class PermissionBuilderProperties : PermissionBuilderTests
        {

        }
        #endregion PermissionBuilderProperties

        #region PermissionBuilderConstruction
        [TestClass]
        public class PermissionBuilderConstruction : PermissionBuilderTests
        {

        }
        #endregion PermissionBuilderConstruction

        #region PermissionBuilderMethods
        [TestClass]
        public class PermissionBuilderMethods : PermissionBuilderTests
        {
            #region Build
            [TestMethod]
            public void Build()
            {
                var result = PermissionBuilder.CreatePermission("Permission1").Build();

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(Permission));
                Assert.AreEqual("Permission1", result.Name);
            }
            #endregion Build
        }
        #endregion PermissionBuilderMethods
    }
}
