using HPE_Log_Tool.ViewModels;
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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePasswordView : UserControl
    {
        ChangePassword_ViewModel viewmodel;

        public ChangePasswordView()
        {
            InitializeComponent();
            viewmodel = new ChangePassword_ViewModel();
            DataContext = viewmodel;
        }
    }
}
