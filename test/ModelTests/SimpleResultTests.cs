using BootStrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using NET.efilnukefesin.BaseClasses.Test;
using Newtonsoft.Json;
using PermissionServer.Core.Interfaces;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTests
{
    [TestClass]
    public class SimpleResultTests : BaseSimpleTest
    {
        #region UserServiceProperties
        [TestClass]
        public class SimpleResultProperties : SimpleResultTests
        {

        }
        #endregion UserServiceProperties

        #region UserServiceConstruction
        [TestClass]
        public class SimpleResultConstruction : SimpleResultTests
        {

        }
        #endregion UserServiceConstruction

        #region UserServiceMethods
        [TestClass]
        public class SimpleResultMethods : SimpleResultTests
        {
            #region JsonSerialize
            [TestMethod]
            public void JsonSerialize()
            {
                DiSetup.Tests();
                IUserService userService = DiHelper.GetService<IUserService>();
                userService.CreateTestUsers();

                User user = userService.GetUserBySubject("88421113");

                SimpleResult<object> result = new SimpleResult<object>(user);

                string output = JsonConvert.SerializeObject(result);

            }
            #endregion JsonSerialize

        }
        #endregion UserServiceMethods
    }
}
