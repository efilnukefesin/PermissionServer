using BootStrapper;
using Microsoft.AspNetCore.Mvc;
using NET.efilnukefesin.Contracts.Base;
using PermissionServer.Core.Helpers;
using PermissionServer.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Server
{
    public abstract class PermissionServerController<T> : PermissionController<T> where T : IBaseObject
    {
        #region Properties

        protected AuthenticationService authenticationService = DiHelper.GetService<AuthenticationService>();

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region authorizeLocally: does a local authorization (only possible on Permission Server itself) for performance's and dead lock's sake
        /// <summary>
        /// does a local authorization (only possible on Permission Server itself) for performance's and dead lock's sake
        /// </summary>
        /// <returns>true, if the user may execute the method, false otherwise</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected bool authorizeLocally()
        {
            string permission = string.Empty;
            permission = this.getLastPermitAttribute();

            return this.authenticationService.CheckPermission(PrincipalHelper.ExtractSubjectId(HttpContext.User), permission);
        }
        #endregion authorizeLocally

        #endregion Methods
    }
}
