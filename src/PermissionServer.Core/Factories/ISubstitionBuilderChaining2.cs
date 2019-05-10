using PermissionServer.Models;

namespace PermissionServer.Core.Factories
{
	public interface ISubstitionBuilderChaining2
	{
		ISubstitionBuilderEnd IsInfinite();
		ISubstitionBuilderEnd SetValidity(Validity Validity);
	}
}
