using PermissionServer.Models;

namespace PermissionServer.Core.Factories
{
	public interface IUserBuilderChaining
	{
		IUserBuilderChaining AddSubstitution(Substitution Substitution);
		IUserBuilderChaining AddOwnedRole(Role Role);
		IUserBuilderChaining AddRole(Role Role);
		IUserBuilderChaining AddLogin(Login Login);
		User Build();
	}
}
