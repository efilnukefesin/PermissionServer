using PermissionServer.Models;

namespace PermissionServer.Core.Factories
{
	public interface IPermissionBuilderChaining
	{
		IPermissionBuilderChaining AddSomething(bool IsActive);
		Permission Build();
	}
}
