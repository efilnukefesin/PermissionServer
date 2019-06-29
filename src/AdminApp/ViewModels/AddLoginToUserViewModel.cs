﻿using Interfaces;
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
        public ICommand AddOrCreateCommand { get; set; }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public string Text { get; set; }
        public string Hint { get; set; } = "Enter sub ID";
        public ObservableCollection<ValueObject<Tuple<string, string>>> SearchResults { get; set; }
        public string ButtonText { get; set; } = "Add Selected Sub ID";

        private PermissionServer.SDK.Client client;

        private object searchResultLockSync = new object();

        #endregion Properties

        #region Construction

        public AddLoginToUserViewModel(IMessageBroker MessageBroker, PermissionServer.SDK.Client client, BaseViewModel Parent = null) : base(MessageBroker, Parent)
        {
            this.setupCommands();
            this.UnknownLogins = new ObservableCollection<ValueObject<Tuple<string, string>>>();
            this.SearchResults = new ObservableCollection<ValueObject<Tuple<string, string>>>();
            this.client = client;
        }

        #endregion Construction

        #region Methods

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

        private void cancelCommandExecute()
        {
            //TODO: rewind changes
            throw new NotImplementedException();
        }

        private bool okCommandCanExecute()
        {
            throw new NotImplementedException();
        }

        private void okCommandExecute()
        {
            throw new NotImplementedException();
        }

        #region addOrCreateCommandCanExecute
        private bool addOrCreateCommandCanExecute()
        {
            bool result = false;
            if (this.Text != null)
            {
                result = this.Text.Length > 0;
            }
            return result;
        }
        #endregion addOrCreateCommandCanExecute

        private void addOrCreateCommandExecute()
        {
            throw new NotImplementedException();
        }

        #region loadedCommandCanExecute
        private bool loadedCommandCanExecute()
        {
            return true;
        }
        #endregion loadedCommandCanExecute

        #region loadedCommandExecute
        private async void loadedCommandExecute()
        {
            //load stuff
            IEnumerable<ValueObject<Tuple<string, string>>> unkownLogins = await this.client.GetUnkownLoginsAsync();
            this.UnknownLogins = new ObservableCollection<ValueObject<Tuple<string, string>>>(unkownLogins);
            lock (this.searchResultLockSync)
            {
                this.SearchResults = new ObservableCollection<ValueObject<Tuple<string, string>>>(unkownLogins);
            }
            this.SendMessage("DticEnterLoginAction", new Action(this.updateSearchResults));
        }
        #endregion loadedCommandExecute

        #region updateSearchResults
        private void updateSearchResults()
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
                    App.Current.Dispatcher.Invoke((Action)delegate //you may only modify this collection on UI thread
                    {
                        this.SearchResults.Clear();
                        foreach (ValueObject<Tuple<string, string>> item in this.UnknownLogins)
                        {
                            if (item.Value.Item1.Contains(this.Text))
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
                    App.Current.Dispatcher.Invoke((Action)delegate //you may only modify this collection on UI thread
                    {
                        this.SearchResults.Clear();
                        foreach (ValueObject<Tuple<string, string>> item in this.UnknownLogins)
                        {
                            lock (this.searchResultLockSync)
                            {
                                this.SearchResults.Add(item);
                            } 
                        }
                    });
                }
            }
            //if search result == 0 then change button text
            if (this.SearchResults.Count.Equals(0))
            {
                this.ButtonText = "Add new Sub ID";
            }
            else
            {
                this.ButtonText = "Add Selected Sub ID";
            }
        }
        #endregion updateSearchResults

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
