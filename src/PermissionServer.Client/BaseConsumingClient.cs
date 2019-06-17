using NET.efilnukefesin.Contracts.Services.DataService;
using PermissionServer.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Client
{
    public abstract class BaseConsumingClient : BaseClient
    {
        #region Properties

        protected ISessionService sessionService;

        #endregion Properties

        #region Construction

        public BaseConsumingClient(IDataService DataService, ISessionService SessionService) : base(DataService)
        {
            this.sessionService = SessionService;
        }

        #endregion Construction

        #region Methods

        #endregion Methods

        #region Events

        #endregion Events
    }
}
