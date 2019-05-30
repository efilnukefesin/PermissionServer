using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PermissionServer.Models
{
    [DataContract]
    public class Policy : BaseObject
    {
        #region Properties

        public string Name { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }

        #endregion Properties

        #region Construction

        public Policy(string Name)
            : base()
        {
            this.Name = Name;
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            this.Permissions = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
