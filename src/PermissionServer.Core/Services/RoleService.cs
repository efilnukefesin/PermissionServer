using PermissionServer.Core.Interfaces;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Core.Services
{
    public class RoleService : IRoleService
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

        #endregion Methods

        #region Events

        #endregion Events

    }
}
