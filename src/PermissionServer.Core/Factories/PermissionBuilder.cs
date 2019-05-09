using PermissionServer.Models;

namespace PermissionServer.Core.Factories
{
	public class PermissionBuilder : IPermissionBuilderChaining
	{
		// Instantiating functions

		public static IPermissionBuilderChaining CreatePermission(string Name)
		{
			return new PermissionBuilder();
		}

		// Chaining functions

		public IPermissionBuilderChaining AddSomething(bool IsActive)
		{
			return this;
		}

		// Executing functions

		public Permission Build()
		{
		}
	}
}
