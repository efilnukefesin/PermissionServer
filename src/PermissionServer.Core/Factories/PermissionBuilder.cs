using PermissionServer.Models;

namespace PermissionServer.Core.Factories
{
	public class PermissionBuilder : IPermissionBuilderChaining
	{
        #region Properties
        private string name;
        #endregion Properties

        #region Construction
        public PermissionBuilder(string Name)
        {
            this.name = Name;
        }
        #endregion Construction

        // Instantiating functions
        #region Methods

        #region CreatePermission
        public static IPermissionBuilderChaining CreatePermission(string Name)
		{
			return new PermissionBuilder(Name);
		}
        #endregion CreatePermission

  //      // Chaining functions
  //      #region AddSomething
  //      public IPermissionBuilderChaining AddSomething(bool IsActive)
		//{
		//	return this;
		//}
  //      #endregion AddSomething

        // Executing functions
        #region Build
        public Permission Build()
		{
            Permission result = new Permission(this.name);
            return result;
		}
        #endregion Build

        #endregion Methods
    }
}
