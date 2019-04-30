using Interfaces;
using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Client.Interfaces;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Client.Services
{
    public class PermissionClientService :  BaseObject, IPermissionClientService
    {
        #region Properties

        private IRestService restService;
        private IConfigurationService configurationService;
        private ISessionService sessionService;

        #endregion Properties

        #region Construction

        public PermissionClientService(IRestService RestService, IConfigurationService ConfigurationService, ISessionService SessionService)
        {
            this.restService = RestService;
            this.configurationService = ConfigurationService;
            this.sessionService = SessionService;
        }

        #endregion Construction

        #region Methods

        #region FetchPermissions
        public bool FetchPermissions()
        {
            bool result = false;

            this.restService.AddAuthenticationHeader(this.sessionService?.AccessToken);
            var userRequestResult = this.restService.GetUser(this.configurationService.PermissionGetEndpoint);
            if (userRequestResult is User)
            {
                this.sessionService.SetUser((userRequestResult as User));
                result = true;
            }
            else if (userRequestResult is string)
            {
                //TODO: show error
            }
            else
            {
                //TODO: dunno
            }
            return result;
        }
        #endregion FetchPermissions

        #region CheckPermission
        public bool CheckPermission(string Token, string SubjectId, string Permission)
        {
            bool result = false;

            this.restService.AddAuthenticationHeader(Token);
            var permissionRequestResult = this.restService.GetPermission(this.configurationService.PermissionCheckEndpoint, SubjectId, Permission);
            if (permissionRequestResult is bool)
            {
                result = (bool)permissionRequestResult;
            }
            else if (permissionRequestResult is string)
            {
                //TODO: show error
            }
            else
            {
                //TODO: dunno
            }
            return result;
        }
        #endregion CheckPermission

        #region dispose
        protected override void dispose()
        {
            //TODO: implement
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
