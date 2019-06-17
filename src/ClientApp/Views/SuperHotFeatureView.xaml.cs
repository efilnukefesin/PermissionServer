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

namespace ClientApp.Views
{
    /// <summary>
    /// Interaktionslogik für SuperHotFeatureView.xaml
    /// </summary>
    [ViewModel("SuperHotFeatureViewModel")]
    [View("SuperHotFeatureView.xaml")]
    public partial class SuperHotFeatureView : Page
    {
        public SuperHotFeatureView()
        {
            InitializeComponent();
        }
    }
}
