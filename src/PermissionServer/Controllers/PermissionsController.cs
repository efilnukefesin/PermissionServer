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

        //#region Get
        ////https://localhost:6000/api/permissions/
        //[HttpGet]
        //public ActionResult<SimpleResult<User>> Get()
        //{
        //    SimpleResult<User> result = new SimpleResult<User>("Nothing happenend");
        //    IHeaderDictionary requestHeaders = Request.Headers;
        //    if (requestHeaders.ContainsKey("Authorization"))
        //    {
        //        //TODO: do Token validation
        //        //TODO: extract Subject Id from token and pass to permissionService
        //        string subjectId = "88421113";
        //        User user = permissionService.GetUser(subjectId);
        //        //result = new SimpleResult<User>(user);  //TODO: find stackoverflow exception, caused by probably wrongly-typed Result
        //        result = new SimpleResult<User>(new User("TestBob"));  //TODO: find stackoverflow exception, caused by probably wrongly-typed Result
        //    }
        //    else
        //    {
        //        result = new SimpleResult<User>("User not identified!");
        //    }
        //    return result;
        //}
        //#endregion Get

        #region Get
        //https://localhost:6000/api/permissions/
        [HttpGet]
        public ActionResult<SimpleResult<User>> Get()
        {
            SimpleResult<User> result = new SimpleResult<User>(new ErrorInfo(1, "Nothing happenend"));
            IHeaderDictionary requestHeaders = Request.Headers;
            if (requestHeaders.ContainsKey("Authorization"))
            {
                //TODO: do Token validation
                //TODO: extract Subject Id from token and pass to permissionService
                string subjectId = "88421113";
                User user = permissionService.GetUser(subjectId);
                result = new SimpleResult<User>(user);  //TODO: find stackoverflow exception, caused by probably wrongly-typed Result
            }
            else
            {
                result = new SimpleResult<User>(new ErrorInfo(1, "User not identified!"));
            }
            return result;
        }
        #endregion Get

        #endregion Methods
    }
}
