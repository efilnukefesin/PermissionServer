using BootStrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using PermissionServer.Core.Interfaces;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.CoreTests
{
    [TestClass]
    public class RoleServiceTests : BaseSimpleTest
    {
        #region RoleServiceProperties
        [TestClass]
        public class RoleServiceProperties : RoleServiceTests
        {

        }
        #endregion RoleServiceProperties

        #region RoleServiceConstruction
        [TestClass]
        public class RoleServiceConstruction : RoleServiceTests
        {

        }
        #endregion RoleServiceConstruction

        #region RoleServiceMethods
        [TestClass]
        public class RoleServiceMethods : RoleServiceTests
        {
            #region GetRoles
            [TestMethod]
            public void GetRoles()
            {
                DiSetup.Tests();
                IRoleService roleService = DiHelper.GetService<IRoleService>();
                roleService.CreateTestData();

                var result = roleService.GetRoles();

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(IEnumerable<Role>));
            }
            #endregion GetRoles

            #region GetRoleByName
            [DataTestMethod]
            [DataRow("TestRole", true)]
            [DataRow("AdminRole", true)]
            [DataRow("NoKnownRole", false)]
            public void GetRoleByName(string Name, bool ExpectedToBeSuccessful)
            {
                DiSetup.Tests();
                IRoleService roleService = DiHelper.GetService<IRoleService>();
                roleService.CreateTestData();

                var role = roleService.GetRoleByName(Name);

                if (ExpectedToBeSuccessful)
                {
                    Assert.IsNotNull(role);
                    Assert.IsInstanceOfType(role, typeof(Role));
                    Assert.AreEqual(Name, role.Name);
                }
                else
                {
                    Assert.IsNull(role);
                }
            }
            #endregion GetRoleByName
        }
        #endregion RoleServiceMethods
    }
}
