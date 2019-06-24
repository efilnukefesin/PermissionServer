using Interfaces;
using NET.efilnukefesin.Extensions.Wpf.Commands;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using WPF.Shared.ViewModels;

namespace AdminApp.ViewModels
{
    [Locator("AddLoginToUserViewModel")]
    internal class AddLoginToUserViewModel : BaseViewModel
    {
        #region Properties

        public User SelectedUser { get; set; }
        public ObservableCollection<ValueObject<Tuple<string, string>>> UnknownLogins { get; set; }
        public ICommand LoadedCommand { get; set; }

        private PermissionServer.SDK.Client client;

        #endregion Properties

        #region Construction

        public AddLoginToUserViewModel(IMessageBroker MessageBroker, PermissionServer.SDK.Client client, BaseViewModel Parent = null) : base(MessageBroker, Parent)
        {
            this.setupCommands();
            this.UnknownLogins = new ObservableCollection<ValueObject<Tuple<string, string>>>();
            this.client = client;
        }

        #endregion Construction

        #region Methods

        #region setupCommands
        protected void setupCommands()
        {
            this.LoadedCommand = new RelayCommand(this.loadedCommandExecute, this.loadedCommandCanExecute);
        }
        #endregion setupCommands

        #region loadedCommandCanExecute
        private bool loadedCommandCanExecute()
        {
            return true;
        }
        #endregion loadedCommandCanExecute

        #region loadedCommandExecute
        private async void loadedCommandExecute()
        {
            //TODO: load stuff
            IEnumerable<ValueObject<Tuple<string, string>>> unkownLogins = await this.client.GetUnkownLoginsAsync();
            this.UnknownLogins = new ObservableCollection<ValueObject<Tuple<string, string>>>(unkownLogins);
        }
        #endregion loadedCommandExecute

        #region receiveMessage
        protected override bool receiveMessage(string Text, object Data)
        {
            bool result = false;
            if (Text.Equals("SelectedUser"))
            {
                this.SelectedUser = (User)Data;
                result = true;
            }
            return result;
        }
        #endregion receiveMessage

        #endregion Methods
    }
}
