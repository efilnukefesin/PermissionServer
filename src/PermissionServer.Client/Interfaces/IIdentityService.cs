using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IIdentityService: IBaseObject
    {
        #region Properties

        #endregion Properties

        #region Methods

        Task<bool> FetchIdentity(string username, string password);
        Task<bool> FetchIdentity(string username, SecureString securePassword);

        #endregion Methods

        #region Events

        #endregion Events
    }
}
