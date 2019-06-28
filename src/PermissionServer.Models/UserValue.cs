using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PermissionServer.Models
{
    [DataContract]
    public class UserValue : BaseObject
    {
        #region Properties

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public object Data { get; set; }

        #endregion Properties

        #region Construction

        public UserValue(string Name, object Data)
            : base()
        {
            this.Name = Name;
            this.Data = Data;
        }
        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            this.Data = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
