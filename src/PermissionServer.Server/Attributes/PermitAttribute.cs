using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Server.Attributes
{
    public class PermitAttribute : Attribute
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

        #endregion Methods
    }
}
