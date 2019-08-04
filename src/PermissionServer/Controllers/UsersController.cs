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

        #region Post
        [Authorize(Policy = "Bearer")]
        [Permit("AddUser")]
        public override ActionResult Post([FromBody] User newContent)
        {
            ActionResult result = default;

            if (this.authorizeLocally())
            {
                bool wasSuccessful = this.authenticationService.AddOrUpdateUser(newContent);
                if (wasSuccessful)
                {
                    result = CreatedAtAction(nameof(this.Get), new { id = newContent.Id }, newContent);
                }
                else
                {
                    result = new BadRequestResult();
                }
            }
            else
            {
                result = new ForbidResult();
            }

            return result;
        }
        #endregion Post

        #region Head
        [Authorize(Policy = "Bearer")]
        //[Permit("AddUser")]
        public override ActionResult Head(Guid Id)
        {
            ActionResult result = default;

            if (this.authorizeLocally())
            {
                if (this.authenticationService.UserExists(Id))
                {
                    result = Ok();
                }
                else
                {
                    result = NotFound();
                }
            }
            else
            {
                result = new ForbidResult();
            }

            return result;
        }
        #endregion Head

        #region Put
        [Authorize(Policy = "Bearer")]
        [Permit("AddUser")]
        public override ActionResult Put(Guid Id, [FromBody] User updatedContent)
        {
            ActionResult result = default;

            if (this.authorizeLocally())
            {
                bool wasSuccessful = this.authenticationService.AddOrUpdateUser(updatedContent);
                if (wasSuccessful)
                {
                    result = AcceptedAtAction(nameof(this.Get), new { id = updatedContent.Id }, updatedContent);
                }
                else
                {
                    result = new BadRequestResult();
                }
            }
            else
            {
                result = new ForbidResult();
            }

            return result;
        }
        #endregion Put

        #endregion Methods

        //#region AddUser: adds a user to the user store
        ///// <summary>
        ///// adds a user to the user store
        ///// </summary>
        ///// <param name="user">the user object to add</param>
        ///// <returns>true, if the user was added successfully</returns>
        //[HttpPost("adduser")]
        //[Authorize(Policy = "Bearer")]
        //[Permit("AddUser")]
        //public SimpleResult<ValueObject<bool>> AddUser(/*[FromBody] User user*/)
        //{
        //    //TODO: replace this workaround
        //    User user = default;
        //    using (var reader = new StreamReader(Request.Body))
        //    {
        //        var body = reader.ReadToEndAsync();
        //        user = JsonConvert.DeserializeObject<User>(body.Result);
        //    }

        //    // here comes the real code
        //    SimpleResult<ValueObject<bool>> result = default;

        //    if (this.authorizeLocally())
        //    {
        //        bool wasSuccessful = this.authenticationService.AddOrUpdateUser(user);
        //        result = new SimpleResult<ValueObject<bool>>(new ValueObject<bool>(wasSuccessful));
        //    }
        //    else
        //    {
        //        result = new SimpleResult<ValueObject<bool>>(new ErrorInfo(3, "Not permitted"));
        //    }

        //    return result;
        //}
        //#endregion AddUser
    }
}
