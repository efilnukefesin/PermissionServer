﻿using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PermissionServer.Models
{
    [DataContract]
    public class Permission : BaseObject
    {
        #region Properties

        [DataMember]
        public string Name { get; set; }

        #endregion Properties

        #region Construction

        public Permission() : base()
        {

        }

        public Permission(string Name) : base()
        {
            this.Name = Name;
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
         
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
