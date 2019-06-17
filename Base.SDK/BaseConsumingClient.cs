using NET.efilnukefesin.Contracts.Services.DataService;
using PermissionServer.Client;
using PermissionServer.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.SDK
{
    public abstract class BaseConsumingClient : BaseClient
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

        #endregion Methods

        #region Events

        #endregion Events
    }
}
