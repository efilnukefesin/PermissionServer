using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IConfigurationService: IBaseObject
    {
        #region Properties

        Uri IdentityEndpoint { get; }
        string Username { get; }
        string Password { get; }
        string Client { get; }
        string ClientSecret { get; }
        string Scope { get; }
        Uri PermissionGetEndpoint { get; }
        Uri SuperHotFeatureEndpoint { get; }

        #endregion Properties
    }
}
