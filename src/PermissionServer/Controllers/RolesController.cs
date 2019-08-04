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

        #region AddRole: adds a role to the role store
        /// <summary>
        /// adds a role to the role store
        /// </summary>
        /// <returns>true, if the role has been added successfully</returns>
        [HttpPost("addrole")]
        [Authorize(Policy = "Bearer")]
        [Permit("AddRole")]
        public SimpleResult<ValueObject<bool>> AddRole([FromBody] Role role)
        {
            SimpleResult<ValueObject<bool>> result = default;

            if (this.authorizeLocally())
            {
                bool wasSuccessful = this.authenticationService.AddRole(role);
                result = new SimpleResult<ValueObject<bool>>(new ValueObject<bool>(wasSuccessful));
            }
            else
            {
                result = new SimpleResult<ValueObject<bool>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion AddRole
    }
}
