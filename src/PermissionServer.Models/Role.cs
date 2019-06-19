using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PermissionServer.Models
{
    [DataContract]
    public class Role : BaseObject
    {
        #region Properties

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public IEnumerable<Permission> Permissions { get; set; }

        #endregion Properties

        #region Construction

        public Role(string Name, IEnumerable<Permission> Permissions, IEnumerable<Role> Roles = null)
        {
            this.Name = Name;
            this.Permissions = Permissions;
        }
        #endregion Construction

        #region Methods

        #region AddPermission
        public void AddPermission(Permission permission)
        {
            if (this.Permissions is null)
            {
                this.Permissions = new List<Permission>();
            }

            ((List<Permission>)this.Permissions).Add(permission);
        }
        #endregion AddPermission

        #region dispose
        protected override void dispose()
        {
            this.Permissions = null;
        }

        public void AddRole(Permission permission)
        {
            throw new NotImplementedException();
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
