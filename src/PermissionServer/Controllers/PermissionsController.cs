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
    public class PermissionsController : PermissionServerController<Permission>
    {
        #region Methods

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

        #region GetAll
        [Authorize(Policy = "Bearer")]
        [Permit("GetPermissions")]
        public override ActionResult<SimpleResult<IEnumerable<Permission>>> GetAll()
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
        #endregion GetAll

        #endregion Methods
    }
}
