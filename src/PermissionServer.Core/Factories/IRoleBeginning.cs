using PermissionServer.Models;

namespace PermissionServer.Core.Factories
{
	public interface IRoleBeginning
	{
		IRoleChaining AddPermission(Permission Permission);
		IRoleChaining AddRole(Role Role);
	}
}
