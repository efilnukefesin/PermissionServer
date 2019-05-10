using PermissionServer.Models;
using System.Collections.Generic;

namespace PermissionServer.Core.Factories
{
	public class UserBuilder : IUserBuilderChaining
	{
        #region Properties

        private User result;

        #endregion Properties

        #region Construction

        public UserBuilder(string Name)
        {
            this.result = new User(Name);
        }

        #endregion Construction

        #region Methods
        // Instantiating functions

        #region CreateUser
        public static IUserBuilderChaining CreateUser(string Name)
		{
			return new UserBuilder(Name);
		}
        #endregion CreateUser

        // Chaining functions

        #region AddSubstitution
        public IUserBuilderChaining AddSubstitution(Substitution Substitution)
		{
            this.result.AddSubstitution(Substitution);
			return this;
		}
        #endregion AddSubstitution

        #region AddOwnedRole
        public IUserBuilderChaining AddOwnedRole(Role Role)
		{
            this.result.AddOwnedRole(Role);
			return this;
		}
        #endregion AddOwnedRole

        #region AddRole
        public IUserBuilderChaining AddRole(Role Role)
		{
            this.result.AddRole(Role);
			return this;
		}
        #endregion AddRole

        #region AddLogin
        public IUserBuilderChaining AddLogin(Login Login)
		{
            this.result.AddLogin(Login);
			return this;
		}
        #endregion AddLogin

        // Executing functions
        #region Build
        public User Build()
		{
            return this.result;
        }
        #endregion Build

        #endregion Methods
    }
}
