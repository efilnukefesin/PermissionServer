using Interfaces;
using PermissionServer.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Client.Services
{
    public class PermissionClientService : IPermissionClientService
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

        #endregion Methods

        #region Events

        #endregion Events
    }
}
