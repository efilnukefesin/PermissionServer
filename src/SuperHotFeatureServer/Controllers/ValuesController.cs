using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BootStrapper;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Client.Interfaces;
using PermissionServer.Server;
using PermissionServer.Server.Attributes;

namespace SuperHotFeatureServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : PermissionController<ValueObject<string>>
    {
        #region Get: Sample Endpoint
        /// <summary>
        /// Sample Endpoint
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "Bearer")]
        [Permit("SuperHotFeature01")]
        public ActionResult<SimpleResult<ValueObject<string>>> Get()
        {
            SimpleResult<ValueObject<string>> result = default;

            if (this.Authorize().GetAwaiter().GetResult())
            {
                result = new SimpleResult<ValueObject<string>>(new ValueObject<string>("Value")); 
            }
            else
            {
                result = new SimpleResult<ValueObject<string>>(new ErrorInfo(3, "Not permitted"));
            }
            return result;
        }
        #endregion Get
    }
}
