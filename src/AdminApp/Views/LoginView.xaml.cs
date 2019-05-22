using System;
using System.Collections.Generic;
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

namespace AdminApp.Views
{
    /// <summary>
    /// Interaktionslogik für LoginView.xaml
    /// </summary>
    public partial class LoginView : Page
    {
        public LoginView()
        {
            InitializeComponent();
        }

        #region Methods

        #region PwPassword_PasswordChanged
        private void PwPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //https://stackoverflow.com/questions/1483892/how-to-bind-to-a-passwordbox-in-mvvm
            //not 100% Mvvm but secure and easy
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).SecurePassword = ((PasswordBox)sender).SecurePassword;
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
            }
        }
        #endregion PwPassword_PasswordChanged

        #endregion Methods
    }
}
