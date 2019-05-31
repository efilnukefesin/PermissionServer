using PermissionServer.Models;
using System.Collections.Generic;

namespace PermissionServer.Core.Factories
{
	public interface IUserBuilderChaining
	{
		IUserBuilderChaining AddSubstitution(Substitution Substitution);
		IUserBuilderChaining AddOwnedRole(Role Role);
		IUserBuilderChaining AddRole(Role Role);
		IUserBuilderChaining AddLogin(Login Login);
        IUserBuilderChaining AddValues(IEnumerable<UserValue> Values);
        IUserBuilderChaining AddValue(UserValue Value);

        User Build();
	}
}
