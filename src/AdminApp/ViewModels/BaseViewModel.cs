using Interfaces;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AdminApp.ViewModels
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
        public bool ReceiveMessage(string Text)
        {
            return this.receiveMessage(Text);
        }
        #endregion ReceiveMessage

        protected abstract bool receiveMessage(string Text);

        #region SendMessage
        public void SendMessage(string Text)
        {
            this.messageBroker.Send(Text);
        }
        #endregion SendMessage

        #endregion Methods

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events
    }
}
