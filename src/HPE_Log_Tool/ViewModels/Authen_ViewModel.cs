﻿using HPE_Log_Tool.Models;
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
    class Authen_ViewModel : BaseViewModel
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
        public void verifyUser()
        {
            if(_password == _config.ConfigPassword)
            {
                MessageBox.Show("Login successfully");
            }
            else
            {
                MessageBox.Show("Wrong password. Try again!");
            }
        }

        #endregion

        #region Cmd
        ICommand _loginCmd;
        ICommand loginCmd => _loginCmd ?? (_loginCmd = new RelayCommand(param=> { verifyUser(); } ));
        #endregion
    }
}
