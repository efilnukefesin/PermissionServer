using PermissionServer.Models;

namespace PermissionServer.Core.Factories
{
	public class RoleBuilder : IRoleBeginning, IRoleChaining
	{
		// Instantiating functions

		public static IRoleBeginning CreateRole(string Name)
		{
			return new RoleBuilder();
		}

		// Chaining functions

		public IRoleChaining AddPermission(Permission Permission)
		{
			return this;
		}

		public IRoleChaining AddRole(Role Role)
		{
			return this;
		}

		// Executing functions

		public Role Build()
		{
		}
	}
}
