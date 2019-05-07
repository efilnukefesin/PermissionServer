using System;
using System.Collections.Generic;
using System.Text;
using PermissionServer.Models;

namespace PermissionServer.Core.Interfaces
{
    public interface IPermissionService
    {
        #region Properties

        #endregion Properties

        #region Methods

        #endregion Methods

        #region Events

        #endregion Events
        IEnumerable<Permission> GetPermissions();
    }
}
