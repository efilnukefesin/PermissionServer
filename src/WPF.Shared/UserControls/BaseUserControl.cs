using BootStrapper;
using Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;

namespace WPF.Shared.UserControls
{
    public abstract class BaseUserControl : UserControl, INotifyPropertyChanged, IMessageTransceiver
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties

        private IMessageBroker messageBroker;

        public BaseUserControl()
        {
            this.messageBroker = DiHelper.GetService<IMessageBroker>();
            this.messageBroker.Register(this);
        }

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

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

        #endregion Methods

        #region Events

        #endregion Events
    }
}
