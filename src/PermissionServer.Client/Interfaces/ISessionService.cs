using NET.efilnukefesin.Contracts.Base;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Client.Interfaces
{
    public interface ISessionService: IBaseObject
    {
        #region Properties

        string AccessToken { get; }
        User User { get; }

        #endregion Properties

        #region Methods

        void SetAccessToken(string Token);
        void SetUser(User user);

        #endregion Methods
    }
}
