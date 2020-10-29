using HPE_Log_Tool.Models;
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
    public class Authen_ViewModel : BaseViewModel
    {
        ConfigModel _config = new ConfigModel();
        string _password;
        #region Props
        public Authen_ViewModel()
        {
            
            Config = ConfigModel.LoadConfig();
            if(Config.ConfigPassword == null)
            {
                Config.ConfigPassword = "ITD2020";
                ConfigModel.SaveConfig(Config);
            }
        }
        public ConfigModel Config
        {
            get => _config;
            set
            {
                if (_config != value)
                {
                    _config = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Method
        private void verifyUser()
        {
            if(_password == Config.ConfigPassword)
            {
                CloseWindow();
                ContainerView view = new ContainerView();
                view.ShowDialog();
            }
            else
            {


                MessageBox.Show("Wrong password. Try again!");
            }
        }

        #endregion

        #region Cmd
        private ICommand _loginCmd;
        public ICommand loginCmd => _loginCmd ?? (_loginCmd = new RelayCommand(param=> { verifyUser(); } ));
        #endregion
    }
}
