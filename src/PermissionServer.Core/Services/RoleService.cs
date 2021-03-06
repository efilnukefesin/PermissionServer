﻿using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Core.Interfaces;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionServer.Core.Services
{
    public class RoleService : BaseObject, IRoleService
    {
        #region Properties

        private List<Role> roles;
        private IPermissionService permissionService;
        protected IDataService dataService;
        public bool IsInitialized { get; set; } = false;

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

        #region Initialize
        public async Task<bool> Initialize()
        {
            bool result = false;

            ((List<Role>)this.roles).Clear();

            await this.permissionService.Initialize();  //TODO: do nicer, integrate in result and stuff

            try
            {
                this.roles = new List<Role>(await this.dataService.GetAllAsync<Role>("PermissionServer.Core.Services.RoleService.Store"));
                result = true;
            }
            catch (Exception ex)
            {
                //TODO: react
            }

            this.IsInitialized = result;
            return result;
        }
        #endregion Initialize

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
            this.roles.Add(new Role("TestRole", new List<Permission>() { this.permissionService.GetPermissionByName("User"), this.permissionService.GetPermissionByName("UserValues"), this.permissionService.GetPermissionByName("SuperHotFeature1"), this.permissionService.GetPermissionByName("SuperHotFeature2"), this.permissionService.GetPermissionByName("UserPermissions") }));
            this.roles.Add(new Role("AdminRole", new List<Permission>() { this.permissionService.GetPermissionByName("User"), this.permissionService.GetPermissionByName("UserValues"), this.permissionService.GetPermissionByName("GetUnknownLogins"), this.permissionService.GetPermissionByName("LinkLoginToUser"), this.permissionService.GetPermissionByName("LinkRoleToUser"), this.permissionService.GetPermissionByName("LinkPermissionToRole"), this.permissionService.GetPermissionByName("CreateUser"), this.permissionService.GetPermissionByName("CreateRole"), this.permissionService.GetPermissionByName("CreatePermission"), this.permissionService.GetPermissionByName("GetUsers"), this.permissionService.GetPermissionByName("GetRoles"), this.permissionService.GetPermissionByName("GetPermissions"), this.permissionService.GetPermissionByName("EditPermissions"), this.permissionService.GetPermissionByName("UserPermissions"), this.permissionService.GetPermissionByName("AddPermission") }));
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
                //store Role
                this.dataService.CreateOrUpdateAsync<Role>("PermissionServer.Core.Services.RoleService.Store", role);
                result = true;
            }
            return result;
        }
        #endregion AddRole

        #region Clear
        public void Clear()
        {
            ((List<Role>)this.roles).Clear();
            this.permissionService.Clear();
        }
        #endregion Clear

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