using NET.efilnukefesin.Contracts.Base;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Core.Interfaces
{
    public interface IUserService : IBaseObject
    {
        #region Properties

        #endregion Properties

        #region Methods

        void CreateTestUsers();
        User GetUserBySubject(string SubjectId);
        bool CheckPermission(string subjectid, string permission);

        #endregion Methods
    }
}
