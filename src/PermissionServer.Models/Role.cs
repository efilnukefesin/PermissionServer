using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PermissionServer.Models
{
    [DataContract]
    public class Role : BaseObject
    {
        #region Properties

        public string Name { get; set; }
        public IEnumerable<User> Owners { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            this.Owners = null;
            this.Permissions = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
