using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        // GET api/values
        //https://localhost:44318/api/permissions
        [HttpGet]
        public ActionResult<SimpleResult<string>> Test()
        {
            return new SimpleResult<string>("Hello World", false);
        }
    }
}
