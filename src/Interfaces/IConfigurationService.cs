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
        string IdentityUsername { get; }
        string IdentityPassword { get; }
        string IdentityClient { get; }
        string IdentityClientSecret { get; }
        string IdentityScope { get; }
        Uri PermissionServerEndpoint { get; }
        Uri SuperHotFeatureServerEndpoint { get; }
        Uri SuperHotOtherFeatureServerEndpoint { get; }
        TimeSpan PermissionBufferTime { get; }
        TimeSpan UserValueBufferTime { get; }

        #endregion Properties
    }
}
