using PermissionServer.Models;

namespace PermissionServer.Core.Factories
{
	public interface IPolicyBuilderBeginning
	{
		IPolicyBuilderChaining AddPermission(Permission Permission);
		IPolicyBuilderChaining AddSomethingElse(int Number);
	}
}
