using Interfaces;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Extensions.Wpf.Commands;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Mvvm.Attributes;
using Newtonsoft.Json;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using WPF.Shared.ViewModels;
using NET.efilnukefesin.Extensions;

namespace AdminApp.ViewModels
{
    [Locator("AddLoginToUserViewModel")]
    internal class AddLoginToUserViewModel : BaseWindowViewModel
    {
        #region Properties

        public User SelectedUser { get; set; }
        public bool HasChanged { get; set; } = false;
        public ObservableCollection<UnknownLogin> UnknownLogins { get; set; }
        public ICommand LoadedCommand { get; set; }
        public ICommand AddOrCreateCommand { get; set; }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public string Text { get; set; }
        public string Hint { get; set; } = "Enter sub ID";
        public UnknownLogin SelectedUnknownLogin { get; set; }
        public ObservableCollection<UnknownLogin> SearchResults { get; set; }
        private List<UnknownLogin> addedUnknownLogins;
        public string ButtonText { get; set; } = "Add Selected Sub ID";

        private PermissionServer.SDK.Client client;
        private INavigationService navigationService;

        private object searchResultLockSync = new object();

        #endregion Properties

        #region Construction

        public AddLoginToUserViewModel(IMessageBroker MessageBroker, INavigationService NavigationService, PermissionServer.SDK.Client client, BaseViewModel Parent = null) : base(MessageBroker, Parent)
        {
            this.WindowTitle = "Add Login to User";
            this.setupCommands();
            this.UnknownLogins = new ObservableCollection<UnknownLogin>();
            this.SearchResults = new ObservableCollection<UnknownLogin>();
            this.addedUnknownLogins = new List<UnknownLogin>();
            this.client = client;
            this.navigationService = NavigationService;
        }

        #endregion Construction

        #region Methods

        #region OnSelectedUnknownLoginChanged
        protected void OnSelectedUnknownLoginChanged()
        {
            if (this.SelectedUnknownLogin != null)
            {
                this.Text = this.SelectedUnknownLogin.SubjectId;
                this.UpdateSearchResults();
            }
        }
        #endregion OnSelectedUnknownLoginChanged

        #region setupCommands
        protected void setupCommands()
        {
            this.LoadedCommand = new RelayCommand(this.loadedCommandExecute, this.loadedCommandCanExecute);
            this.AddOrCreateCommand = new RelayCommand(this.addOrCreateCommandExecute, this.addOrCreateCommandCanExecute);
            this.OkCommand = new RelayCommand(this.okCommandExecute, this.okCommandCanExecute);
            this.CancelCommand = new RelayCommand(this.cancelCommandExecute, this.cancelCommandCanExecute);
        }
        #endregion setupCommands

        #region cancelCommandCanExecute
        private bool cancelCommandCanExecute()
        {
            return true;
        }
        #endregion cancelCommandCanExecute

        #region cancelCommandExecute
        private void cancelCommandExecute()
        {
            //rewind changes
            this.SelectedUser.Restore();
            //close window
            this.navigationService.Back();
        }
        #endregion cancelCommandExecute

        #region okCommandCanExecute
        private bool okCommandCanExecute()
        {
            this.checkIfChanged();
            return this.HasChanged;
        }
        #endregion okCommandCanExecute

        #region okCommandExecute
        private async void okCommandExecute()
        {
            await this.client.AddOrUpdateUserAsync(this.SelectedUser);
            //close window
            this.navigationService.Back();
        }
        #endregion okCommandExecute

        #region addOrCreateCommandCanExecute
        private bool addOrCreateCommandCanExecute()
        {
            bool result = false;
            if (this.Text != null)
            {
                result = this.Text.Length > 0;
            }
            if (!result && this.SelectedUnknownLogin != null)
            {
                result = true;
            }
            return result;
        }
        #endregion addOrCreateCommandCanExecute

        #region addOrCreateCommandExecute
        private async void addOrCreateCommandExecute()
        {
            Login loginToAdd = null;
            if (this.SelectedUnknownLogin != null)
            {
                loginToAdd = new Login(this.SelectedUnknownLogin.SubjectId);
                //delete unknown login
                //TODO: mark unkown login for deletion
                //TODO: restore when cancelled
                this.addedUnknownLogins.Add(this.SelectedUnknownLogin.Clone());
                this.client.DeleteUnknownLoginAsync(this.SelectedUnknownLogin);
                this.SelectedUnknownLogin = null;
            }
            else
            {
                loginToAdd = new Login(this.Text);
                this.Text = string.Empty;
            }
            this.SelectedUser.AddLogin(loginToAdd);
            this.NotifyPropertyChanged();
        }
        #endregion addOrCreateCommandExecute

        #region loadedCommandCanExecute
        private bool loadedCommandCanExecute()
        {
            return true;
        }
        #endregion loadedCommandCanExecute

        #region loadedCommandExecute
        private async void loadedCommandExecute()
        {
            //make backup of selected user
            this.SelectedUser.Save();
            //load stuff
            IEnumerable<UnknownLogin> unknownLogins = await this.client.GetUnknownLoginsAsync();
            this.UnknownLogins = new ObservableCollection<UnknownLogin>(unknownLogins);
            this.UnknownLogins.Save(nameof(this.UnknownLogins));
            lock (this.searchResultLockSync)
            {
                this.SearchResults = new ObservableCollection<UnknownLogin>(unknownLogins);
            }
            this.SendMessage("DticEnterLoginAction", new Action(this.UpdateSearchResults));
        }
        #endregion loadedCommandExecute

        #region UpdateSearchResults
        public void UpdateSearchResults()
        {
            if (this.Text == null)
            {
                this.Text = "";
            }

            //this method should be called as action in the user control.
            if (this.Text != null)
            {
                //reduce search results
                if (this.Text.Length > 0)
                {
                    this.ExecuteOnUIThread((Action)delegate //you may only modify this collection on UI thread
                    {
                        this.SearchResults.Clear();
                        foreach (UnknownLogin item in this.UnknownLogins)
                        {
                            if (item.SubjectId.Contains(this.Text))
                            {
                                lock (this.searchResultLockSync)
                                {
                                    this.SearchResults.Add(item);
                                }
                            }
                        }
                    });
                }
                else
                {
                    //show all results
                    this.ExecuteOnUIThread((Action)delegate //you may only modify this collection on UI thread
                    {
                        this.SearchResults.Clear();
                        foreach (UnknownLogin item in this.UnknownLogins)
                        {
                            lock (this.searchResultLockSync)
                            {
                                this.SearchResults.Add(item);
                            } 
                        }
                    });
                }
            }
            //if search result == 0 then change button text - AHA change
            // Button text is depending on whether the user lastly changed the text or clicked something (?)
            if (this.SearchResults.Count.Equals(0))
            {
                this.ButtonText = "Add new Sub ID";
            }
            else
            {
                this.ButtonText = "Add Selected Sub ID";
            }
        }
        #endregion UpdateSearchResults

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

        #region checkIfChanged: checks, if the user has been changed and sets HasChanged accordingly.
        /// <summary>
        /// checks, if the user has been changed and sets HasChanged accordingly.
        /// </summary>
        private void checkIfChanged()
        {
            this.HasChanged = this.SelectedUser.DiffersFromMemory();
        }
        #endregion checkIfChanged

        #endregion Methods
    }
}
