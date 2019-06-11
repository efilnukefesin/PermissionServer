using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IMessageSender
    {
        public void SendMessage(string Text, object Data);
    }
}
