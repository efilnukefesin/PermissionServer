using PermissionServer.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperHotFeatureServer.SDK
{
    public class Client : BaseClient
    {
        #region Properties

        #endregion Properties

        #region Construction

        public Client(Uri BaseUrl) : base(BaseUrl)
        {
        }

        #endregion Construction

        #region Methods

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
