using NET.efilnukefesin.Implementations.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BootStrapper
{
    public static class DiHelper
    {
        #region Methods

        #region GetService
        public static I GetService<I>()
        {
            return DiManager.GetInstance().Resolve<I>();
        }
        #endregion GetService

        #endregion Methods
    }
}
