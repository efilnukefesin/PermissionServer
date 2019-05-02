using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Core.Interfaces;
using PermissionServer.Core.Options;
using PermissionServer.Core.Strategies;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Core.Services
{
    public class PermissionService : BaseObject
    {
        #region Properties

        private IPermissionFlowStrategy permissionFlowStrategy;
        private PermissionServiceOptions config;
        private IUserService userService;

        #endregion Properties

        #region Construction

        public PermissionService(Action<PermissionServiceOptions> options, IUserService UserService)
        {
            this.userService = UserService;
            this.userService.CreateTestUsers();  //TODO: delete

            this.config = new PermissionServiceOptions();
            if (options != null)
            {
                options(this.config);
            }

            switch (this.config.FlowType)
            {
                case Enums.PermissionFlowType.ClientSide:
                    this.permissionFlowStrategy = new ClientSidePermissionFlowStrategy();
                    break;
                case Enums.PermissionFlowType.ServerSide:
                    this.permissionFlowStrategy = new ServerSidePermissionFlowStrategy();
                    break;
                default:
                    break;
            }
        }

        #endregion Construction

        #region Methods

        #region GetUser: Gets a User by subject id - if not found, adds it to the new logins
        /// <summary>
        /// Gets a User by subject id - if not found, adds it to the new logins
        /// </summary>
        /// <param name="subjectId">the subject id of the user login</param>
        /// <returns>the user, or, if not found, null</returns>
        public User GetUser(string subjectId)
        {
            User result = this.userService.GetUserBySubject(subjectId);
            if (result == null)
            {
                this.RegisterNewLogin(subjectId);
            }
            return result;
        }
        #endregion GetUser

        #region RegisterNewLogin
        public void RegisterNewLogin(string subjectId)
        {
            this.userService.RegisterNewLogin(subjectId);
        }
        #endregion RegisterNewLogin

        #region CheckPermission
        public bool CheckPermission(string subjectid, string permission)
        {
            bool result = this.userService.CheckPermission(subjectid, permission);
            return result;
        }
        #endregion CheckPermission

        #region dispose
        protected override void dispose()
        {
            this.unknownLogins.Clear();
            this.unknownLogins = null;
            this.userService = null;
            this.config = null;
            this.permissionFlowStrategy = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
