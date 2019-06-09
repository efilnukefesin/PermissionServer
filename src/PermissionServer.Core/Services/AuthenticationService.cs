using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Core.Interfaces;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Core.Services
{
    public class AuthenticationService : BaseObject
    {
        #region Properties

        private IUserService userService;
        private IRoleService roleService;
        private IPermissionService permissionService;

        #endregion Properties

        #region Construction

        public AuthenticationService(IUserService UserService, IRoleService RoleService, IPermissionService PermissionService)
        {
            this.userService = UserService;
            this.roleService = RoleService;
            this.permissionService = PermissionService;
            this.userService.CreateTestData();  //TODO: delete
        }

        #endregion Construction

        #region Methods

        #region GetUser: Gets a User by subject id - if not found, adds it to the new logins
        /// <summary>
        /// Gets a User by subject id - if not found, adds it to the new logins
        /// </summary>
        /// <param name="subjectId">the subject id of the user login</param>
        /// <returns>the user, or, if not found, null</returns>
        public User GetUser(string subjectId)
        {
            User result = this.userService.GetUserBySubject(subjectId);
            if (result == null)
            {
                this.RegisterNewLogin(subjectId, null);
            }
            return result;
        }
        #endregion GetUser

        #region RegisterNewLogin: registers a new log in
        /// <summary>
        /// registers a new log in
        /// </summary>
        /// <param name="subjectId">the subject which is obviously not known (yet)</param>
        /// /// <param name="Email">a potential hint for linking</param>
        public void RegisterNewLogin(string subjectId, string Email)
        {
            this.userService.RegisterNewLogin(subjectId, Email);
        }
        #endregion RegisterNewLogin

        #region CheckPermission
        public bool CheckPermission(string subjectid, string permission)
        {
            bool result = this.userService.CheckPermission(subjectid, permission);
            return result;
        }
        #endregion CheckPermission

        #region GetUnkownLogins
        public IEnumerable<Tuple<string, string>> GetUnkownLogins()
        {
            return this.userService.UnknownLogins;
        }
        #endregion GetUnkownLogins

        #region GetUsers
        public IEnumerable<User> GetUsers()
        {
            return this.userService.GetUsers();
        }
        #endregion GetUsers

        #region GetRoles
        public IEnumerable<Role> GetRoles()
        {
            return this.roleService.GetRoles();
        }
        #endregion GetRoles

        #region GetPermissions
        public IEnumerable<Permission> GetPermissions()
        {
            return this.permissionService.GetPermissions();
        }
        #endregion GetPermissions

        #region AddUser
        public bool AddUser(User user)
        {
            return this.userService.AddUser(user);
        }
        #endregion AddUser

        #region AddRole
        public bool AddRole(Role role)
        {
            return this.roleService.AddRole(role);
        }
        #endregion AddRole

        #region GetUserValues
        public IEnumerable<UserValue> GetUserValues(string subjectId)
        {
            IEnumerable<UserValue> result = default;
            User tempUser = this.userService.GetUserBySubject(subjectId);
            if (tempUser != null)
            {
                result = tempUser.Values;
            }
            return result;
        }
        #endregion GetUserValues

        #region AddPermission
        public bool AddPermission(Permission permission)
        {
            return this.permissionService.AddPermission(permission);
        }
        #endregion AddPermission

        #region dispose
        protected override void dispose()
        {
            this.userService = null;
            this.roleService = null;
            this.permissionService = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
