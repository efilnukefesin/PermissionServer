using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IMessageReceiver
    {
        public bool ReceiveMessage(string Text);
    }
}
