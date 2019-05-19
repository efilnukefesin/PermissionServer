using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IEndpointRegister
    {
        #region Properties

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        bool AddEndpoint(string Action, string Endpoint);
        string GetEndpoint(string Action);

        #endregion Methods

        #region Events

        #endregion Events
    }
}
