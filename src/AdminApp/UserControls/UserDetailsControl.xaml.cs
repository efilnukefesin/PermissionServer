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

        public User Item2 { get; set; }

        #endregion Properties

        #region Construction
        public UserDetailsControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion Construction

        #region Methods
        private void UpdateUI()
        {
            this.Item2 = this.Item;
        }
        #endregion Methods
    }
}
