using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<Role> OwnedRoles { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public IEnumerable<Substitution> Substitutions { get; set; }

        [DataMember]
        public bool IsSystem{ get; set; }

        [DataMember]
        public IEnumerable<UserValue> Values { get; set; }

        #endregion Properties

        #region Construction

        public User(string Name, bool IsSystem = false)
        {
            this.Name = Name;
            this.IsSystem = IsSystem;
            this.Logins = new List<Login>();
            this.Roles = new List<Role>();
            this.OwnedRoles = new List<Role>();
            this.Substitutions = new List<Substitution>();
            this.Values = new List<UserValue>();
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

        #region AddOwnedRole
        public void AddOwnedRole(Role role)
        {
            ((List<Role>)this.OwnedRoles).Add(role);
        }
        #endregion AddOwnedRole

        #region AddValue
        public void AddValue(string Name, object Data)
        {
            ((List<UserValue>)this.Values).Add(new UserValue(Name, Data));
        }
        #endregion AddValue

        #region AddValue
        public void AddValue(UserValue Value)
        {
            ((List<UserValue>)this.Values).Add(Value);
        }
        #endregion AddValue

        #region GetValue: returns a vlaue from the value list
        /// <summary>
        /// returns a vlaue from the value list
        /// </summary>
        /// <param name="ValueName">The Value Name</param>
        /// <returns>the Value or null if not found</returns>
        public UserValue GetValue(string ValueName)
        {
            return this.Values?.FirstOrDefault(x => x.Name.Equals(ValueName));
        }
        #endregion GetValue

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
