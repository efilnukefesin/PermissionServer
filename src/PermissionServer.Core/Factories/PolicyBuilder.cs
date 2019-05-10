using PermissionServer.Models;

namespace PermissionServer.Core.Factories
{
	public class PolicyBuilder : IPolicyBuilderBeginning, IPolicyBuilderChaining
	{
		// Instantiating functions

		public static IPolicyBuilderBeginning CreatePolicy(string Name)
		{
			return new PolicyBuilder();
		}

		// Chaining functions

		public IPolicyBuilderChaining AddPermission(Permission Permission)
		{
			return this;
		}

		public IPolicyBuilderChaining AddSomethingElse(int Number)
		{
			return this;
		}

		// Executing functions

		public Policy Build()
		{
            //TODO: implement
            return null;
		}
	}
}
