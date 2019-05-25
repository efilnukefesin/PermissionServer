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

        #endregion Properties

        #region Construction

        public BaseViewModel(BaseViewModel Parent = null)
        {
            this.Parent = Parent;
        }

        #endregion Construction

        #region Methods

        #region NotifyPropertyChanged
        public void NotifyPropertyChanged()
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(String.Empty));
        }
        #endregion NotifyPropertyChanged

        #endregion Methods

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events
    }
}
