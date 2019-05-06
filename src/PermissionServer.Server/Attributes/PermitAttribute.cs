using System;
using System.Collections.Generic;
using System.Text;
using PermissionServer.Server.Args;

namespace PermissionServer.Server.Attributes
{
    public class PermitAttribute : MethodInterceptionAttribute
    {
        #region Properties

        public string PermissionName { get; set; }

        #endregion Properties

        #region Construction

        public PermitAttribute(string PermissionName)
        {
            this.PermissionName = PermissionName;
        }

        #endregion Construction

        #region Methods

        #region OnInvoke
        public override void OnInvoke(MethodArgs args)
        {
            //throw new NotImplementedException();
        }
        #endregion OnInvoke

        #endregion Methods
    }
}
