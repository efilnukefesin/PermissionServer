using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Contracts.Services.DataService;
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
        private ILogger logger;
        protected IDataService dataService;

        #endregion Properties

        #region Construction

        public PermissionService(ILogger logger, IDataService dataService)
        {
            this.dataService = dataService;
            this.logger = logger;
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
            this.permissions.Add(new Permission() { Name = "CreateUser" });
            this.permissions.Add(new Permission() { Name = "CreateRole" });
            this.permissions.Add(new Permission() { Name = "CreatePermission" });
            this.permissions.Add(new Permission() { Name = "GetUsers" });
            this.permissions.Add(new Permission() { Name = "GetRoles" });
            this.permissions.Add(new Permission() { Name = "GetPermissions" });
            this.permissions.Add(new Permission() { Name = "EditPermissions" });
            this.permissions.Add(new Permission() { Name = "UserValues" });
            this.permissions.Add(new Permission() { Name = "UserPermissions" });
            this.permissions.Add(new Permission() { Name = "AddPermission" });

            //TODO: store in file
            var hasBeenWrittenSuccessfully = this.dataService.CreateOrUpdateAsync<Permission>("PermissionServer.Core.Services.PermissionService.CreateTestData", this.permissions);
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
            Permission result = this.permissions.FirstOrDefault(x => x.Name.Equals(Name));
            if (result == null)
            {
                //not existing permission has been asked for
                this.logger.Log($"PermissionService.GetPermissionByName: not existing permission has been asked for - {Name}", NET.efilnukefesin.Contracts.Logger.Enums.LogLevel.Warning);
            }
            return result;
        }
        #endregion GetPermission

        #region AddPermission
        public bool AddPermission(Permission permission)
        {
            bool result = false;
            if (!this.permissions.Any(x => x.Name.Equals(permission.Name)))
            {
                this.permissions.Add(permission);
                //TODO: store Permissions
                result = true;
            }
            return result;
        }
        #endregion AddPermission

        #region dispose
        protected override void dispose()
        {
            this.permissions.Clear();
            this.permissions = null;
            this.logger = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
