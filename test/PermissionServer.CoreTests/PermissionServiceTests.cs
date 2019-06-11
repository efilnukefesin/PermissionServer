using BootStrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using PermissionServer.Core.Interfaces;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PermissionServer.CoreTests
{
    [TestClass]
    public class PermissionServiceTests : BaseSimpleTest
    {
        #region PermissionServiceProperties
        [TestClass]
        public class PermissionServiceProperties : PermissionServiceTests
        {

        }
        #endregion PermissionServiceProperties

        #region PermissionServiceConstruction
        [TestClass]
        public class PermissionServiceConstruction : PermissionServiceTests
        {

        }
        #endregion PermissionServiceConstruction

        #region PermissionServiceMethods
        [TestClass]
        public class PermissionServiceMethods : PermissionServiceTests
        {
            #region GetPermissions
            [TestMethod]
            public void GetPermissions()
            {
                DiSetup.Tests();
                IPermissionService permissionService = DiHelper.GetService<IPermissionService>();
                permissionService.CreateTestData();

                var result = permissionService.GetPermissions();

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(IEnumerable<Permission>));
            }
            #endregion GetPermissions

            #region GetPermissionByName
            [TestMethod]
            public void GetPermissionByName()
            {
                DiSetup.Tests();
                IPermissionService permissionService = DiHelper.GetService<IPermissionService>();
                permissionService.CreateTestData();

                var result = permissionService.GetPermissionByName("User");

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(Permission));
                Assert.AreEqual("User", result.Name);
            }
            #endregion GetPermissionByName

            #region AddPermission
            [DataTestMethod]
            [DataRow("111", true)]
            [DataRow("SomeOtherPermission", true)]
            [DataRow("111", false)]
            public void AddPermission(string PermissionName, bool IsExpectedToBeSuccessful)
            {
                DiSetup.Tests();
                IPermissionService permissionService = DiHelper.GetService<IPermissionService>();
                permissionService.CreateTestData();

                int numberBefore = permissionService.GetPermissions().Count();
                var result = permissionService.AddPermission(new Permission(PermissionName));
                int numberAfter = permissionService.GetPermissions().Count();

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(bool));
                if (IsExpectedToBeSuccessful)
                {
                    Assert.AreEqual(true, result);
                    Assert.AreEqual(numberAfter, numberBefore + 1);
                }
                else
                {
                    Assert.AreEqual(false, result);
                    Assert.AreEqual(numberAfter, numberBefore);
                }
            }
            #endregion AddPermission
        }
        #endregion PermissionServiceMethods
    }
}
