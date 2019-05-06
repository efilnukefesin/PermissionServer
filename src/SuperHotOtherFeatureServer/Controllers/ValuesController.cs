﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BootStrapper;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using PermissionServer.Client.Interfaces;
using PermissionServer.Server;
using PermissionServer.Server.Attributes;

namespace SuperHotOtherFeatureServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : PermissionController
    {
        #region Properties
        private PermissionServer.SDK.Client permissionServerClient = DiHelper.GetService<PermissionServer.SDK.Client>(DiHelper.GetService<IConfigurationService>().PermissionServerEndpoint);
        #endregion Properties

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
        [Permit("SuperHotFeature2")]
        public ActionResult<SimpleResult<string>> Get()
        {
            SimpleResult<string> result = default(SimpleResult<string>);

            if (this.permissionServerClient.CheckPermissionAsync(HttpContext.Request.Headers["Authorization"], HttpContext.User, "SuperHotFeature2").Result)
            {
                result = new SimpleResult<string>("OtherValue");
            }
            else
            {
                result = new SimpleResult<string>(new ErrorInfo(3, "Not permitted"));
            }
            return result;
        }
        #endregion Get

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
