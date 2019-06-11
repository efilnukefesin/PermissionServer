using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IMessageBroker : IBaseObject
    {
        void Register(IMessageReceiver Receiver);
        void Send(string text, object Data);
    }
}
