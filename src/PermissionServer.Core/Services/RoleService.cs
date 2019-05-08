using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Core.Interfaces;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Core.Services
{
    public class RoleService : BaseObject, IRoleService
    {
        #region Properties

        private List<Role> roles;

        #endregion Properties

        #region Construction

        public RoleService()
        {
            this.roles = new List<Role>();
        }

        #endregion Construction

        #region Methods

        #region GetRoles
        public IEnumerable<Role> GetRoles()
        {
            return this.roles;
        }
        #endregion GetRoles

        #region CreateTestData
        public void CreateTestData()
        {
            this.roles.Add(new Role("TestRole", null, null));
            this.roles.Add(new Role("AdminRole", null, null));

            throw new NotImplementedException();
        }
        #endregion CreateTestData

        #region dispose
        protected override void dispose()
        {
            this.roles.Clear();
            this.roles = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events

    }
}
