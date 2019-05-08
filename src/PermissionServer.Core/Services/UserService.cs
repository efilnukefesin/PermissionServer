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

        public IEnumerable<User> Users { get; private set; }
        public IEnumerable<Tuple<string, string>> UnknownLogins { get; private set; }

        #endregion Properties

        #region Construction

        public UserService()
        {
            this.Users = new List<User>();
            this.UnknownLogins = new List<Tuple<string, string>>();
        }

        #endregion Construction

        #region Methods

        #region CreateTestData
        public void CreateTestData()
        {
            User userBob = this.createTestUser("Bob", new List<Login>() { new Login("88421113") });
            User userAlice = this.createTestUser("Alice", new List<Login>() { new Login("818727") });
            User userAdmin = this.createTestUser("Admin", new List<Login>() { new Login("123") });

            Permission permissionUser = new Permission() { Name = "User" };
            Permission permissionSuperHotFeature1 = new Permission() { Name = "SuperHotFeature1" };
            Permission permissionSuperHotFeature2 = new Permission() { Name = "SuperHotFeature2" };
            Permission permissionGetUnknownLogins = new Permission() { Name = "GetUnknownLogins" };
            Permission permissionLinkLoginToUser = new Permission() { Name = "LinkLoginToUser" };
            Permission permissionLinkRoleToUser = new Permission() { Name = "LinkRoleToUser" };
            Permission permissionLinkPermissionToRole = new Permission() { Name = "LinkPermissionToRole" };
            Permission permissionCreateUser = new Permission() { Name = "CreateUser" };
            Permission permissionCreateRole = new Permission() { Name = "CreateRole" };
            Permission permissionCreatePermission = new Permission() { Name = "CreatePermission" };
            Permission permissionGetUsers = new Permission() { Name = "GetUsers" };
            Permission permissionGetRoles = new Permission() { Name = "GetRoles" };
            Permission permissionGetPermissions = new Permission() { Name = "GetPermissions" };

            Role roleTest = new Role("TestRole", new List<User>() { userAdmin }, new List<Permission>() { permissionSuperHotFeature1, permissionSuperHotFeature2, permissionUser });
            Role roleAdmin = new Role("AdminRole", new List<User>() { userAdmin }, new List<Permission>() { permissionUser, permissionGetUnknownLogins, permissionLinkLoginToUser, permissionLinkRoleToUser, permissionLinkPermissionToRole, permissionCreateUser, permissionCreateRole, permissionCreatePermission, permissionGetUsers, permissionGetRoles, permissionGetPermissions });

            userBob.AddRole(roleTest);
            userAlice.AddRole(roleTest);
            userAdmin.AddRole(roleTest);
            userAdmin.AddRole(roleAdmin);

            ((List<User>)this.Users).Add(userBob);
            ((List<User>)this.Users).Add(userAlice);
            ((List<User>)this.Users).Add(userAdmin);
        }
        #endregion CreateTestData

        #region createTestUser
        private User createTestUser(string Name, List<Login> Logins)
        {
            User result = new User(Name);

            foreach (Login login in Logins)
            {
                result.AddLogin(login);
            }

            return result;
        }
        #endregion createTestUser

        #region GetUserBySubject: returns a user by subject id of a login
        /// <summary>
        /// returns a user by subject id of a login
        /// </summary>
        /// <param name="SubjectId">the sub id</param>
        /// <returns>a user object belonging to the sub id or null</returns>
        public User GetUserBySubject(string SubjectId)
        {
            return this.Users.Where(x => x.Logins.Any(y => y.SubjectId.Equals(SubjectId))).FirstOrDefault();
        }
        #endregion GetUserBySubject

        #region CheckPermission
        public bool CheckPermission(string subjectid, string permission)
        {
            bool result = false;

            User user = this.GetUserBySubject(subjectid);
            if (user != null)
            {
                if (user.Roles.Any(x => x.Permissions.Any(y => y.Name.Equals(permission))))
                {
                    result = true;
                }
            }

            return result;
        }
        #endregion CheckPermission

        #region RegisterNewLogin: adds a new subject to the list of unkown logins
        /// <summary>
        /// adds a new subject to the list of unkown logins
        /// </summary>
        /// <param name="subjectId">the subject id</param>
        public void RegisterNewLogin(string subjectId, string Email)
        {
            ((List<Tuple<string, string>>)this.UnknownLogins).Add(Tuple.Create<string, string>(subjectId, Email));
        }
        #endregion RegisterNewLogin

        #region GetUsers
        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }
        #endregion GetUsers

        #region dispose
        protected override void dispose()
        {
            ((List<string>)this.Users).Clear();
            this.Users = null;
            ((List<string>)this.UnknownLogins).Clear();
            this.UnknownLogins = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
