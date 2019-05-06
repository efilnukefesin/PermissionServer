using BootStrapper;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using PermissionServer.Core.Helpers;
using PermissionServer.Core.Interfaces;
using PermissionServer.Models;
using PermissionServer.Server.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PermissionServer.Server
{
    public abstract class PermissionController : ControllerBase
    {
        //TODO: add helper to consolidate permission overviews
        //TODO: declare common end points for common tasks
        #region Properties

        protected PermissionServer.SDK.Client permissionServerClient = DiHelper.GetService<PermissionServer.SDK.Client>(DiHelper.GetService<IConfigurationService>().PermissionServerEndpoint);

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region GivenPermissions
        [HttpGet("givenpermissions")]
        [Authorize(Policy = "Bearer")]
        [Permit("User")]
        public ActionResult<SimpleResult<IEnumerable<Permission>>> GivenPermissions()
        {
            SimpleResult<IEnumerable<Permission>> result = default(SimpleResult<IEnumerable<Permission>>);

            if (this.permissionServerClient.CheckPermissionAsync(HttpContext.Request.Headers["Authorization"], HttpContext.User, "User").Result)
            {

                IEnumerable<Permission> evaluation = this.getAllPermissionsOnController();

                //now, ask for each Permission if the specific user has it
                IUserService userService = DiHelper.GetService<IUserService>();

                string sub = PrincipalHelper.ExtractSubjectId(HttpContext.User);

                List<Permission> resultPayload = new List<Permission>();
                foreach (Permission permission in evaluation)
                {
                    if (userService.CheckPermission(sub, permission.Name))
                    {
                        resultPayload.Add(permission);
                    }
                }

                result = new SimpleResult<IEnumerable<Permission>>(resultPayload);
            }
            else
            {
                result = new SimpleResult<IEnumerable<Permission>>(new ErrorInfo(3, "Not permitted"));
            }

            return result;
        }
        #endregion GivenPermissions

        #region evaluatePermissions
        private IEnumerable<Permission> getAllPermissionsOnController()
        {
            List<Permission> result = new List<Permission>();

            // Loop through all properties
            foreach (MethodInfo method in this.GetType().GetMethods())
            {
                // for every property loop through all attributes
                foreach (Attribute customAttribute in method.GetCustomAttributes(true))
                {
                    PermitAttribute permitAttribute = customAttribute as PermitAttribute;
                    if (permitAttribute != null)
                    {
                        if (!result.Any(x => x.Name.Equals(permitAttribute.PermissionName)))
                        {
                            result.Add(new Permission() { Name = permitAttribute.PermissionName });
                        }
                    }
                }
            }

            return result;
        }
        #endregion evaluatePermissions

        #endregion Methods
    }
}
