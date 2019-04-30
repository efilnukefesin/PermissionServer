using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Client.Interfaces
{
    public interface IPermissionClientService: IBaseObject
    {
        #region Properties

        #endregion Properties

        #region Methods

        bool FetchPermissions();

        bool CheckPermission(string SubjectId, string Permission);

        #endregion Methods
    }
}
