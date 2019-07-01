using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace PermissionServer.Models
{
    [DataContract]
    public class UnknownLogin : BaseObject
    {
        #region Properties

        [DataMember]
        public string SubjectId { get; set; }

        [IgnoreDataMember]
        [XmlIgnore]
        public DateTimeOffset SubmitTime { get; set; }

        [DataMember]
        public string SubmitTimeString
        {
            get { return this.SubmitTime.ToString(CultureInfo.InvariantCulture); }
            set { this.SubmitTime = DateTimeOffset.Parse(value, CultureInfo.InvariantCulture); }
        }

        #endregion Properties

        #region Construction

        public UnknownLogin(string subjectId)
        {
            this.SubjectId = subjectId;
            this.SubmitTime = DateTimeOffset.Now;
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
