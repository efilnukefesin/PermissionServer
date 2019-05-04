using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PermissionServer.Client.Interfaces
{
    public interface IPermissionClientService: IBaseObject
    {
        #region Properties

        #endregion Properties

        #region Methods

        bool FetchPermissions(string Token);
        bool CheckPermission(string Token, string SubjectId, string Permission);
        string ExtractToken(Microsoft.Extensions.Primitives.StringValues HttpAuthHeader);
        string ExtractSubjectId(ClaimsPrincipal principal);
        bool CheckPermission(Microsoft.Extensions.Primitives.StringValues HttpAuthHeader, ClaimsPrincipal principal, string Permission);

        #endregion Methods
    }
}
