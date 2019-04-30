using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IClientRestService: IBaseObject
    {
        #region Methods

        void AddAuthenticationHeader(string value, string type = "Bearer");
        T Get<T>(Uri Endpoint);

        #endregion Methods
    }
}
