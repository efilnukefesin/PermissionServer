﻿using BootStrapper;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Core.Helpers;
using PermissionServer.Core.Interfaces;
using PermissionServer.Models;
using PermissionServer.Server.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PermissionServer.Server
{
    public abstract class PermissionController : ControllerBase
    {
        #region Properties

        protected PermissionServer.SDK.Client permissionServerClient = DiHelper.GetService<PermissionServer.SDK.Client>();

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region GivenPermissions
        [HttpGet("givenpermissions")]
        [Authorize(Policy = "Bearer")]
        [Permit("User")]
        public async Task<ActionResult<SimpleResult<IEnumerable<Permission>>>> GivenPermissions()
        {
            SimpleResult<IEnumerable<Permission>> result = default;

            if (await this.Authorize())
            {
                IEnumerable<Permission> evaluation = this.getAllPermissionsOnController();

                //now, ask for each Permission if the specific user has it
                string sub = PrincipalHelper.ExtractSubjectId(HttpContext.User);

                List<Permission> resultPayload = new List<Permission>();
                foreach (Permission permission in evaluation)
                {
                    if (await this.permissionServerClient.CheckPermissionAsync(sub, permission.Name))
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
        [ApiExplorerSettings(IgnoreApi = true)]
        private IEnumerable<Permission> getAllPermissionsOnController()
        {
            List<Permission> result = new List<Permission>();

            // Loop through all properties
            foreach (MethodInfo method in this.GetType().GetMethods())
            {
                // for every property loop through all attributes
                foreach (Attribute customAttribute in method.GetCustomAttributes(true))
                {
                    if (customAttribute is PermitAttribute permitAttribute)
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

        #region getLastPermitAttribute
        protected string getLastPermitAttribute()
        {
            string result = string.Empty;
            int maxIteration = 15;
            for (int i = 0; i < maxIteration; i++)
            {
                MethodBase method = new StackFrame(i).GetMethod();
                foreach (Attribute customAttribute in method.GetCustomAttributes(true))
                {
                    if (customAttribute is PermitAttribute permitAttribute)
                    {
                        result = permitAttribute.PermissionName;
                        break;
                    }
                }
            }
            return result;
        }
        #endregion getLastPermitAttribute

        #region authorizeAsync
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> authorizeAsync()
        {
            bool result = false;

            string permission = string.Empty;
            permission = this.getLastPermitAttribute();

            if (!string.IsNullOrWhiteSpace(permission))
            {
                result = await this.permissionServerClient.CheckPermissionAsync(HttpContext.Request.Headers["Authorization"], HttpContext.User, permission);
            }

            return result;
        }
        #endregion authorizeAsync

        #region Authorize
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> Authorize()
        {
            return await this.authorizeAsync();
        }
        #endregion Authorize

        #endregion Methods
    }
}
