using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Models
{
    public class User : BaseObject
    {
        #region Properties

        public IEnumerable<Login> Logins { get; set; }
        public IEnumerable<Role> Roles { get; set; }

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            this.Logins = null;
            this.Roles = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
