using IdentityModel;
using Interfaces;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class StaticConfigurationService : BaseObject, IConfigurationService
    {
        #region Properties

        public Uri IdentityEndpoint { get; } = new Uri("http://localhost:5000/");
        public string IdentityUsername { get; } = "bob";
        public string IdentityPassword { get; } = "bob";
        public string IdentityClient { get; } = "ro.client";
        //public string ClientSecret { get; } = "511536EF-F270-4058-80CA-1C89C192F69A".ToSha256();
        public string IdentityClientSecret { get; } = "511536EF-F270-4058-80CA-1C89C192F69A";
        public string IdentityScope { get; } = "openid profile api1";
        public Uri PermissionGetEndpoint { get; } = new Uri("http://localhost:6008/api/permissions");

        public Uri SuperHotFeatureEndpoint { get; } = new Uri("http://localhost:6010/api/values");

        public Uri PermissionCheckEndpoint { get; } = new Uri("http://localhost:6008/api/permissions/check");
        public Uri PermissionUnkownLoginsEndpoint { get; } = new Uri("http://localhost:6008/api/permissions/getunknownlogins");

        #endregion Properties

        #region Methods

        #region dispose
        protected override void dispose()
        {
            //TODO: implement
        }
        #endregion dispose

        #endregion Methods
    }
}
