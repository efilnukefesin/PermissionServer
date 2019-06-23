using NET.efilnukefesin.Implementations.Mvvm.Attributes;
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
using System.Windows.Shapes;

namespace AdminApp.Views
{
    /// <summary>
    /// Interaktionslogik für AddValueToUserWindow.xaml
    /// </summary>
    [ViewModel("AddValueToUserViewModel")]
    [View("AddValueToUserWindow.xaml")]
    public partial class AddValueToUserWindow : Window
    {
        public AddValueToUserWindow()
        {
            InitializeComponent();
        }
    }
}
