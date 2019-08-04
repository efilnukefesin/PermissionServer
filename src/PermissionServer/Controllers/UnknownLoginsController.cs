using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NET.efilnukefesin.Implementations.Base;
using Newtonsoft.Json;
using PermissionServer.Models;
using PermissionServer.Server;
using PermissionServer.Server.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionServer.Controllers
{
    public class UnknownLoginsController : PermissionServerController<UnknownLogin>
    {
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

        #region AddUnkownLogins: adds unknown logins to the store
        /// <summary>
        /// adds unknown logins to the store
        /// </summary>
        /// <returns>true, if the list was added successfully</returns>
        [HttpPost("unknownlogins")]
        [Authorize(Policy = "Bearer")]
        //[Permit("AddUnkownLogins")]
        public SimpleResult<ValueObject<bool>> AddUnkownLogins([FromBody] UnknownLogin unknownLogin)  //TOSO: check if list?
        {
            //TODO: replace this workaround
            List<UnknownLogin> unknownLogins = default;
            using (var reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadToEndAsync();
                unknownLogins = JsonConvert.DeserializeObject<List<UnknownLogin>>(body.Result);
            }

            // here comes the real code
            SimpleResult<ValueObject<bool>> result = default;

            if (this.authorizeLocally())
            {
                bool wasSuccessful = this.authenticationService.AddUnkownLogins(unknownLogins);
                result = new SimpleResult<ValueObject<bool>>(new ValueObject<bool>(wasSuccessful));
            }
            else
            {
                result = new SimpleResult<ValueObject<bool>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion AddUnkownLogins

        #region DeleteUnkownLogin: adds unknown logins to the store
        /// <summary>
        /// adds unknown logins to the store
        /// </summary>
        /// <returns>true, if the list was added successfully</returns>
        [HttpDelete("unknownlogins/{Id}")]
        [Authorize(Policy = "Bearer")]
        //[Permit("AddUnkownLogins")]  //TODO: add permission and stuff
        public SimpleResult<ValueObject<bool>> DeleteUnkownLogin(string Id)
        {
            SimpleResult<ValueObject<bool>> result = default;

            if (this.authorizeLocally())
            {
                bool wasSuccessful = this.authenticationService.DeleteUnkownLogin(Id);
                result = new SimpleResult<ValueObject<bool>>(new ValueObject<bool>(wasSuccessful));
            }
            else
            {
                result = new SimpleResult<ValueObject<bool>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion DeleteUnkownLogin
    }
}
