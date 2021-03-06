﻿using BootStrapper;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Extensions.Wpf.Commands;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.Shared.UserControls;

namespace AdminApp.UserControls
{
    /// <summary>
    /// Interaktionslogik für UserDetailsControl.xaml
    /// </summary>
    public partial class UserDetailsControl : BaseUserControl
    {
        #region Properties

        #region Item Property
        public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(User), typeof(UserDetailsControl), new PropertyMetadata(default, Item_ValueChanged));

        static void Item_ValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            UserDetailsControl self = obj as UserDetailsControl;
            if (self.ItemChanged != null) self.ItemChanged(self, new EventArgs());

            self.UpdateUI();
        }

        [Description("The User to show the details of"), Category("Own Properties"), DisplayName("Item")]
        public User Item
        {
            get { return (User)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        public event EventHandler ItemChanged;
        #endregion Item Property

        #region MayEdit Property
        public static readonly DependencyProperty MayEditProperty = DependencyProperty.Register("MayEdit", typeof(bool), typeof(UserDetailsControl), new PropertyMetadata(false, MayEdit_ValueChanged));

        static void MayEdit_ValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            UserDetailsControl self = obj as UserDetailsControl;
            if (self.MayEditChanged != null) self.MayEditChanged(self, new EventArgs());

            self.UpdateUI();
        }

        [Description("May the Item be edited as well?"), Category("Own Properties"), DisplayName("MayEdit")]
        public bool MayEdit
        {
            get { return (bool)GetValue(MayEditProperty); }
            set { SetValue(MayEditProperty, value); }
        }

        public event EventHandler MayEditChanged;
        #endregion MayEdit Property

        public ICommand AddLoginCommand { get; set; }
        public ICommand AddValueCommand { get; set; }
        public ICommand AddRoleCommand { get; set; }
        public ICommand AddOwnedRoleCommand { get; set; }

        private INavigationService navigationService;

        #endregion Properties

        #region Construction
        public UserDetailsControl()
        {
            InitializeComponent();
            this.navigationService = DiHelper.GetService<INavigationService>();
            this.setupCommands();
        }
        #endregion Construction

        #region Methods

        #region setupCommands
        private void setupCommands()
        {
            this.AddLoginCommand = new RelayCommand(this.addLoginCommandExecute, this.addLoginCommandCanExecute);
            this.AddValueCommand = new RelayCommand(this.addValueCommandExecute, this.addValueCommandCanExecute);
            this.AddRoleCommand = new RelayCommand(this.addRoleCommandExecute, this.addRoleCommandCanExecute);
            this.AddOwnedRoleCommand = new RelayCommand(this.addOwnedRoleCommandExecute, this.addOwnedRoleCommandCanExecute);
        }
        #endregion setupCommands

        #region addLoginCommandCanExecute
        private bool addLoginCommandCanExecute()
        {
            return this.MayEdit && this.Item != null;
        }
        #endregion addLoginCommandCanExecute

        #region addLoginCommandExecute
        private void addLoginCommandExecute()
        {
            this.navigationService.Navigate("AddLoginToUserViewModel");
        }
        #endregion addLoginCommandExecute

        #region addValueCommandCanExecute
        private bool addValueCommandCanExecute()
        {
            return this.MayEdit && this.Item != null;
        }
        #endregion addValueCommandCanExecute

        #region addValueCommandExecute
        private void addValueCommandExecute()
        {
            this.navigationService.Navigate("AddValueToUserViewModel");
        }
        #endregion addValueCommandExecute

        #region addRoleCommandCanExecute
        private bool addRoleCommandCanExecute()
        {
            return this.MayEdit && this.Item != null;
        }
        #endregion addRoleCommandCanExecute

        #region addRoleCommandExecute
        private void addRoleCommandExecute()
        {
            this.navigationService.Navigate("AddRoleToUserViewModel");
        }
        #endregion addRoleCommandExecute

        #region addOwnedRoleCommandCanExecute
        private bool addOwnedRoleCommandCanExecute()
        {
            return this.MayEdit && this.Item != null;
        }
        #endregion addOwnedRoleCommandCanExecute

        #region addOwnedRoleCommandExecute
        private void addOwnedRoleCommandExecute()
        {
            this.navigationService.Navigate("AddOwnedRoleToUserViewModel");
        }
        #endregion addOwnedRoleCommandExecute

        #region UpdateUI
        private void UpdateUI()
        {
            if (this.Item != null)
            {
                this.SendMessage("SelectedUser", this.Item);
            }
        }
        #endregion UpdateUI

        #region receiveMessage
        protected override bool receiveMessage(string Text, object Data)
        {
            bool result = false;
            return result;
        }
        #endregion receiveMessage

        #endregion Methods
    }
}
