using PermissionServer.Models;

namespace PermissionServer.Core.Factories
{
	public interface IRoleChaining
	{
		IRoleChaining AddPermission(Permission Permission);
		Role Build();
	}
}
