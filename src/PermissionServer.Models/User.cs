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

        [DataMember]
        public bool IsSystem{ get; set; }

        #endregion Properties

        #region Construction

        public User(string Name, bool IsSystem = false)
        {
            this.Name = Name;
            this.IsSystem = IsSystem;
            this.Logins = new List<Login>();
            this.Roles = new List<Role>();
            this.Substitutions = new List<Substitution>();
        }

        #endregion Construction

        #region Methods

        #region AddLogin
        public void AddLogin(Login login)
        {
            ((List<Login>)this.Logins).Add(login);
        }
        #endregion AddLogin

        #region AddRole
        public void AddRole(Role role)
        {
            ((List<Role>)this.Roles).Add(role);
        }
        #endregion AddRole

        #region AddSubstitution
        public void AddSubstitution(Substitution substitution)
        {
            ((List<Substitution>)this.Substitutions).Add(substitution);
        }
        #endregion AddSubstitution

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
