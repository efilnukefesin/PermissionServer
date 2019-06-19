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
    public class RoleBuilderTests : BaseSimpleTest
    {
        #region RoleBuilderProperties
        [TestClass]
        public class RoleBuilderProperties : RoleBuilderTests
        {

        }
        #endregion RoleBuilderProperties

        #region RoleBuilderConstruction
        [TestClass]
        public class RoleBuilderConstruction : RoleBuilderTests
        {

        }
        #endregion RoleBuilderConstruction

        #region RoleBuilderMethods
        [TestClass]
        public class RoleBuilderMethods : RoleBuilderTests
        {
            #region Build
            [TestMethod]
            public void Build()
            {
                var result = RoleBuilder.CreateRole("Role1").AddPermission(new Permission("Permission1")).AddPermission(new Permission("Permission2")).Build();

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(Role));
                Assert.AreEqual("Role1", result.Name);
                Assert.AreEqual(2, result.Permissions.Count());
            }
            #endregion Build
        }
        #endregion RoleBuilderMethods
    }
}
