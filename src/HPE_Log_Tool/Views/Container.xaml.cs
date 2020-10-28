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
using System.Windows.Shapes;

namespace HPE_Log_Tool.Views
{
    /// <summary>
    /// Interaction logic for Container.xaml
    /// </summary>
    public partial class Container : Window
    {
        private config cf = new config();
        private ChangePassword cp = new ChangePassword();
        public Container()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = cf;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            contentControl.Content = cp;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            contentControl.Content = cf;
        }
    }
}
