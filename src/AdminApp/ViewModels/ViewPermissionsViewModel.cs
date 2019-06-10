using Interfaces;
using NET.efilnukefesin.Extensions.Wpf.Commands;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace AdminApp.ViewModels
{
    [Locator("ViewPermissionsViewModel")]
    internal class ViewPermissionsViewModel : BaseViewModel
    {
        #region Properties

        public bool IsIdle { get; set; } = true;
        public bool MayEdit { get; set; } = false;
        public ObservableCollection<Permission> Permissions { get; set; }

        public ICommand LoadedCommand { get; set; }
        public ICommand AddCommand { get; set; }

        private PermissionServer.SDK.Client client;

        #endregion Properties

        #region Construction

        public ViewPermissionsViewModel(IMessageBroker MessageBroker, PermissionServer.SDK.Client client, BaseViewModel Parent = null)
            : base(MessageBroker, Parent)
        {
            this.client = client;
            this.Permissions = new ObservableCollection<Permission>();
            this.setupCommands();
        }

        #endregion Construction

        #region Methods

        #region setupCommands
        //TODO: move to abstract base class?
        protected void setupCommands()
        {
            this.LoadedCommand = new RelayCommand(this.loadedCommandExecute, this.loadedCommandCanExecute);
            this.AddCommand = new RelayCommand(this.addCommandExecute, this.addCommandCanExecute);
        }
        #endregion setupCommands

        #region addCommandCanExecute
        private bool addCommandCanExecute()
        {
            //TODO: implement
            return false;
        }
        #endregion addCommandCanExecute

        #region addCommandExecute
        private void addCommandExecute()
        {
            throw new NotImplementedException();
        }
        #endregion addCommandExecute

        #region loadedCommandCanExecute
        private bool loadedCommandCanExecute()
        {
            return true;
        }
        #endregion loadedCommandCanExecute

        #region loadedCommandExecute
        private async void loadedCommandExecute()
        {
            IEnumerable<Permission> permissions = await this.client.GetAllPermissionsAsync();
            if (permissions != null)
            {
                this.Permissions = new ObservableCollection<Permission>(permissions);
            }
        }
        #endregion loadedCommandExecute

        #region dispose
        protected override void dispose()
        {

        }
        #endregion dispose

        #region receiveMessage
        protected override bool receiveMessage(string Text)
        {
            return false;
        }
        #endregion receiveMessage

        #endregion Methods
    }
}
