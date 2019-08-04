using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Core.Interfaces;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            this.userService.Initialize();
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
                this.RegisterNewLogin(subjectId);
            }
            return result;
        }
        #endregion GetUser

        #region RegisterNewLogin: registers a new log in
        /// <summary>
        /// registers a new log in
        /// </summary>
        /// <param name="subjectId">the subject which is obviously not known (yet)</param>
        public void RegisterNewLogin(string subjectId)
        {
            this.userService.RegisterNewLogin(subjectId);
        }
        #endregion RegisterNewLogin

        #region CheckPermission
        public bool CheckPermission(string subjectid, string permission)
        {
            bool result = this.userService.CheckPermission(subjectid, permission);
            return result;
        }
        #endregion CheckPermission

        #region GetUnkownLogins: returns all unkown logins
        /// <summary>
        /// returns all unkown logins
        /// </summary>
        /// <returns>a list of unkown logins</returns>
        public IEnumerable<UnknownLogin> GetUnkownLogins()
        {
            //transform into valueobjects
            foreach (UnknownLogin login in this.userService.UnknownLogins)
            {
                yield return login;
            }
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

        #region AddOrUpdateUser
        public bool AddOrUpdateUser(User user)
        {
            return this.userService.AddOrUpdateUser(user);
        }
        #endregion AddOrUpdateUser

        #region UserExists
        public bool UserExists(Guid Id)
        {
            bool result = false;

            if (this.userService.GetUsers().Any(x => x.Id.Equals(Id)))
            {
                result = true;
            }

            return result;
        }
        #endregion UserExists

        #region AddUnkownLogins
        public bool AddUnkownLogins(List<UnknownLogin> unknownLogins)
        {
            bool result = true;
            foreach (UnknownLogin unknownLogin in unknownLogins)
            {
                result &= this.addUnkownLogin(unknownLogin);
            }
            return result;
        }
        #endregion AddUnkownLogins

        #region DeleteUnkownLogin
        public bool DeleteUnkownLogin(string id)
        {
            return this.userService.DeleteUnknownLogin(id);
        }
        #endregion DeleteUnkownLogin

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

        #region GetUserPermissions
        public IEnumerable<Permission> GetUserPermissions(string subjectId)
        {
            List<Permission> result = new List<Permission>();
            User tempUser = this.userService.GetUserBySubject(subjectId);
            if (tempUser != null)
            {
                foreach (Role role in tempUser.Roles)
                {
                    result.AddRange(role.Permissions);
                }
            }
            return result;
        }
        #endregion GetUserPermissions

        #region AddUnkownLogin
        public void AddUnkownLogin(string subjectId)
        {
            this.addUnkownLogin(new UnknownLogin(subjectId));
        }
        #endregion AddUnkownLogin

        #region addUnkownLogin
        private bool addUnkownLogin(UnknownLogin unknownLogin)
        {
            return this.userService.AddUnknownLogin(unknownLogin);
        }
        #endregion addUnkownLogin

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
