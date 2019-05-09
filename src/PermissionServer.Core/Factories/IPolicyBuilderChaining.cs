using PermissionServer.Models;

namespace PermissionServer.Core.Factories
{
	public interface IPolicyBuilderChaining
	{
		IPolicyBuilderChaining AddPermission(Permission Permission);
		IPolicyBuilderChaining AddSomethingElse(int Number);
		Policy Build();
	}
}
