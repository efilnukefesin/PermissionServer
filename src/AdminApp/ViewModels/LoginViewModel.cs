using NET.efilnukefesin.Extensions.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Text;
using System.Windows.Input;

namespace AdminApp.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        #region Properties

        public string Hint { get; set; }
        public SecureString SecurePassword { private get; set; }
        public string Password { get; set; }  //just for lookup reasons, delete in a productive app
        public string Username { get; set; }

        public ICommand OkCommand { get; set; }

        #endregion Properties

        #region Construction

        public LoginViewModel()
        {
            this.Hint = "Test123";
            this.setupCommands();
        }

        #endregion Construction

        #region Methods

        #region setupCommands
        //TODO: move to abstract base class?
        protected void setupCommands()
        {
            this.OkCommand = new RelayCommand(this.okCommandExecute, this.okCommandCanExecute);
        }
        #endregion setupCommands

        #region okCommandCanExecute
        private bool okCommandCanExecute()
        {
            bool result = false;
            result = !string.IsNullOrWhiteSpace(this.Username) && this.SecurePassword?.Length > 0;
            return result;
        }
        #endregion okCommandCanExecute

        #region okCommandExecute
        private void okCommandExecute()
        {
            throw new NotImplementedException();
        }
        #endregion okCommandExecute

        #endregion Methods

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events
    }
}
