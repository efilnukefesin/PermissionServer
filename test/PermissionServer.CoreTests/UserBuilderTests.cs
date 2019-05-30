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
    public class UserBuilderTests : BaseSimpleTest
    {
        #region UserBuilderProperties
        [TestClass]
        public class UserBuilderProperties : UserBuilderTests
        {

        }
        #endregion UserBuilderProperties

        #region UserBuilderConstruction
        [TestClass]
        public class UserBuilderConstruction : UserBuilderTests
        {

        }
        #endregion UserBuilderConstruction

        #region UserBuilderMethods
        [TestClass]
        public class UserBuilderMethods : UserBuilderTests
        {
            #region Build
            [TestMethod]
            public void Build()
            {
                var result = UserBuilder.CreateUser("Bob2").AddLogin(new Login("321")).AddRole(new Role("SampleRole", null)).Build();

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(User));
                Assert.AreEqual("Bob2", result.Name);
                Assert.AreEqual(1, result.Logins.Count());
                Assert.AreEqual(1, result.Roles.Count());
            }
            #endregion Build
        }
        #endregion UserBuilderMethods
    }
}
