﻿using HPE_Log_Tool.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HPE_Log_Tool.Views
{
    /// <summary>
    /// Interaction logic for config.xaml
    /// </summary>
    public partial class ConfigView : Window
    {
        Config_ViewModel viewmodel;
        public ConfigView()
        {
            InitializeComponent();
            viewmodel = new Config_ViewModel();
            DataContext = viewmodel;
        }
        
    }
}
