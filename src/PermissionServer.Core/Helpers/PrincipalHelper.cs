using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PermissionServer.Core.Helpers
{
    public static class PrincipalHelper
    {
        #region ExtractSubjectId
        public static string ExtractSubjectId(ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        #endregion ExtractSubjectId
    }
}
