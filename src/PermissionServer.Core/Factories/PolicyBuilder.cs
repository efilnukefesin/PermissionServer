using PermissionServer.Models;

namespace PermissionServer.Core.Factories
{
	public class PolicyBuilder : IPolicyBuilderBeginning, IPolicyBuilderChaining
	{
        #region Properties

        private Policy result;

        #endregion Properties

        #region Construction

        public PolicyBuilder(string Name)
        {
            this.result = new Policy(Name);
        }

        #endregion Construction

        #region Methods

        // Instantiating functions
        #region CreatePolicy
        public static IPolicyBuilderBeginning CreatePolicy(string Name)
        {
            return new PolicyBuilder(Name);
        }
        #endregion CreatePolicy

        // Chaining functions

        public IPolicyBuilderChaining AddPermission(Permission Permission)
        {
            result.AddPermission(Permission);
            return this;
        }

        public IPolicyBuilderChaining AddSomethingElse(int Number)
        {
            //TODO: implement
            return this;
        }

        // Executing functions
        #region Build
        public Policy Build()
        {
            return this.result;
        }
        #endregion Build

        #endregion Methods
    }
}
