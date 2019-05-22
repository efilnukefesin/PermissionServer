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
            string result = string.Empty;
            if (principal.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                result = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            return result;
        }
        #endregion ExtractSubjectId
    }
}
