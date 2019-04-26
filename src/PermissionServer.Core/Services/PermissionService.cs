using PermissionServer.Core.Options;
using PermissionServer.Core.Strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Core.Services
{
    public class PermissionService
    {
        #region Properties

        private IPermissionFlowStrategy permissionFlowStrategy;

        #endregion Properties

        #region Construction

        public PermissionService(PermissionServiceOptions options = null)
        {
            if (options == null)
            {
                options = new PermissionServiceOptions();  //init to default values
            }

            switch (options.FlowType)
            {
                case Enums.PermissionFlowType.ClientSide:
                    this.permissionFlowStrategy = new ClientSidePermissionFlowStrategy();
                    break;
                case Enums.PermissionFlowType.ServerSide:
                    this.permissionFlowStrategy = new ServerSidePermissionFlowStrategy();
                    break;
                default:
                    break;
            }
        }

        #endregion Construction

        #region Methods

        #endregion Methods

        #region Events

        #endregion Events
    }
}
