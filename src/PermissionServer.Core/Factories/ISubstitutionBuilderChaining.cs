using PermissionServer.Models;

namespace PermissionServer.Core.Factories
{
	public interface ISubstitutionBuilderChaining
	{
		ISubstitutionBuilderEnding AddValidity(Validity Validity);
		ISubstitutionBuilderEnding IsInfinite(bool IsInfinite);
	}
}
