using Interfaces;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WPF.Shared.ViewModels
{
    public abstract class BaseViewModel : BaseObject, INotifyPropertyChanged, IMessageTransceiver
    {
        #region Properties

        protected BaseViewModel Parent { get; set; }
        private IMessageBroker messageBroker;

        #endregion Properties

        #region Construction

        public BaseViewModel(IMessageBroker MessageBroker, BaseViewModel Parent = null)
        {
            this.messageBroker = MessageBroker;
            this.Parent = Parent;

            this.messageBroker.Register(this);
        }

        #endregion Construction

        #region Methods

        #region NotifyPropertyChanged
        public void NotifyPropertyChanged()
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(String.Empty));
        }
        #endregion NotifyPropertyChanged

        #region ReceiveMessage
        public bool ReceiveMessage(string Text, object Data)
        {
            return this.receiveMessage(Text, Data);
        }
        #endregion ReceiveMessage

        protected abstract bool receiveMessage(string Text, object Data);

        #region SendMessage
        public void SendMessage(string Text, object Data = null)
        {
            this.messageBroker.Send(Text, Data);
        }
        #endregion SendMessage

        #region dispose
        protected override void dispose()
        {
            this.Parent = null;
            this.messageBroker = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events
    }
}
