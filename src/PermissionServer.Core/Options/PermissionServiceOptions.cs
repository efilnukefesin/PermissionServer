using PermissionServer.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Core.Options
{
    public class PermissionServiceOptions
    {
        #region Properties

        public PermissionFlowType FlowType { get; set; } = PermissionFlowType.ServerSide;

        #endregion Properties
    }
}
