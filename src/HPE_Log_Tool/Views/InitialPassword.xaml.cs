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
    /// Interaction logic for InitialPassword.xaml
    /// </summary>
    public partial class InitialPassword : Window
    {
        MainWindow mw = new MainWindow();
        public InitialPassword()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(pwdPassword.Password.ToString() == "1234")
            {
                mw.Show();
                this.Hide();
            }
        }
    }
}
