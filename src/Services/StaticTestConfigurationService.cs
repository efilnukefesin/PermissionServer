using Interfaces;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class StaticTestConfigurationService : BaseObject, IConfigurationService
    {
        #region Properties

        public Uri IdentityEndpoint { get; } = new Uri("http://localhost:5000/");
        public string IdentityUsername { get; } = "bob";
        public string IdentityPassword { get; } = "bob";
        //public string IdentityUsername { get; } = "admin";
        //public string IdentityPassword { get; } = "admin";
        public string IdentityClient { get; } = "ro.client";
        public string IdentityClientSecret { get; } = "511536EF-F270-4058-80CA-1C89C192F69A";
        public string IdentityScope { get; } = "openid profile api1";
        public Uri PermissionServerEndpoint { get; } = new Uri("http://localhost/");
        public Uri SuperHotFeatureServerEndpoint { get; } = new Uri("http://localhost:6010/");
        public Uri SuperHotOtherFeatureServerEndpoint { get; } = new Uri("http://localhost:6012/");

        public TimeSpan PermissionBufferTime { get; } = TimeSpan.FromSeconds(30);
        public TimeSpan UserValueBufferTime { get; } = TimeSpan.FromSeconds(30);

        #endregion Properties

        #region Methods

        #region dispose
        protected override void dispose()
        {

        }
        #endregion dispose

        #endregion Methods
    }
}
