using Microsoft.AspNetCore.Mvc;
using Models;
using PermissionServer.Core.Services;
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

        private PermissionService permissionService = new PermissionService(options => { options.FlowType = Core.Enums.PermissionFlowType.ServerSide; });

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #endregion Methods

        #region Events

        #endregion Events
        //// GET api/values
        ////https://localhost:44318/api/permissions
        //[HttpGet]
        //public ActionResult<SimpleResult<string>> Test()
        //{
        //    return new SimpleResult<string>("Hello World", false);
        //}

        #region GetPermissions
        //https://localhost:44318/api/permissions/GetPermissions
        [HttpGet]
        public ActionResult<SimpleResult<string>> GetPermissions(string Token)
        {
            var requestHeaders = Request.Headers; ;
            return new SimpleResult<string>("Hello World", false);
        }
        #endregion GetPermissions
    }
}
