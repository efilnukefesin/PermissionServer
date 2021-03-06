﻿using NET.efilnukefesin.Implementations.Mvvm.Attributes;
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
    /// Interaktionslogik für AddOwnedRoleToUserWindow.xaml
    /// </summary>
    [ViewModel("AddOwnedRoleToUserViewModel")]
    [View("AddOwnedRoleToUserWindow.xaml")]
    public partial class AddOwnedRoleToUserWindow : Window
    {
        public AddOwnedRoleToUserWindow()
        {
            InitializeComponent();
        }
    }
}
