using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PermissionServer.Models
{
    [DataContract]
    public class Validity : BaseObject
    {
        #region Properties

        [DataMember]
        public bool IsInifinite { get; set; }

        [DataMember]
        public DateTimeOffset? From { get; set; }

        [DataMember]
        public DateTimeOffset? To { get; set; }

        #endregion Properties

        #region Construction

        public Validity()
        {
            this.IsInifinite = true;
        }

        public Validity(DateTimeOffset From, DateTimeOffset To)
        {
            this.IsInifinite = false;
            this.From = From;
            this.To = To;
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            this.From = null;
            this.To = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
