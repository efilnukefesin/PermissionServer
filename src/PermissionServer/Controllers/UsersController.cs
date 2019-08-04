using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Models;
using PermissionServer.Server;
using PermissionServer.Server.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        #region Get
        [Authorize(Policy = "Bearer")]
        public override ActionResult<SimpleResult<User>> Get(Guid Id)
        {
            //TODO: how to differ between "get my own user" or "get any user"? -> Permission?
            SimpleResult<User> result = new SimpleResult<User>(new ErrorInfo(1, "Nothing happenend"));
            if (Id.Equals(Guid.Empty))
            {
                ClaimsPrincipal principal = HttpContext.User;
                string subjectId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
                User user = this.authenticationService.GetUser(subjectId);
                if (user != null)
                {
                    result = new SimpleResult<User>(user);  //TODO: find stackoverflow exception, caused by probably wrongly-typed Result
                }
                else
                {
                    // login is not known
                    this.authenticationService.RegisterNewLogin(subjectId);
                    result = new SimpleResult<User>(new ErrorInfo(2, "Login not known (yet)"));
                }
            }
            else
            {
                /***/
            }
            return result;
        }
        #endregion Get

        #endregion Methods
    }
}
