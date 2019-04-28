﻿using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Core.Options;
using PermissionServer.Core.Strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Core.Services
{
    public class PermissionService : BaseObject
    {
        #region Properties

        private IPermissionFlowStrategy permissionFlowStrategy;
        private PermissionServiceOptions config;

        #endregion Properties

        #region Construction

        public PermissionService(Action<PermissionServiceOptions> options)
        {
            this.config = new PermissionServiceOptions();
            if (options != null)
            {
                options(this.config);
            }

            switch (this.config.FlowType)
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
