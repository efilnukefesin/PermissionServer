using PermissionServer.Models;

namespace PermissionServer.Core.Factories
{
	public class SubstitutionBuilder : ISubstitutionBuilderBeginning, ISubstitutionBuilderChaining, ISubstitutionBuilderEnding
	{
		// Instantiating functions

		public static ISubstitutionBuilderBeginning CreateSubstitution()
		{
			return new SubstitutionBuilder();
		}

		// Chaining functions

		public ISubstitutionBuilderChaining AddUser(User User)
		{
			return this;
		}

		public ISubstitutionBuilderEnding AddValidity(Validity Validity)
		{
			return this;
		}

		public ISubstitutionBuilderEnding IsInfinite(bool IsInfinite)
		{
			return this;
		}

		// Executing functions

		public Substitution Build()
		{
		}
	}
}
