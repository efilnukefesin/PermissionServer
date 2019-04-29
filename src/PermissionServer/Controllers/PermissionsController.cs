using BootStrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using PermissionServer.Core.Interfaces;
using PermissionServer.Core.Services;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        #region GetPermissions
        //https://localhost:44318/api/permissions/GetPermissions
        [HttpGet]
        public ActionResult<SimpleResult<object>> Get()
        {
            SimpleResult<object> result = new SimpleResult<object>("Nothing happenend");
            IHeaderDictionary requestHeaders = Request.Headers;
            if (requestHeaders.ContainsKey("Authorization"))
            {
                //TODO: do Token validation
                //TODO: extract Subject Id from token and pass to permissionService
                string subjectId = "88421113";
                User user = permissionService.GetUser(subjectId);
                result = new SimpleResult<object>(user); 
            }
            else
            {
                result = new SimpleResult<object>("User not identified!");
            }
            return result;
        }
        #endregion GetPermissions

        #endregion Methods
    }
}
