using PermissionServer.Core.Interfaces;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Core.Services
{
    public class PermissionService : IPermissionService
    {
        #region Properties

        private List<Permission> permissions;

        #endregion Properties

        #region Construction

        public PermissionService()
        {
            this.permissions = new List<Permission>();
        }

        #endregion Construction

        #region Methods

        #region GetPermissions
        public IEnumerable<Permission> GetPermissions()
        {
            return this.permissions;
        }
        #endregion GetPermissions

        #endregion Methods

        #region Events

        #endregion Events
    }
}
