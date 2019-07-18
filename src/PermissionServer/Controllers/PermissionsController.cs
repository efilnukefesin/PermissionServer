using BootStrapper;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using NET.efilnukefesin.Implementations.Base;
using Newtonsoft.Json;
using PermissionServer.Client.Interfaces;
using PermissionServer.Core.Helpers;
using PermissionServer.Core.Interfaces;
using PermissionServer.Core.Services;
using PermissionServer.Models;
using PermissionServer.Server;
using PermissionServer.Server.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PermissionServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : PermissionController<Permission>
    {
        #region Properties

        private AuthenticationService authenticationService = DiHelper.GetService<AuthenticationService>();

        #endregion Properties

        #region Methods

        #region Get: returns a User which is associated with the current sub claim of the JWT token  
        /// <summary>
        /// returns a User which is associated with the current sub claim of the JWT token        
        /// </summary>
        /// <returns>a SimpleResult either having the User object as Payload or a descriptive error message.</returns>
        [HttpGet]
        [Authorize(Policy = "Bearer")]
        public ActionResult<SimpleResult<User>> Get()
        {
            SimpleResult<User> result = new SimpleResult<User>(new ErrorInfo(1, "Nothing happenend"));
            ClaimsPrincipal principal = HttpContext.User;
            string subjectId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = this.authenticationService.GetUser(subjectId);
            if (user != null)
            {
                result = new SimpleResult<User>(user);  //TODO: find stackoverflow exception, caused by probably wrongly-typed Result
            }
            else
            {
                // login is not known
                this.authenticationService.RegisterNewLogin(subjectId);
                result = new SimpleResult<User>(new ErrorInfo(2, "Login not known (yet)"));
            }
            return result;
        }
        #endregion Get

        #region Check: checks if a given subject / login has a given permission
        /// <summary>
        /// checks if a given subject / login has a given permission
        /// </summary>
        /// <param name="subjectid">the sub id of the log in data to check</param>
        /// <param name="permission">the permission name</param>
        /// <returns>true, if the sub may do what it asked for.</returns>
        [HttpGet("check/{subjectid}/{permission}")]
        [Authorize(Policy = "Bearer")]
        [Permit("User")]
        public ActionResult<SimpleResult<ValueObject<bool>>> Check(string subjectid, string permission)
        {
            SimpleResult<ValueObject<bool>> result = new SimpleResult<ValueObject<bool>>(new ErrorInfo(1, "Nothing happenend"));

            if (this.authorizeLocally())
            {
                bool questionResult = this.authenticationService.CheckPermission(subjectid, permission);
                result = new SimpleResult<ValueObject<bool>>(new ValueObject<bool>(questionResult));
            }

            return result;
        }
        #endregion Check

        #region GetUnknownLogins: gets a list of subs unknown but tried to log in
        /// <summary>
        /// gets a list of subs unknown but tried to log in
        /// </summary>
        /// <returns></returns>
        [HttpGet("getunknownlogins")]
        [Authorize(Policy = "Bearer")]
        [Permit("GetUnknownLogins")]
        public ActionResult<SimpleResult<List<UnknownLogin>>> GetUnknownLogins()
        {
            SimpleResult<List<UnknownLogin>> result = default;
            //check permissions
            if (this.authorizeLocally())
            {
                List<UnknownLogin> values = this.authenticationService.GetUnkownLogins().ToList();  //TODO: move to SDK
                result = new SimpleResult<List<UnknownLogin>>(values);
            }
            else
            {
                result = new SimpleResult<List<UnknownLogin>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion GetUnknownLogins

        #region LinkLoginToUser
        [HttpPost("linklogintouser")]
        [Authorize(Policy = "Bearer")]
        [Permit("LinkLoginToUser")]
        public SimpleResult<ValueObject<bool>> LinkLoginToUser()
        {
            SimpleResult<ValueObject<bool>> result = default;

            if (this.authorizeLocally())
            {
                //TODO: implement
            }

            return result;
        }
        #endregion LinkLoginToUser

        #region LinkRoleToUser
        [HttpPost("linkroletouser")]
        [Authorize(Policy = "Bearer")]
        [Permit("LinkRoleToUser")]
        public SimpleResult<ValueObject<bool>> LinkRoleToUser()
        {
            SimpleResult<ValueObject<bool>> result = default;

            if (this.authorizeLocally())
            {
                //TODO: implement
            }

            return result;
        }
        #endregion LinkRoleToUser

        #region LinkPermissionToRole
        [HttpPost("linkpermissiontorole")]
        [Authorize(Policy = "Bearer")]
        [Permit("LinkPermissionToRole")]
        public SimpleResult<ValueObject<bool>> LinkPermissionToRole()
        {
            SimpleResult<ValueObject<bool>> result = default;

            if (this.authorizeLocally())
            {
                //TODO: implement
            }

            return result;
        }
        #endregion LinkPermissionToRole

        #region AddUser: adds a user to the user store
        /// <summary>
        /// adds a user to the user store
        /// </summary>
        /// <param name="user">the user object to add</param>
        /// <returns>true, if the user was added successfully</returns>
        [HttpPost("adduser")]
        [Authorize(Policy = "Bearer")]
        [Permit("AddUser")]
        public SimpleResult<ValueObject<bool>> AddUser(/*[FromBody] User user*/)
        {
            //TODO: replace this workaround
            User user = default;
            using (var reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadToEndAsync();
                user = JsonConvert.DeserializeObject<User>(body.Result);
            }

            // here comes the real code
            SimpleResult<ValueObject<bool>> result = default;

            if (this.authorizeLocally())
            {
                bool wasSuccessful = this.authenticationService.AddOrUpdateUser(user);
                result = new SimpleResult<ValueObject<bool>>(new ValueObject<bool>(wasSuccessful));
            }
            else
            {
                result = new SimpleResult<ValueObject<bool>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion AddUser

        #region AddUnkownLogins: adds unknown logins to the store
        /// <summary>
        /// adds unknown logins to the store
        /// </summary>
        /// <returns>true, if the list was added successfully</returns>
        [HttpPost("unknownlogins")]
        [Authorize(Policy = "Bearer")]
        //[Permit("AddUnkownLogins")]
        public SimpleResult<ValueObject<bool>> AddUnkownLogins(/*[FromBody] User user*/)
        {
            //TODO: replace this workaround
            List<UnknownLogin> unknownLogins = default;
            using (var reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadToEndAsync();
                unknownLogins = JsonConvert.DeserializeObject<List<UnknownLogin>>(body.Result);
            }

            // here comes the real code
            SimpleResult<ValueObject<bool>> result = default;

            if (this.authorizeLocally())
            {
                bool wasSuccessful = this.authenticationService.AddUnkownLogins(unknownLogins);
                result = new SimpleResult<ValueObject<bool>>(new ValueObject<bool>(wasSuccessful));
            }
            else
            {
                result = new SimpleResult<ValueObject<bool>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion AddUnkownLogins

        #region DeleteUnkownLogin: adds unknown logins to the store
        /// <summary>
        /// adds unknown logins to the store
        /// </summary>
        /// <returns>true, if the list was added successfully</returns>
        [HttpDelete("unknownlogins/{Id}")]
        [Authorize(Policy = "Bearer")]
        //[Permit("AddUnkownLogins")]  //TODO: add permission and stuff
        public SimpleResult<ValueObject<bool>> DeleteUnkownLogin(string Id)
        {
            SimpleResult<ValueObject<bool>> result = default;

            if (this.authorizeLocally())
            {
                bool wasSuccessful = this.authenticationService.DeleteUnkownLogin(Id);
                result = new SimpleResult<ValueObject<bool>>(new ValueObject<bool>(wasSuccessful));
            }
            else
            {
                result = new SimpleResult<ValueObject<bool>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion DeleteUnkownLogin

        #region AddRole: adds a role to the role store
        /// <summary>
        /// adds a role to the role store
        /// </summary>
        /// <returns>true, if the role has been added successfully</returns>
        [HttpPost("addrole")]
        [Authorize(Policy = "Bearer")]
        [Permit("AddRole")]
        public SimpleResult<ValueObject<bool>> AddRole([FromBody] Role role)
        {
            SimpleResult<ValueObject<bool>> result = default;

            if (this.authorizeLocally())
            {
                bool wasSuccessful = this.authenticationService.AddRole(role);
                result = new SimpleResult<ValueObject<bool>>(new ValueObject<bool>(wasSuccessful));
            }
            else
            {
                result = new SimpleResult<ValueObject<bool>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion AddRole

        #region AddPermission: adds a permission to the permission store
        /// <summary>
        /// adds a permission to the permission store
        /// </summary>
        /// <returns>true, if the permission has been added successfully</returns>
        [HttpPost("addpermission")]
        [Authorize(Policy = "Bearer")]
        [Permit("AddPermission")]
        public SimpleResult<ValueObject<bool>> AddPermission(/*Permission permission*/)
        {
            //TODO: replace this workaround
            Permission permission = default;
            using (var reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadToEndAsync();
                permission = JsonConvert.DeserializeObject<Permission>(body.Result);
            }

            // here comes the real code
            SimpleResult<ValueObject<bool>> result = default;

            if (this.authorizeLocally())
            {
                bool wasSuccessful = this.authenticationService.AddPermission(permission);
                result = new SimpleResult<ValueObject<bool>>(new ValueObject<bool>(wasSuccessful));
            }
            else
            {
                result = new SimpleResult<ValueObject<bool>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion AddPermission

        #region GetUsers: gets the list of users in the user store
        /// <summary>
        /// gets the list of users in the user store
        /// </summary>
        /// <returns>a list of user objects</returns>
        [HttpGet("getusers")]
        [Authorize(Policy = "Bearer")]
        [Permit("GetUsers")]
        public SimpleResult<IEnumerable<User>> GetUsers()
        {
            SimpleResult<IEnumerable<User>> result = default;

            //check permissions
            if (this.authorizeLocally())
            {
                IEnumerable<User> values = this.authenticationService.GetUsers();
                result = new SimpleResult<IEnumerable<User>>(values);
            }
            else
            {
                result = new SimpleResult<IEnumerable<User>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion GetUsers

        #region GetRoles: gets a list of roles in the role store
        /// <summary>
        /// gets a list of roles in the role store
        /// </summary>
        /// <returns>a list of roles</returns>
        [HttpGet("getroles")]
        [Authorize(Policy = "Bearer")]
        [Permit("GetRoles")]
        public SimpleResult<IEnumerable<Role>> GetRoles()
        {
            SimpleResult<IEnumerable<Role>> result = default;

            //check permissions
            if (this.authorizeLocally())
            {
                IEnumerable<Role> values = this.authenticationService.GetRoles();
                result = new SimpleResult<IEnumerable<Role>>(values);
            }
            else
            {
                result = new SimpleResult<IEnumerable<Role>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion GetRoles

        #region GetPermissions: gets a list of permission in the permission store
        /// <summary>
        /// gets a list of permission in the permission store
        /// </summary>
        /// <returns>a list of permissions</returns>
        [HttpGet("getpermissions")]
        [Authorize(Policy = "Bearer")]
        [Permit("GetPermissions")]
        public SimpleResult<IEnumerable<Permission>> GetPermissions()
        {
            SimpleResult<IEnumerable<Permission>> result = default;

            //check permissions
            if (this.authorizeLocally())
            {
                IEnumerable<Permission> values = this.authenticationService.GetPermissions();
                result = new SimpleResult<IEnumerable<Permission>>(values);
            }
            else
            {
                result = new SimpleResult<IEnumerable<Permission>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion GetPermissions

        #region UserValues: gets a list of uservalues the user has
        /// <summary>
        /// gets a list of uservalues the user has
        /// </summary>
        /// <returns>a list of permissions</returns>
        [HttpGet("uservalues")]
        [Authorize(Policy = "Bearer")]
        [Permit("UserValues")]
        public SimpleResult<IEnumerable<UserValue>> UserValues()
        {
            SimpleResult<IEnumerable<UserValue>> result = default;

            //check permissions
            if (this.authorizeLocally())
            {
                ClaimsPrincipal principal = HttpContext.User;
                string subjectId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
                IEnumerable<UserValue> values = this.authenticationService.GetUserValues(subjectId);
                result = new SimpleResult<IEnumerable<UserValue>>(values);
            }
            else
            {
                result = new SimpleResult<IEnumerable<UserValue>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion GetPermissions

        #region UserPermissions: gets a list of permissions the user has
        /// <summary>
        /// gets a list of permissions the user has
        /// </summary>
        /// <returns>a list of permissions</returns>
        [HttpGet("userpermissions")]
        [Authorize(Policy = "Bearer")]
        [Permit("UserPermissions")]
        public SimpleResult<IEnumerable<Permission>> UserPermissions()
        {
            SimpleResult<IEnumerable<Permission>> result = default;
            ClaimsPrincipal principal = HttpContext.User;

            //check permissions
            if (this.authorizeLocally())
            {
                string subjectId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
                IEnumerable<Permission> values = this.authenticationService.GetUserPermissions(subjectId);
                result = new SimpleResult<IEnumerable<Permission>>(values);
            }
            else
            {
                if (principal.Claims.Count() > 0)
                {
                    string subjectId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
                    this.authenticationService.AddUnkownLogin(subjectId);
                }
                result = new SimpleResult<IEnumerable<Permission>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion UserPermissions

        #region authorizeLocally: does a local authorization (only possible on Permission Server itself) for performance's and dead lock's sake
        /// <summary>
        /// does a local authorization (only possible on Permission Server itself) for performance's and dead lock's sake
        /// </summary>
        /// <returns>true, if the user may execute the method, false otherwise</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        private bool authorizeLocally()
        {
            string permission = string.Empty;
            permission = this.getLastPermitAttribute();

            return this.authenticationService.CheckPermission(PrincipalHelper.ExtractSubjectId(HttpContext.User), permission);
        }
        #endregion authorizeLocally

        #endregion Methods
    }
}
