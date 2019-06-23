using Interfaces;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using WPF.Shared.ViewModels;

namespace AdminApp.ViewModels
{
    [Locator("AddValueToUserViewModel")]
    internal class AddValueToUserViewModel : BaseViewModel
    {
        public AddValueToUserViewModel(IMessageBroker MessageBroker, BaseViewModel Parent = null) : base(MessageBroker, Parent)
        {
        }

        protected override bool receiveMessage(string Text, object Data)
        {
            //throw new NotImplementedException();
            return false;
        }
    }
}
