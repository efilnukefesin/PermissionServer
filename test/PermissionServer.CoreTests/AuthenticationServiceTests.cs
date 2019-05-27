using BootStrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using PermissionServer.Core.Interfaces;
using PermissionServer.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PermissionServer.CoreTests
{
    [TestClass]
    public class AuthenticationServiceTests : BaseSimpleTest
    {
        #region AuthenticationServiceProperties
        [TestClass]
        public class AuthenticationServiceProperties : AuthenticationServiceTests
        {

        }
        #endregion AuthenticationServiceProperties

        #region AuthenticationServiceConstruction
        [TestClass]
        public class AuthenticationServiceConstruction : AuthenticationServiceTests
        {

        }
        #endregion AuthenticationServiceConstruction

        #region AuthenticationServiceMethods
        [TestClass]
        public class AuthenticationServiceMethods : AuthenticationServiceTests
        {
            #region GetUsers
            [TestMethod]
            public void GetUsers()
            {
                DiSetup.Tests();
                IUserService userService = DiHelper.GetService<IUserService>();
                userService.CreateTestData();
                AuthenticationService authenticationService = DiHelper.GetService<AuthenticationService>();

                var result = authenticationService.GetUsers();

                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() > 0);
            }
            #endregion GetUsers

            #region GetRoles
            [TestMethod]
            public void GetRoles()
            {
                DiSetup.Tests();
                IUserService userService = DiHelper.GetService<IUserService>();
                userService.CreateTestData();
                AuthenticationService authenticationService = DiHelper.GetService<AuthenticationService>();

                var result = authenticationService.GetRoles();

                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() > 0);
            }
            #endregion GetRoles

            #region GetPermissions
            [TestMethod]
            public void GetPermissions()
            {
                DiSetup.Tests();
                IUserService userService = DiHelper.GetService<IUserService>();
                userService.CreateTestData();
                AuthenticationService authenticationService = DiHelper.GetService<AuthenticationService>();

                var result = authenticationService.GetPermissions();

                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() > 0);
            }
            #endregion GetPermissions

            #region AddUser
            [TestMethod]
            public void AddUser()
            {
                DiSetup.Tests();
                IUserService userService = DiHelper.GetService<IUserService>();
                userService.CreateTestData();
                AuthenticationService authenticationService = DiHelper.GetService<AuthenticationService>();

                int usercountBefore = authenticationService.GetUsers().Count();

                var result = authenticationService.AddUser(new Models.User("Nigel"));

                int usercountAfter = authenticationService.GetUsers().Count();

                Assert.IsNotNull(result);
                Assert.AreEqual(true, result);
                Assert.IsTrue(usercountBefore +1 == usercountAfter);
            }
            #endregion AddUser

            #region AddUserNegative
            [TestMethod]
            public void AddUserNegative()
            {
                DiSetup.Tests();
                IUserService userService = DiHelper.GetService<IUserService>();
                userService.CreateTestData();
                AuthenticationService authenticationService = DiHelper.GetService<AuthenticationService>();
                var existingUser = authenticationService.GetUser("123");

                int usercountBefore = authenticationService.GetUsers().Count();

                var result = authenticationService.AddUser(existingUser);  //try to add an existing user

                int usercountAfter = authenticationService.GetUsers().Count();

                Assert.IsNotNull(result);
                Assert.AreEqual(false, result);
                Assert.IsTrue(usercountBefore == usercountAfter);
            }
            #endregion AddUserNegative
        }
        #endregion AuthenticationServiceMethods
    }
}
