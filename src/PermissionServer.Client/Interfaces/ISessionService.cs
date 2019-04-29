using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Client.Interfaces
{
    public interface ISessionService: IBaseObject
    {
        #region Properties

        string AccessToken { get; }

        #endregion Properties

        #region Methods

        void SetAccessToken(string Token);
        void SetUser(global::PermissionServer.Models.User user);

        #endregion Methods

        #region Events

        #endregion Events
    }
}
