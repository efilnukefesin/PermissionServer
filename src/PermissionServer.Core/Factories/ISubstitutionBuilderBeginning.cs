using PermissionServer.Models;

namespace PermissionServer.Core.Factories
{
	public interface ISubstitutionBuilderBeginning
	{
		ISubstitutionBuilderChaining AddUser(User User);
	}
}
