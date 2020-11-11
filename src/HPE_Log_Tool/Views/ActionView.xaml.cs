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
using System.Windows.Shapes;

namespace HPE_Log_Tool.Views
{
    /// <summary>
    /// Interaction logic for ActionView.xaml
    /// </summary>
   
    public partial class ActionView : Window
    {
        private ActionPasswordViewModel viewmodel;
        public ActionView()
        {
            InitializeComponent();
            viewmodel = new ActionPasswordViewModel();
            viewmodel.CloseWindowRequest += closeWindow;
            DataContext = viewmodel;
        }
        private void closeWindow()
        {
            if(viewmodel != null)
            {
                viewmodel.Dispose();
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
