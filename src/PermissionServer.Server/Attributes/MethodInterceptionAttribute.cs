using PermissionServer.Server.Args;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Server.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    //TODO: IF this works, place it in a lib
    public abstract class MethodInterceptionAttribute : Attribute
    {
        #region Properties

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        public abstract void OnInvoke(MethodArgs args);

        #endregion Methods

        #region Events

        #endregion Events
    }
}
