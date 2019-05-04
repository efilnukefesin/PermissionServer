using BootStrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using PermissionServer.Client.Interfaces;
using PermissionServer.Core.Interfaces;
using PermissionServer.Core.Services;
using PermissionServer.Models;
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
    public class PermissionsController : ControllerBase
    {
        #region Properties

        private PermissionService permissionService = DiHelper.GetService<PermissionService>();
        private IPermissionClientService permissionClientService = DiHelper.GetService<IPermissionClientService>();

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
                this.permissionService.RegisterNewLogin(subjectId);
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
        public ActionResult<SimpleResult<List<string>>> GetUnknownLogins()
        {
            SimpleResult<List<string>> result = default(SimpleResult<List<string>>);
            //TODO: check permissions
            ClaimsPrincipal principal = HttpContext.User;

            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string subjectId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (this.permissionClientService.CheckPermission(token, subjectId, "AdminUsers"))
            {
                List<string> values = this.permissionService.GetUnkownLogins().ToList();
                result = new SimpleResult<List<string>>(values);
            }
            else
            {
                result = new SimpleResult<List<string>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion GetUnknownLogins

        #endregion Methods
    }
}
