using Microsoft.AspNetCore.Mvc;
using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Models;
using PermissionServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionServer.Controllers
{
    public class OwnPermissionsController : PermissionController<Permission>
    {

        #region initializeData
        protected override void initializeData()
        {
            //TODO. load data?
        }
        #endregion initializeData

        #region GetAll
        public override ActionResult<SimpleResult<IEnumerable<Permission>>> GetAll()
        {
            return base.GetAll();
        }
        #endregion GetAll

        //#region UserPermissions: gets a list of permissions the user has
        ///// <summary>
        ///// gets a list of permissions the user has
        ///// </summary>
        ///// <returns>a list of permissions</returns>
        //[HttpGet("userpermissions")]
        //[Authorize(Policy = "Bearer")]
        //[Permit("UserPermissions")]
        //public SimpleResult<IEnumerable<Permission>> UserPermissions()
        //{
        //    SimpleResult<IEnumerable<Permission>> result = default;
        //    ClaimsPrincipal principal = HttpContext.User;

        //    //check permissions
        //    if (this.authorizeLocally())
        //    {
        //        string subjectId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
        //        IEnumerable<Permission> values = this.authenticationService.GetUserPermissions(subjectId);
        //        result = new SimpleResult<IEnumerable<Permission>>(values);
        //    }
        //    else
        //    {
        //        if (principal.Claims.Count() > 0)
        //        {
        //            string subjectId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
        //            this.authenticationService.AddUnkownLogin(subjectId);
        //        }
        //        result = new SimpleResult<IEnumerable<Permission>>(new ErrorInfo(3, "Not permitted"));
        //    }

        //    return result;
        //}
        //#endregion UserPermissions
    }
}
