using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AdminApp.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        #region Properties

        public string Hint { get; set; }

        #endregion Properties

        #region Construction

        public LoginViewModel()
        {
            this.Hint = "Test123";
        }

        #endregion Construction

        #region Methods

        #endregion Methods

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events
    }
}
