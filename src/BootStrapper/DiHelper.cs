using NET.efilnukefesin.Implementations.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BootStrapper
{
    /// <summary>
    /// helper class to avoid direct calls to DiManager class
    /// </summary>
    public static class DiHelper
    {
        #region Methods

        #region GetService: gets a service implementation from the DI Container
        /// <summary>
        /// gets a service implementation from the DI Container
        /// </summary>
        /// <typeparam name="I">the interface you need the implementation for</typeparam>
        /// <returns>the service implementation</returns>
        public static I GetService<I>()
        {
            return DiManager.GetInstance().Resolve<I>();
        }
        #endregion GetService

        #endregion Methods
    }
}
