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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdminApp.Views
{
    [ViewModel("ViewPermissionsViewModel")]
    [View("ViewPermissionsView.xaml")]
    /// <summary>
    /// Interaktionslogik für ViewPermissionsView.xaml
    /// </summary>
    public partial class ViewPermissionsView : Page
    {
        public ViewPermissionsView()
        {
            InitializeComponent();
        }
    }
}
