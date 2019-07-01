using NET.efilnukefesin.Contracts.Base;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.Core.Interfaces
{
    public interface IUserService : IBaseObject, ICreateTestData, IInitalize, IClear
    {
        #region Properties

        IEnumerable<User> Users { get; }
        IEnumerable<UnknownLogin> UnknownLogins { get; }

        #endregion Properties

        #region Methods

        User GetUserBySubject(string SubjectId);
        bool CheckPermission(string SubjectId, string Permission);
        void RegisterNewLogin(string SubjectId);
        IEnumerable<User> GetUsers();
        User GetUserByName(string Name);
        bool AddOrUpdateUser(User User);
        void AddUnknownLogin(string subjectId);

        #endregion Methods
    }
}
