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

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #endregion Methods

        #region Events

        #endregion Events
    }
}
