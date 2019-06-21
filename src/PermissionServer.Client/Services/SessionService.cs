using Interfaces;
using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Client.Interfaces;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Client.Services
{
    public class SessionService : BaseObject, ISessionService
    {
        #region Properties

        public string AccessToken { get; private set; }
        public User User { get; private set; }

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region SetAccessToken
        public void SetAccessToken(string Token)
        {
            this.AccessToken = Token;
        }
        #endregion SetAccessToken

        #region SetUser
        public void SetUser(User User)
        {
            this.User = User;
        }
        #endregion SetUser

        //TODO: do periodic refresh of the tokens

        #region dispose
        protected override void dispose()
        {
            this.User = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
