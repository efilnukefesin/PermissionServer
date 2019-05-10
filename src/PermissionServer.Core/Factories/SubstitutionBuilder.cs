using PermissionServer.Models;

namespace PermissionServer.Core.Factories
{
	public class SubstitutionBuilder : ISubstitionBuilderStart, ISubstitionBuilderChaining1, ISubstitionBuilderChaining2, ISubstitionBuilderEnd
	{
        #region Properties

        private Validity validity;
        private User source;
        private User target;

        #endregion Properties

        #region Methods
        // Instantiating functions

        #region CreateSubstitution
        public static ISubstitionBuilderStart CreateSubstitution()
		{
			return new SubstitutionBuilder();
		}
        #endregion CreateSubstitution

        // Chaining functions

        #region SetSource
        public ISubstitionBuilderChaining1 SetSource(User Source)
		{
            this.source = Source;
			return this;
		}
        #endregion SetSource

        #region SetTarget
        public ISubstitionBuilderChaining2 SetTarget(User Target)
		{
            this.target = Target;
			return this;
		}
        #endregion SetTarget

        #region SetValidity
        public ISubstitionBuilderEnd SetValidity(Validity Validity)
		{
            this.validity = Validity;
			return this;
		}
        #endregion SetValidity

        // Executing functions
        #region Build

        public Substitution Build()
		{
            Substitution result = new Substitution(this.source, this.target, this.validity);
            return result;
		}
        #endregion Build

        #endregion Methods
    }
}
