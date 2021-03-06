﻿using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Extensions;
using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Core.Interfaces;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionServer.Core.Services
{
    public class UserService : BaseObject, IUserService
    {
        #region Properties

        public IEnumerable<User> Users { get; private set; }
        public IEnumerable<Tuple<string, string>> UnknownLogins { get; private set; }
        private IRoleService roleService;
        public bool IsInitialized { get; set; } = false;

        protected IDataService dataService;

        #endregion Properties

        #region Construction

        public UserService(IRoleService RoleService, IDataService dataService)
        {
            this.dataService = dataService;
            this.Users = new List<User>();
            this.UnknownLogins = new List<Tuple<string, string>>();
            this.roleService = RoleService;
        }

        #endregion Construction

        #region Methods

        #region Initialize
        public async Task<bool> Initialize()
        {
            bool result = false;

            ((List<User>)this.Users).Clear();

            await this.roleService.Initialize();  //TODO: integrate nicer, result and stuff

            try
            {
                this.Users = new List<User>(await this.dataService.GetAllAsync<User>("PermissionServer.Core.Services.UserService.Store"));
                result = true;
            }
            catch (Exception ex)
            {
                //TODO: react
            }

            this.IsInitialized = result;
            return result;
        }
        #endregion Initialize

        #region CreateTestData
        public void CreateTestData()
        {
            if (this.roleService.GetRoles().Count().Equals(0))
            {
                this.roleService.CreateTestData();
            }

            User userBob = this.createTestUser("Bob", new List<Login>() { new Login("88421113") });
            User userAlice = this.createTestUser("Alice", new List<Login>() { new Login("818727") });
            User userAdmin = this.createTestUser("Admin", new List<Login>() { new Login("123") });

            userAdmin.AddValue("SomeValue", "Text");
            userAdmin.AddValue("SomeOtherValue", 12345);

            userAdmin.AddOwnedRole(this.roleService.GetRoleByName("TestRole"));
            userAdmin.AddOwnedRole(this.roleService.GetRoleByName("AdminRole"));

            string nameTestRole = "TestRole";
            string nameAdminRole = "AdminRole";

            userBob.AddRole(this.roleService.GetRoleByName(nameTestRole));
            userAlice.AddRole(this.roleService.GetRoleByName(nameTestRole));
            userAdmin.AddRole(this.roleService.GetRoleByName(nameTestRole));
            userAdmin.AddRole(this.roleService.GetRoleByName(nameAdminRole));

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
        public bool CheckPermission(string subjectid, string permissionName)
        {
            bool result = false;

            User user = this.GetUserBySubject(subjectid);
            if (user != null)
            {
                foreach (Role role in user.Roles)
                {
                    foreach (Permission permission in role.Permissions)
                    {
                        if (permission != null && permission.Name.Equals(permissionName))
                        {
                            result = true;
                            break;
                        }
                    }
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

        #region GetUsers: returns all users.
        /// <summary>
        /// returns all users.
        /// </summary>
        /// <returns>a list of all users</returns>
        public IEnumerable<User> GetUsers()
        {
            return this.Users;
        }
        #endregion GetUsers

        #region GetUserByName
        /// <summary>
        /// returns a user by name
        /// </summary>
        /// <param name="Name">the name to be looked for</param>
        /// <returns>a user object or null, if not found</returns>
        public User GetUserByName(string Name)
        {
            return this.Users.Where(x => x.Name.Equals(Name)).FirstOrDefault();
        }
        #endregion GetUserByName

        #region AddUser
        public bool AddUser(User User)
        {
            bool result = false;
            if (!this.Users.Any(x => x.Id.Equals(User.Id)))
            {
                ((List<User>)this.Users).Add(User);
                //store User
                this.dataService.CreateOrUpdateAsync<User>("PermissionServer.Core.Services.UserService.Store", User);

                result = true;
            }
            return result;
        }
        #endregion AddUser

        #region AddUnknownLogin
        public void AddUnknownLogin(string subjectId, string potentialEmail)
        {
            bool doesAlreadyExist = false;
            foreach (Tuple<string, string> tuple in this.UnknownLogins)
            {
                if (tuple.Item1.Equals(subjectId))
                {
                    doesAlreadyExist = true;
                    break;
                }
            }
            if (!doesAlreadyExist)
            {
                this.UnknownLogins = this.UnknownLogins.Add(new Tuple<string, string>(subjectId, potentialEmail));
            }
        }
        #endregion AddUnknownLogin

        #region Clear
        public void Clear()
        {
            ((List<User>)this.Users).Clear();
            this.roleService.Clear();
            ((List<Tuple<string, string>>)this.UnknownLogins).Clear();
        }
        #endregion Clear

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