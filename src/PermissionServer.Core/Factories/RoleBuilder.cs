using PermissionServer.Models;
using System.Collections.Generic;

namespace PermissionServer.Core.Factories
{
	public class RoleBuilder : IRoleBeginning, IRoleChaining
	{
        #region Properties

        private Role result;

        #endregion Properties

        #region Construction

        public RoleBuilder(string Name)
        {
            this.result = new Role(Name, new List<Permission>(), new List<Role>());
        }

        #endregion Construction

        #region Methods

        // Instantiating functions
        #region CreateRole
        public static IRoleBeginning CreateRole(string Name)
        {
            return new RoleBuilder(Name);
        }
        #endregion CreateRole

        // Chaining functions
        #region AddPermission
        public IRoleChaining AddPermission(Permission Permission)
        {
            this.result.AddPermission(Permission);
            return this;
        }
        #endregion AddPermission

        // Executing functions
        #region Build
        public Role Build()
        {
            return this.result;
        }
        #endregion Build

        #endregion Methods
    }
}
