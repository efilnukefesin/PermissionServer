using NET.efilnukefesin.Contracts.Services.DataService;
using PermissionServer.Client;
using PermissionServer.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Base.SDK
{
    public abstract class BaseConsumingClient : BasePermissionClient
    {
        #region Properties

        protected ISessionService sessionService;
        private PermissionServer.SDK.Client permissionServerClient;

        #endregion Properties

        #region Construction

        public BaseConsumingClient(PermissionServer.SDK.Client PermissionServerClient, IDataService DataService, ISessionService SessionService) : base(DataService)
        {
            this.permissionServerClient = PermissionServerClient;
            this.sessionService = SessionService;
        }

        #endregion Construction

        #region Methods

        #region FetchPermissionsAndUserValues
        public async Task<bool> FetchPermissionsAndUserValues()
        {
            bool result = false;
            this.permissionServerClient.AddAuthenticationHeader(this.sessionService.AccessToken);
            bool hasFetchedPermissionsSuccessully = await permissionServerClient.FetchPermissions();
            bool hasFetchedUserValuesSuccessfully = await permissionServerClient.FetchUserValues();
            result = hasFetchedPermissionsSuccessully /*&& hasFetchedUserValuesSuccessfully*/;  //user values not important / necessary
            return result;
        }
        #endregion FetchPermissionsAndUserValues

        #endregion Methods

        #region Events

        #endregion Events
    }
}
