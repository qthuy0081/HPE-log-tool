using HPE_Log_Tool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HPE_Log_Tool.ViewModels
{
    class ActionPasswordViewModel : BaseViewModel
    {
        private AppConfig config;
        private string _password;
        public ActionPasswordViewModel ()
        {
            config = AppConfig.LoadConfig();
        }

        public string Password 
        { 
            get => _password;
            set
            {
                if(_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }
        private void verifyPassword()
        {
            if(config.InsertDB.CommandPassword == Password)
            {
                CloseWindow();
            }
            else
            {
                MessageBox.Show("Wrong password");
            }
        }

        private ICommand _checkPassCmd;
        public ICommand checkCmd => _checkPassCmd ?? (_checkPassCmd = new RelayCommand(param => { verifyPassword(); }));
    }
}
