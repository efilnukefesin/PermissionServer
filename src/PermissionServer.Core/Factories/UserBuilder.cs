using PermissionServer.Models;

namespace PermissionServer.Core.Factories
{
	public class UserBuilder : IUserBuilderChaining
	{
		// Instantiating functions

		public static IUserBuilderChaining CreateUser(string Name)
		{
			return new UserBuilder();
		}

		// Chaining functions

		public IUserBuilderChaining AddSubstitution(Substitution Substitution)
		{
			return this;
		}

		public IUserBuilderChaining AddOwnedRole(Role Role)
		{
			return this;
		}

		public IUserBuilderChaining AddRole(Role Role)
		{
			return this;
		}

		public IUserBuilderChaining AddLogin(Login Login)
		{
			return this;
		}

		// Executing functions

		public User Build()
		{
		}
	}
}
