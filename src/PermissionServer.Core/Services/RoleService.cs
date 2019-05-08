using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Core.Interfaces;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Core.Services
{
    public class RoleService : BaseObject, IRoleService
    {
        #region Properties

        private List<Role> roles;
        private IPermissionService permissionService;
        private IUserService userService;

        #endregion Properties

        #region Construction

        public RoleService(IPermissionService PermissionService, IUserService UserService)
        {
            this.roles = new List<Role>();
            this.permissionService = PermissionService;
            this.userService = UserService;
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
            string adminUserName = "Admin";

            this.roles.Add(new Role("TestRole", new List<User>() { this.userService.GetUserByName(adminUserName) }, null));
            this.roles.Add(new Role("AdminRole", new List<User>() { this.userService.GetUserByName(adminUserName) }, null));

            //Role roleTest = new Role("TestRole", new List<User>() { this.userService.GetUserByName("Admin") }, new List<Permission>() { permissionSuperHotFeature1, permissionSuperHotFeature2, permissionUser });
            //Role roleAdmin = new Role("AdminRole", new List<User>() { this.userService.GetUserByName("Admin") }, new List<Permission>() { permissionUser, permissionGetUnknownLogins, permissionLinkLoginToUser, permissionLinkRoleToUser, permissionLinkPermissionToRole, permissionCreateUser, permissionCreateRole, permissionCreatePermission, permissionGetUsers, permissionGetRoles, permissionGetPermissions });

            throw new NotImplementedException();
        }
        #endregion CreateTestData

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
