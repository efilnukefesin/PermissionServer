using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDataService
    {
        #region Properties

        #endregion Properties

        #region Methods

        Task<T> GetAsync<T>();

        #endregion Methods

        #region Events

        #endregion Events
    }
}
