using BootStrapper;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
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
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PermissionServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : PermissionController
    {
        #region Properties

        private AuthenticationService permissionService = DiHelper.GetService<AuthenticationService>();

        #endregion Properties

        #region Methods

        #region Get
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
            User user = this.permissionService.GetUser(subjectId);
            if (user != null)
            {
                result = new SimpleResult<User>(user);  //TODO: find stackoverflow exception, caused by probably wrongly-typed Result
            }
            else
            {
                // login is not known
                this.permissionService.RegisterNewLogin(subjectId, principal.FindFirst(ClaimTypes.Email).Value);
                result = new SimpleResult<User>(new ErrorInfo(2, "Login not known (yet)"));
            }
            return result;
        }
        #endregion Get

        #region Check
        [HttpGet("check/{subjectid}/{permission}")]
        [Authorize(Policy = "Bearer")]
        [Permit("User")]
        public ActionResult<SimpleResult<bool>> Check(string subjectid, string permission)
        {
            SimpleResult<bool> result = new SimpleResult<bool>(new ErrorInfo(1, "Nothing happenend"));

            if (this.authorizeLocally())
            {
                bool questionResult = this.permissionService.CheckPermission(subjectid, permission);
                result = new SimpleResult<bool>(questionResult);
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
        public ActionResult<SimpleResult<List<Tuple<string, string>>>> GetUnknownLogins()
        {
            SimpleResult<List<Tuple<string, string>>> result = default(SimpleResult<List<Tuple<string, string>>>);
            //check permissions
            if (this.Authorize())
            {
                List<Tuple<string, string>> values = this.permissionService.GetUnkownLogins().ToList();  //TODO: move to SDK
                result = new SimpleResult<List<Tuple<string, string>>>(values);
            }
            else
            {
                result = new SimpleResult<List<Tuple<string, string>>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion GetUnknownLogins

        #region LinkLoginToUser
        [HttpPost("linklogintouser")]
        [Authorize(Policy = "Bearer")]
        [Permit("LinkLoginToUser")]
        public SimpleResult<bool> LinkLoginToUser()
        {
            SimpleResult<bool> result = default(SimpleResult<bool>);

            return result;
        }
        #endregion LinkLoginToUser

        #region LinkRoleToUser
        [HttpPost("linkroletouser")]
        [Authorize(Policy = "Bearer")]
        [Permit("LinkRoleToUser")]
        public SimpleResult<bool> LinkRoleToUser()
        {
            SimpleResult<bool> result = default(SimpleResult<bool>);

            return result;
        }
        #endregion LinkRoleToUser

        #region LinkPermissionToRole
        [HttpPost("linkpermissiontorole")]
        [Authorize(Policy = "Bearer")]
        [Permit("LinkPermissionToRole")]
        public SimpleResult<bool> LinkPermissionToRole()
        {
            SimpleResult<bool> result = default(SimpleResult<bool>);

            return result;
        }
        #endregion LinkPermissionToRole

        #region CreateUser
        [HttpPost("createuser")]
        [Authorize(Policy = "Bearer")]
        [Permit("CreateUser")]
        public SimpleResult<bool> CreateUser()
        {
            SimpleResult<bool> result = default(SimpleResult<bool>);

            return result;
        }
        #endregion CreateUser

        #region CreateRole
        [HttpPost("createrole")]
        [Authorize(Policy = "Bearer")]
        [Permit("CreateRole")]
        public SimpleResult<bool> CreateRole()
        {
            SimpleResult<bool> result = default(SimpleResult<bool>);

            return result;
        }
        #endregion CreateRole

        #region CreatePermission
        [HttpPost("createpermission")]
        [Authorize(Policy = "Bearer")]
        [Permit("CreatePermission")]
        public SimpleResult<bool> CreatePermission()
        {
            SimpleResult<bool> result = default(SimpleResult<bool>);

            return result;
        }
        #endregion CreatePermission

        #region GetUsers
        [HttpGet("getusers")]
        [Authorize(Policy = "Bearer")]
        [Permit("GetUsers")]
        public SimpleResult<IEnumerable<User>> GetUsers()
        {
            SimpleResult<IEnumerable<User>> result = default(SimpleResult<IEnumerable<User>>);

            //check permissions
            if (this.Authorize())
            {
                IEnumerable<User> values = this.permissionService.GetUsers();
                result = new SimpleResult<IEnumerable<User>>(values);
            }
            else
            {
                result = new SimpleResult<IEnumerable<User>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion GetUsers

        #region GetRoles
        [HttpGet("getroles")]
        [Authorize(Policy = "Bearer")]
        [Permit("GetRoles")]
        public SimpleResult<IEnumerable<Role>> GetRoles()
        {
            SimpleResult<IEnumerable<Role>> result = default(SimpleResult<IEnumerable<Role>>);

            //check permissions
            if (this.Authorize())
            {
                IEnumerable<Role> values = this.permissionService.GetRoles();
                result = new SimpleResult<IEnumerable<Role>>(values);
            }
            else
            {
                result = new SimpleResult<IEnumerable<Role>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion GetRoles

        #region GetPermissions
        [HttpGet("getpermissions")]
        [Authorize(Policy = "Bearer")]
        [Permit("GetPermissions")]
        public SimpleResult<IEnumerable<Permission>> GetPermissions()
        {
            SimpleResult<IEnumerable<Permission>> result = default(SimpleResult<IEnumerable<Permission>>);

            //check permissions
            if (this.Authorize())
            {
                IEnumerable<Permission> values = this.permissionService.GetPermissions();
                result = new SimpleResult<IEnumerable<Permission>>(values);
            }
            else
            {
                result = new SimpleResult<IEnumerable<Permission>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion GetPermissions

        #region authorizeLocally
        private bool authorizeLocally()
        {
            string permission = string.Empty;

            MethodBase method = new StackFrame(1).GetMethod();  //TODO: magic number - trouble expected; 5 is the number for non-tasked return values; 8 for task return values. Hrmpf.
            foreach (Attribute customAttribute in method.GetCustomAttributes(true))
            {
                PermitAttribute permitAttribute = customAttribute as PermitAttribute;
                if (permitAttribute != null)
                {
                    permission = permitAttribute.PermissionName;
                }
            }
            
            return this.permissionService.CheckPermission(PrincipalHelper.ExtractSubjectId(HttpContext.User), permission);
        }
        #endregion authorizeLocally

        #endregion Methods
    }
}
