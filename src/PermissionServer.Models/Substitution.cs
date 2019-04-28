﻿using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PermissionServer.Models
{
    [DataContract]
    public class Substitution : BaseObject
    {
        #region Properties

        [DataMember]
        public Validity Validity { get; set; }

        [DataMember]
        public User Source { get; set; }

        [DataMember]
        public User Target { get; set; }

        #endregion Properties

        #region Construction

        public Substitution(User source, User target, Validity validity)
        {
            this.Source = source;
            this.Target = target;
            this.Validity = validity;
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            this.Validity = null;
            this.Source = null;
            this.Target = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
