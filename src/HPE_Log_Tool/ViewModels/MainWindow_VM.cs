using HPE_Log_Tool.Common;
using HPE_Log_Tool.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace HPE_Log_Tool.ViewModels
{
    public class MainWindow_VM : BaseViewModel
    {
        #region Fields
        #endregion

        #region Properties
        

        


        #endregion

        #region Methods
        public MainWindow_VM()
        {
            
        }

        // Load Config Form
        private void ShowConfig()
        {
            InitialPasswordView ip = new InitialPasswordView();
            ip.ShowDialog();
        }

        private bool CanClick()
        {
            return true;
        }

        #endregion

        #region Commands

        private ICommand _cmdLoadConfig;
        public ICommand cmdLoadConfig => _cmdLoadConfig ?? (_cmdLoadConfig = new RelayCommand(param => { ShowConfig(); }, param => CanClick()));


        #endregion
    }
}
