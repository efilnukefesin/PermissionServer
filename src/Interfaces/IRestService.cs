using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IRestService: IBaseObject
    {
        #region Properties

        #endregion Properties

        #region Methods

        void AddAuthenticationHeader(string value, string type = "Bearer");
        object GetUser(Uri permissionGetEndpoint);

        #endregion Methods

        #region Events

        #endregion Events
    }
}
