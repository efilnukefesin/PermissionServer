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

        #endregion Properties

        #region Construction
        public UserDetailsControl()
        {
            InitializeComponent();
        }
        #endregion Construction

        #region Methods
        private void UpdateUI()
        {
            
        }
        #endregion Methods
    }
}
