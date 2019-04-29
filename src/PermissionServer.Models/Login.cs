using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace PermissionServer.Models
{
    [DataContract]
    public class Login : BaseObject
    {
        #region Properties

        [Key]
        [DataMember]
        public string SubjectId { get; set; }

        //[DataMember]
        //public User Parent { get; set; }

        #endregion Properties

        #region Construction

        public Login(string SubjectId/*, User Parent*/)
        {
            this.SubjectId = SubjectId;
            //this.Parent = Parent;
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
