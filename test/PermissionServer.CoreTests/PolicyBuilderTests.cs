using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using PermissionServer.Core.Factories;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PermissionServer.CoreTests
{
    [TestClass]
    public class PolicyBuilderTests : BaseSimpleTest
    {
        #region PolicyBuilderProperties
        [TestClass]
        public class PolicyBuilderProperties : PolicyBuilderTests
        {

        }
        #endregion PolicyBuilderProperties

        #region PolicyBuilderConstruction
        [TestClass]
        public class PolicyBuilderConstruction : PolicyBuilderTests
        {

        }
        #endregion PolicyBuilderConstruction

        #region PolicyBuilderMethods
        [TestClass]
        public class PolicyBuilderMethods : PolicyBuilderTests
        {
            #region Build
            [TestMethod]
            public void Build()
            {
                var result = PolicyBuilder.CreatePolicy("Policy1").AddPermission(new Permission("Permission3")).Build();

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(Policy));
                Assert.AreEqual("Policy1", result.Name);
                Assert.AreEqual(1, result.Permissions.Count());
            }
            #endregion Build
        }
        #endregion PolicyBuilderMethods
    }
}
