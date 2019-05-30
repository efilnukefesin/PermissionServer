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
                var result = RoleBuilder.CreateRole("Role1").AddPermission(new Permission("Permission1")).AddPermission(new Permission("Permission2")).AddRole(new Role("Role2", null, null)).Build();

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(Role));
                Assert.AreEqual("Role1", result.Name);
                Assert.AreEqual(2, result.Permissions.Count());
                Assert.AreEqual(1, result.Roles.Count());
            }
            #endregion Build
        }
        #endregion RoleBuilderMethods
    }
}
