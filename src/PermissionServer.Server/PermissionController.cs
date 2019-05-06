using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Server
{
    public abstract class PermissionController : ControllerBase
    {
        //TODO: add helper to consolidate permission overviews
        //TODO: declare common end points for common tasks
        #region Properties

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region GivenPermissions
        [HttpGet("givenpermissions")]
        [Authorize(Policy = "Bearer")]
        public ActionResult<SimpleResult<IEnumerable<Permission>>> GivenPermissions()
        {
            SimpleResult<IEnumerable<Permission>> result = default(SimpleResult<IEnumerable<Permission>>);

            //TODO: do list
            var dummyList = new List<Permission>();
            dummyList.Add(new Permission() { Name = "Dummy"});
            result = new SimpleResult<IEnumerable<Permission>>(dummyList);

            return result;
        }

        #endregion GivenPermissions

        #endregion Methods
    }
}
