using PermissionServer.Models;

namespace PermissionServer.Core.Factories
{
	public interface ISubstitionBuilderStart
	{
		ISubstitionBuilderChaining1 SetSource(User Source);
	}
}
