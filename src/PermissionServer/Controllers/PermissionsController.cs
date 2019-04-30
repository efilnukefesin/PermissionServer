using BootStrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
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

        private PermissionService permissionService = new PermissionService(options => { options.FlowType = Core.Enums.PermissionFlowType.ServerSide; }, DiHelper.GetService<IUserService>());

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region Get
        //https://localhost:6000/api/permissions/
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

        #endregion Methods
    }
}
