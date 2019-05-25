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
        private List<IMessageReceiver> receivers;

        #endregion Properties

        #region Construction

        public SimpleMessageBroker(ILogger logger)
        {
            this.logger = logger;
            this.receivers = new List<IMessageReceiver>();
        }

        #endregion Construction

        #region Methods

        #region Register
        public void Register(IMessageReceiver Receiver)
        {
            this.receivers.Add(Receiver);
        }
        #endregion Register

        #region Send
        public void Send(string text)
        {
            foreach (IMessageReceiver receiver in this.receivers)
            {
                receiver.ReceiveMessage(text);
            }
        }
        #endregion Send

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
