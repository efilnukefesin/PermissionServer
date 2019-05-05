using BootStrapper;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using PermissionServer.Client.Interfaces;
using PermissionServer.Core.Interfaces;
using PermissionServer.Core.Services;
using PermissionServer.Models;
using PermissionServer.Server;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PermissionServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : PermissionController
    {
        #region Properties

        private PermissionService permissionService = DiHelper.GetService<PermissionService>();
        private PermissionServer.SDK.Client permissionServerClient = DiHelper.GetService<PermissionServer.SDK.Client>(DiHelper.GetService<IConfigurationService>().PermissionServerEndpoint);

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
        public ActionResult<SimpleResult<bool>> Check(string subjectid, string permission)
        {
            SimpleResult<bool> result = new SimpleResult<bool>(new ErrorInfo(1, "Nothing happenend"));

            bool questionResult = this.permissionService.CheckPermission(subjectid, permission);
            result = new SimpleResult<bool>(questionResult);

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
        public ActionResult<SimpleResult<List<Tuple<string, string>>>> GetUnknownLogins()
        {
            SimpleResult<List<Tuple<string, string>>> result = default(SimpleResult<List<Tuple<string, string>>>);
            //check permissions
            if (this.permissionServerClient.CheckPermissionAsync(HttpContext.Request.Headers["Authorization"], HttpContext.User, "GetUnknownLogins").Result)
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
        public SimpleResult<bool> LinkLoginToUser()
        {
            SimpleResult<bool> result = default(SimpleResult<bool>);

            return result;
        }
        #endregion LinkLoginToUser

        #region LinkRoleToUser
        [HttpPost("linkroletouser")]
        [Authorize(Policy = "Bearer")]
        public SimpleResult<bool> LinkRoleToUser()
        {
            SimpleResult<bool> result = default(SimpleResult<bool>);

            return result;
        }
        #endregion LinkRoleToUser

        #region LinkPermissionToRole
        [HttpPost("linkpermissiontorole")]
        [Authorize(Policy = "Bearer")]
        public SimpleResult<bool> LinkPermissionToRole()
        {
            SimpleResult<bool> result = default(SimpleResult<bool>);

            return result;
        }
        #endregion LinkPermissionToRole

        #region CreateUser
        [HttpPost("createuser")]
        [Authorize(Policy = "Bearer")]
        public SimpleResult<bool> CreateUser()
        {
            SimpleResult<bool> result = default(SimpleResult<bool>);

            return result;
        }
        #endregion CreateUser

        #region CreateRole
        [HttpPost("createrole")]
        [Authorize(Policy = "Bearer")]
        public SimpleResult<bool> CreateRole()
        {
            SimpleResult<bool> result = default(SimpleResult<bool>);

            return result;
        }
        #endregion CreateRole

        #region CreatePermission
        [HttpPost("createpermission")]
        [Authorize(Policy = "Bearer")]
        public SimpleResult<bool> CreatePermission()
        {
            SimpleResult<bool> result = default(SimpleResult<bool>);

            return result;
        }
        #endregion CreatePermission

        #region GetUsers
        [HttpGet("getusers")]
        [Authorize(Policy = "Bearer")]
        public SimpleResult<IEnumerable<User>> GetUsers()
        {
            SimpleResult<IEnumerable<User>> result = default(SimpleResult<IEnumerable<User>>);

            return result;
        }
        #endregion GetUsers

        #region GetRoles
        [HttpGet("getroles")]
        [Authorize(Policy = "Bearer")]
        public SimpleResult<IEnumerable<Role>> GetRoles()
        {
            SimpleResult<IEnumerable<Role>> result = default(SimpleResult<IEnumerable<Role>>);

            return result;
        }
        #endregion GetRoles

        #region GetPermissions
        [HttpGet("getpermissions")]
        [Authorize(Policy = "Bearer")]
        public SimpleResult<IEnumerable<Permission>> GetPermissions()
        {
            SimpleResult<IEnumerable<Permission>> result = default(SimpleResult<IEnumerable<Permission>>);

            return result;
        }
        #endregion GetPermissions

        #endregion Methods
    }
}
