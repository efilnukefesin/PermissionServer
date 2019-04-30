using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IRestService: IBaseObject
    {
        #region Methods

        void AddAuthenticationHeader(string value, string type = "Bearer");
        object GetUser(Uri permissionGetEndpoint);
        object GetPermission(Uri permissionCheckEndpoint, string subjectId, string permission);

        #endregion Methods
    }
}
