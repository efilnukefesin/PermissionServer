using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDataService
    {
        #region Properties

        IEndpointRegister EndpointRegister { get; }

        #endregion Properties

        #region Methods

        void AddOrReplaceAuthentication(string BearerToken);

        Task<T> GetAsync<T>();

        //TODO: implement CRUD methods

        #endregion Methods

        #region Events

        #endregion Events
    }
}
