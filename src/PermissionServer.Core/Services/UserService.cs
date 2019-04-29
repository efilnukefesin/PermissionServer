using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Core.Interfaces;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PermissionServer.Core.Services
{
    public class UserService : BaseObject, IUserService
    {
        #region Properties

        public IEnumerable<User> Users { get; set; }

        #endregion Properties

        #region Construction

        public UserService()
        {
            this.Users = new List<User>();
        }

        #endregion Construction

        #region Methods

        #region CreateTestUsers
        public void CreateTestUsers()
        {
            //TODO: build logical order + clean up
            User user = new User("Bob");

            Permission permission = new Permission();
            permission.Name = "TestPermission";

            Role role = new Role("TestRole", new List<User>() { user }, new List<Permission>() { permission });
            role.Name = "TestRole";

            user.AddRole(role);
            
            Login login = new Login("88421113"/*, user*/);
            user.AddLogin(login);

            ((List<User>)this.Users).Add(user);
        }
        #endregion CreateTestUsers

        #region GetUserBySubject
        public User GetUserBySubject(string SubjectId)
        {
            return this.Users.Where(x => x.Logins.Any(y => y.SubjectId.Equals(SubjectId))).FirstOrDefault();
        }
        #endregion GetUserBySubject

        #region dispose
        protected override void dispose()
        {
            //TODO: implement
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events

    }
}
