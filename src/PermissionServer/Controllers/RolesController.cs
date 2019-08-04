using Microsoft.AspNetCore.Authorization;
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
    public class RolesController : PermissionServerController<Role>
    {
        #region Properties

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region GetAll
        [Authorize(Policy = "Bearer")]
        [Permit("GetRoles")]
        public override ActionResult<SimpleResult<IEnumerable<Role>>> GetAll()
        {
            SimpleResult<IEnumerable<Role>> result = default;

            //check permissions
            if (this.authorizeLocally())
            {
                IEnumerable<Role> values = this.authenticationService.GetRoles();
                result = new SimpleResult<IEnumerable<Role>>(values);
            }
            else
            {
                result = new SimpleResult<IEnumerable<Role>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion GetAll

        #endregion Methods
    }
}
