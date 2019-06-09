using NET.efilnukefesin.Contracts.Mvvm;
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

namespace AdminApp.UserControls
{
    /// <summary>
    /// Interaktionslogik für AddPermissionUserControl.xaml
    /// </summary>
    public partial class AddPermissionUserControl : UserControl, INotifyPropertyChanged
    {
        #region Properties

        public bool IsIdle { get; set; } = true;

        #endregion Properties

        #region Construction

        public AddPermissionUserControl()
        {
            InitializeComponent();
        }

        #endregion Construction

        #region Methods

        #endregion Methods

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events
    }
}
