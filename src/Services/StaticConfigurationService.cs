using IdentityModel;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class StaticConfigurationService : IConfigurationService
    {
        #region Properties

        public Uri IdentityEndpoint { get; } = new Uri("http://localhost:5000/");
        public string Username { get; } = "bob";
        public string Password { get; } = "bob";
        public string Client { get; } = "ro.client";
        //public string ClientSecret { get; } = "511536EF-F270-4058-80CA-1C89C192F69A".ToSha256();
        public string ClientSecret { get; } = "511536EF-F270-4058-80CA-1C89C192F69A";
        public string Scope { get; } = "openid profile api1";
        public Uri PermissionGetEndpoint { get; } = new Uri("https://localhost:44318/api/permissions");

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #endregion Methods

        #region Events

        #endregion Events
    }
}
