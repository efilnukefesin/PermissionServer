using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PermissionServer.Models
{
    [DataContract]
    public class User : BaseObject
    {
        #region Properties

        [DataMember]
        public IEnumerable<Login> Logins { get; set; }

        [DataMember]
        public IEnumerable<Role> Roles { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public IEnumerable<Substitution> Substitutions { get; set; }

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
