using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PermissionServer.Models
{
    [DataContract]
    public class UnknownLogin : BaseObject
    {
        #region Properties

        [DataMember]
        public string SubjectId { get; set; }

        [DataMember]
        public DateTime SubmitTime { get; set; }

        #endregion Properties

        #region Construction

        public UnknownLogin(string subjectId)
        {
            this.SubjectId = subjectId;
            this.SubmitTime = DateTime.Now;
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
