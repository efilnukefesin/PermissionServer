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
    public class UsersController : PermissionServerController<User>
    {
        #region Properties

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region GetAll
        [Authorize(Policy = "Bearer")]
        [Permit("GetUsers")]
        public override ActionResult<SimpleResult<IEnumerable<User>>> GetAll()
        {
            SimpleResult<IEnumerable<User>> result = default;

            //check permissions
            if (this.authorizeLocally())
            {
                IEnumerable<User> values = this.authenticationService.GetUsers();
                result = new SimpleResult<IEnumerable<User>>(values);
            }
            else
            {
                result = new SimpleResult<IEnumerable<User>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion GetAll

        #endregion Methods
    }
}
