using BootStrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using PermissionServer.Core.Interfaces;
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

                throw new NotImplementedException();
            }
            #endregion GetRoles
        }
        #endregion RoleServiceMethods
    }
}
