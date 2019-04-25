using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IConfigurationService
    {
        #region Properties

        Uri IdentityEndpoint { get; }
        string Username { get; }
        string Password { get; }
        string Client { get; }
        string ClientSecret { get; }
        string Scope { get; }

        #endregion Properties

        #region Methods

        #endregion Methods

        #region Events

        #endregion Events
    }
}
