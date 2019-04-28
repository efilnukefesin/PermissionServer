using Interfaces;
using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Client.Interfaces;
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
            this.restService.AddAuthenticationHeader(this.sessionService?.AccessToken);
            var x = this.restService.Get(this.configurationService.PermissionGetEndpoint);
            return false;
        }
        #endregion FetchPermissions

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
