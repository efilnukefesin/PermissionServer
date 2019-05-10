using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Core.Interfaces;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PermissionServer.Core.Services
{
    public class PermissionService : BaseObject, IPermissionService
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

        #region CreateTestData
        public void CreateTestData()
        {
            this.permissions.Add(new Permission() { Name = "User" });
            this.permissions.Add(new Permission() { Name = "SuperHotFeature1" });
            this.permissions.Add(new Permission() { Name = "SuperHotFeature2" });
            this.permissions.Add(new Permission() { Name = "GetUnknownLogins" });
            this.permissions.Add(new Permission() { Name = "LinkLoginToUser" });
            this.permissions.Add(new Permission() { Name = "LinkRoleToUser" });
            this.permissions.Add(new Permission() { Name = "LinkPermissionToRole" });
            this.permissions.Add(new Permission() { Name = "AddUser" });
            this.permissions.Add(new Permission() { Name = "AddRole" });
            this.permissions.Add(new Permission() { Name = "AddPermission" });
            this.permissions.Add(new Permission() { Name = "GetUsers" });
            this.permissions.Add(new Permission() { Name = "GetRoles" });
            this.permissions.Add(new Permission() { Name = "GetPermissions" });
        }
        #endregion CreateTestData

        #region GetPermissions
        public IEnumerable<Permission> GetPermissions()
        {
            return this.permissions;
        }
        #endregion GetPermissions

        #region GetPermission: returns a Permission with the given name
        /// <summary>
        /// returns a Permission with the given name
        /// </summary>
        /// <param name="Name">the name of the permission</param>
        /// <returns>the permission object or null if not found</returns>
        public Permission GetPermissionByName(string Name)
        {
            return this.permissions.FirstOrDefault(x => x.Name.Equals(Name));
        }
        #endregion GetPermission

        #region dispose
        protected override void dispose()
        {
            this.permissions.Clear();
            this.permissions = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
