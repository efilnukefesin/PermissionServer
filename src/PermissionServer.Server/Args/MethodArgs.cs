using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PermissionServer.Server.Args
{
    public class MethodArgs : EventArgs
    {
        #region Properties

        public MethodBase Method { get; set; }
        public PermissionController Controller { get; set; }

        #endregion Properties

        #region Construction

        public MethodArgs(MethodBase Method, PermissionController Controller)
        {
            this.Method = Method;
            this.Controller = Controller;
        }

        #endregion Construction

        #region Methods

        #endregion Methods
    }
}
