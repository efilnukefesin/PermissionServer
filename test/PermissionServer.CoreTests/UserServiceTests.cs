using BootStrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using PermissionServer.Core.Interfaces;
using PermissionServer.Core.Services;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PermissionServer.CoreTests
{
    [TestClass]
    public class UserServiceTests : BaseSimpleTest
    {
        #region UserServiceProperties
        [TestClass]
        public class UserServiceProperties : UserServiceTests
        {

        }
        #endregion UserServiceProperties

        #region UserServiceConstruction
        [TestClass]
        public class UserServiceConstruction : UserServiceTests
        {

        }
        #endregion UserServiceConstruction

        #region UserServiceMethods
        [TestClass]
        public class UserServiceMethods : UserServiceTests
        {
            #region CreateTestUsers
            [TestMethod]
            public void CreateTestUsers()
            {
                DiSetup.Tests();
                IUserService userService = DiHelper.GetService<IUserService>();

                userService.CreateTestData();

                Assert.AreEqual("88421113", userService.GetUserBySubject("88421113").Logins.ToList()[0].SubjectId);
                Assert.AreEqual("Bob", userService.GetUserBySubject("88421113").Name);
            }
            #endregion CreateTestUsers

            #region GetUserByName
            [DataTestMethod]
            [DataRow("Admin", true)]
            [DataRow("Bob", true)]
            [DataRow("Nigel", false)]
            public void GetUserByName(string Name, bool ExpectedToBeSuccessful)
            {
                DiSetup.Tests();
                IUserService userService = DiHelper.GetService<IUserService>();
                userService.CreateTestData();

                var user = userService.GetUserByName(Name);

                if (ExpectedToBeSuccessful)
                {
                    Assert.IsNotNull(user);
                    Assert.IsInstanceOfType(user, typeof(User));
                    Assert.AreEqual(Name, user.Name);
                }
                else
                {
                    Assert.IsNull(user);
                }
            }
            #endregion GetUserByName
        }
        #endregion UserServiceMethods
    }
}
