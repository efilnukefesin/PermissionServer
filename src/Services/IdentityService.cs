﻿using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class IdentityService : IIdentityService
    {
        #region Properties

        private IConfigurationService configurationService;

        #endregion Properties

        #region Construction

        public IdentityService(IConfigurationService ConfigurationService)
        {
            this.configurationService = ConfigurationService;
        }

        #endregion Construction

        #region Methods

        #endregion Methods

        #region Events

        #endregion Events
    }
}
