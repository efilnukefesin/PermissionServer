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
    public class RoleService : BaseObject, IRoleService
    {
        #region Properties

        private List<Role> roles;
        private IPermissionService permissionService;
        protected IDataService dataService;

        #endregion Properties

        #region Construction

        public RoleService(IPermissionService PermissionService, IDataService dataService)
        {
            this.dataService = dataService;
            this.roles = new List<Role>();
            this.permissionService = PermissionService;
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
            if (this.permissionService.GetPermissions().Count().Equals(0))
            {
                this.permissionService.CreateTestData();
            }

            //TODO: turn it the other way round - link the owned role to the owning user to prevent circular reference
            this.roles.Add(new Role("TestRole", new List<Permission>() { this.permissionService.GetPermissionByName("User"), this.permissionService.GetPermissionByName("SuperHotFeature1"), this.permissionService.GetPermissionByName("SuperHotFeature2") }));
            this.roles.Add(new Role("AdminRole", new List<Permission>() { this.permissionService.GetPermissionByName("User"), this.permissionService.GetPermissionByName("GetUnknownLogins"), this.permissionService.GetPermissionByName("LinkLoginToUser"), this.permissionService.GetPermissionByName("LinkRoleToUser"), this.permissionService.GetPermissionByName("LinkPermissionToRole"), this.permissionService.GetPermissionByName("CreateUser"), this.permissionService.GetPermissionByName("CreateRole"), this.permissionService.GetPermissionByName("CreatePermission"), this.permissionService.GetPermissionByName("GetUsers"), this.permissionService.GetPermissionByName("GetRoles"), this.permissionService.GetPermissionByName("GetPermissions") }));
        }
        #endregion CreateTestData

        #region GetRoleByName
        /// <summary>
        /// returns a role by name
        /// </summary>
        /// <param name="Name">the name to be looked for</param>
        /// <returns>a user object or null, if not found</returns>
        public Role GetRoleByName(string Name)
        {
            return this.roles.Where(x => x.Name.Equals(Name)).FirstOrDefault();
        }
        #endregion GetRoleByName

        #region AddRole
        public bool AddRole(Role role)
        {
            bool result = false;
            if (!this.roles.Any(x => x.Name.Equals(role.Name)))
            {
                ((List<Role>)this.roles).Add(role);
                result = true;
            }
            return result;
        }
        #endregion AddRole

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
