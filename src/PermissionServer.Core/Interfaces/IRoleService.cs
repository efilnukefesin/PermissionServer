using System;
using System.Collections.Generic;
using System.Text;
using NET.efilnukefesin.Contracts.Base;
using PermissionServer.Models;

namespace PermissionServer.Core.Interfaces
{
    public interface IRoleService : IBaseObject, ICreateTestData
    {
        #region Properties

        #endregion Properties

        #region Methods

        IEnumerable<Role> GetRoles();
        Role GetRoleByName(string Name);

        #endregion Methods

        #region Events

        #endregion Events
    }
}
