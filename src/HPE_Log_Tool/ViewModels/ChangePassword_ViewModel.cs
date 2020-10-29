using HPE_Log_Tool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace HPE_Log_Tool.ViewModels
{
    public class ChangePassword_ViewModel : BaseViewModel
    {
        private string _oldPassword;
        private string _newPassword;
        private string _confirmPassword;
        private ConfigModel config;
        public ChangePassword_ViewModel()
        {
            config = ConfigModel.LoadConfig();
        }

        public string OldPassword 
        { 
            get => _oldPassword;
            set
            {
                if(_oldPassword != value)
                {
                    _oldPassword = value;
                    OnPropertyChanged();
                }
            }
        }
        public string NewPassword 
        { 
            get => _newPassword;
            set
            {
                if(_newPassword != value)
                {
                    _newPassword = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ConfirmPassword 
        { 
            get => _confirmPassword;
            set
            {
                if(_confirmPassword != value)
                {
                    _confirmPassword = value;
                    OnPropertyChanged();
                }

            }
        }
        private void changePassword()
        {
            if(OldPassword == config.ConfigPassword && NewPassword == ConfirmPassword)
            {
                config.ConfigPassword = NewPassword;
                if(ConfigModel.SaveConfig(config))
                {
                    MessageBox.Show("Change password successfully!");
                }
            }
        }
        private ICommand _changePass;
        public ICommand changePass => _changePass ?? (_changePass = new RelayCommand(param => { changePassword(); }));
    }
}

