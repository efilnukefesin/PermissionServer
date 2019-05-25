using Interfaces;
using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class SimpleMessageBroker : BaseObject, IMessageBroker
    {
        #region Properties

        private ILogger logger;

        #endregion Properties

        #region Construction

        public SimpleMessageBroker(ILogger logger)
        {
            this.logger = logger;
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            throw new NotImplementedException();
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
