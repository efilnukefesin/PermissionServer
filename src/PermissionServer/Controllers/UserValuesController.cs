﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Models;
using PermissionServer.Server;
using PermissionServer.Server.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionServer.Controllers
{
    public class UserValuesController : PermissionController<UserValue>
    {
        #region initializeData
        protected override void initializeData()
        {
            //TODO. load data?
        }
        #endregion initializeData

        #region GetAll
        [Authorize(Policy = "Bearer")]
        [Permit("UserValues")]
        public override ActionResult<SimpleResult<IEnumerable<UserValue>>> GetAll()
        {
            return base.GetAll();
        }
        #endregion GetAll

        //#region UserValues: gets a list of uservalues the user has
        ///// <summary>
        ///// gets a list of uservalues the user has
        ///// </summary>
        ///// <returns>a list of permissions</returns>
        //[HttpGet("uservalues")]
        //[Authorize(Policy = "Bearer")]
        //[Permit("UserValues")]
        //public SimpleResult<IEnumerable<UserValue>> UserValues()
        //{
        //    SimpleResult<IEnumerable<UserValue>> result = default;

        //    //check permissions
        //    if (this.authorizeLocally())
        //    {
        //        ClaimsPrincipal principal = HttpContext.User;
        //        string subjectId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
        //        IEnumerable<UserValue> values = this.authenticationService.GetUserValues(subjectId);
        //        result = new SimpleResult<IEnumerable<UserValue>>(values);
        //    }
        //    else
        //    {
        //        result = new SimpleResult<IEnumerable<UserValue>>(new ErrorInfo(3, "Not permitted"));
        //    }

        //    return result;
        //}
        //#endregion UserValues
    }
}