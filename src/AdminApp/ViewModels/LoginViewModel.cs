using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Text;

namespace AdminApp.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        #region Properties

        public string Hint { get; set; }
        public SecureString SecurePassword { private get; set; }
        public string Password { get; set; }  //just for lookup reasons, delete in a productive app
        public string Username { get; set; }

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
