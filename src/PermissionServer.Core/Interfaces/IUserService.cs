using NET.efilnukefesin.Contracts.Base;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Core.Interfaces
{
    public interface IUserService : IBaseObject, ICreateTestData
    {
        #region Properties

        IEnumerable<User> Users { get; }
        IEnumerable<Tuple<string, string>> UnknownLogins { get; }

        #endregion Properties

        #region Methods

        User GetUserBySubject(string SubjectId);
        bool CheckPermission(string SubjectId, string Permission);
        void RegisterNewLogin(string SubjectId, string Email);
        IEnumerable<User> GetUsers();
        User GetUserByName(string Name);

        #endregion Methods
    }
}
