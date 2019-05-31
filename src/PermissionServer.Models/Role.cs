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

        public string Name { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
        public IEnumerable<Role> Roles { get; set; }

        #endregion Properties

        #region Construction

        public Role(string Name, IEnumerable<Permission> Permissions, IEnumerable<Role> Roles = null)
        {
            this.Name = Name;
            this.Permissions = Permissions;
            this.Roles = Roles;

            if (this.Roles == null)
            {
                this.Roles = new List<Role>();
            }
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

        #region AddRole
        public void AddRole(Role role)
        {
            if (this.Roles is null)
            {
                this.Roles = new List<Role>();
            }

            ((List<Role>)this.Roles).Add(role);
        }
        #endregion AddRole

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
