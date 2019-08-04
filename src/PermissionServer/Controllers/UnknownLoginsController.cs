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
    public class UnknownLoginsController : PermissionServerController<UnknownLogin>
    {
        #region Properties

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region GetAll
        [Authorize(Policy = "Bearer")]
        [Permit("GetUnknownLogins")]
        public override ActionResult<SimpleResult<IEnumerable<UnknownLogin>>> GetAll()
        {
            SimpleResult<IEnumerable<UnknownLogin>> result = default;
            //check permissions
            if (this.authorizeLocally())
            {
                IEnumerable<UnknownLogin> values = this.authenticationService.GetUnkownLogins().ToList();  //TODO: move to SDK
                result = new SimpleResult<IEnumerable<UnknownLogin>>(values);
            }
            else
            {
                result = new SimpleResult<IEnumerable<UnknownLogin>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion GetAll

        #endregion Methods
    }
}
