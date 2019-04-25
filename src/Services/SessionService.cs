using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class SessionService : ISessionService
    {
        #region Properties

        public string AccessToken { get; private set; }

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

        #endregion Methods

        #region Events

        #endregion Events
    }
}
