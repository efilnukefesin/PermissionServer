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

                var result = permissionService.GetPermissions();

                throw new NotImplementedException();
            }
            #endregion GetPermissions
        }
        #endregion PermissionServiceMethods
    }

}
