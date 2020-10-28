using HPE_Log_Tool.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HPE_Log_Tool.ViewModels
{
    public class MainWindow_VM : BaseViewModel
    {
        #region Fields
        #endregion

        #region Properties
        Container container = new Container();
        #endregion

        #region Methods
        public MainWindow_VM()
        {
            
        }

        // Load Config Form
        private void ShowConfig()
        {
            InitialPassword ip = new InitialPassword();
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
