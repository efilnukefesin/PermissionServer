using System;
using System.Collections.Generic;
using System.Text;
using Interfaces;
using NET.efilnukefesin.Contracts.Base;
using PermissionServer.Models;

namespace PermissionServer.Core.Interfaces
{
    public interface IPermissionService : IBaseObject, ICreateTestData, IInitalize
    {
        #region Properties

        #endregion Properties

        #region Methods

        IEnumerable<Permission> GetPermissions();

        #region GetPermissionByName: returns a Permission with the given name
        /// <summary>
        /// returns a Permission with the given name
        /// </summary>
        /// <param name="Name">the name of the permission</param>
        /// <returns>the permission object or null if not found</returns>
        Permission GetPermissionByName(string Name);
        bool AddPermission(Permission permission);
        #endregion GetPermissionByName

        #endregion Methods

        #region Events

        #endregion Events
    }
}
