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
        Uri PermissionGetEndpoint { get; }
        Uri PermissionCheckEndpoint { get; }
        Uri SuperHotFeatureEndpoint { get; }

        Uri PermissionServerEndpoint { get; }
        Uri SuperHotFeatureServerEndpoint { get; }
        Uri SuperHotOtherFeatureServerEndpoint { get; }

        string PermissionGivenPermissionEndpointUffix { get; }
        string PermissionGetUsersEndpointUffix { get; }
        string PermissionGetRolesEndpointUffix { get; }
        string PermissionGetPermissionsEndpointUffix { get; }

        TimeSpan PermissionBufferTime { get; }
        TimeSpan UserValueBufferTime { get; }

        #endregion Properties
    }
}
