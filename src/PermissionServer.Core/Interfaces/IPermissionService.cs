using System;
using System.Collections.Generic;
using System.Text;
using NET.efilnukefesin.Contracts.Base;
using PermissionServer.Models;

namespace PermissionServer.Core.Interfaces
{
    public interface IPermissionService : IBaseObject, ICreateTestData
    {
        #region Properties

        #endregion Properties

        #region Methods

        IEnumerable<Permission> GetPermissions();

        #region GetPermission: returns a Permission with the given name
        /// <summary>
        /// returns a Permission with the given name
        /// </summary>
        /// <param name="Name">the name of the permission</param>
        /// <returns>the permission object or null if not found</returns>
        Permission GetPermission(string Name);
        #endregion GetPermission

        #endregion Methods

        #region Events

        #endregion Events
    }
}
