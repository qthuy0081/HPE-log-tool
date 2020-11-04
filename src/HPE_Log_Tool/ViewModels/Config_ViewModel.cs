using HPE_Log_Tool.Models;
using HPE_Log_Tool.Views;
using ITD_Review_license__plates.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
namespace HPE_Log_Tool.ViewModels
{
    public class Config_ViewModel : BaseViewModel
    {
        #region Props
        string _password;
        private ObservableCollection<Station> _stations;
        private AppConfig _appConfig;
        private ConfigModel _compareDbConfig = new ConfigModel();
        private ConfigModel _insertDbConfig = new ConfigModel();
       
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
        public AppConfig Config
        {
            get => _appConfig;
            set 
            { 
                if(_appConfig != value)
                {
                    _appConfig = value;
                    OnPropertyChanged();
                }
            }
        }
        

        public ConfigModel CompareDbConfig
        {
            get => _compareDbConfig;
            set
            {
                if(_compareDbConfig != value)
                {
                    _compareDbConfig = value;
                    OnPropertyChanged();
                }
            }
        }
        public ConfigModel InsertDbConfig
        {
            get => _insertDbConfig;
            set
            {
                if(_insertDbConfig != value)
                {
                    _insertDbConfig = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<Station> Stations
        {
            get => _stations;
            set
            {
                if(_stations != value)
                {
                    _stations = value;
                    OnPropertyChanged();
                }
            }
        }
        //Constructor
        public Config_ViewModel()
        {
            Config = AppConfig.LoadConfig();
            CompareDbConfig = _appConfig.CompareDB;
            InsertDbConfig = _appConfig.InsertDB;
            if (Config.AuthenPassword == null)
            {
                Config.AuthenPassword = "ITD2020";
                AppConfig.SaveConfig(Config);
            }
            Stations = new ObservableCollection<Station>
            {
                new Station(0,"00","Trung Tâm"),
                new Station(1,"01","Đầu Tuyền"),
                new Station(2,"01","QL39"),
                new Station(3,"02","QL38"),
                new Station(4,"03","QL10"),
                new Station(5,"04","Cuối Tuyến"),
                new Station(6,"05","Đình Vũ"),
                new Station(7,"06","TL353"),
            };
        }
        #endregion
   
        #region Method
        private void saveConfig()
        {
            bool ret = AppConfig.SaveConfig(_appConfig);
            if(ret)
            {
                MessageBox.Show("Save config successfully!");
            } else
            {
                MessageBox.Show("Save config failed!");
            }   
        }
        private void checkConnection()
        {
            string connectionString = DbHelper.GetConnectionString(CompareDbConfig.DatabaseServer, CompareDbConfig.DatabaseName, CompareDbConfig.DatabaseUser, CompareDbConfig.DatabasePassword, CompareDbConfig.DatabaseTimeout.ToString());
            DbHelper db = new DbHelper(connectionString);
            bool ret = db.CheckOpenConnection();
            if (ret)
            {
                MessageBox.Show("Connect to Database successfully!");
            }
            else
            {
                MessageBox.Show("Connect to Database failed");
            }


        }
        private bool CanClick()
        {
            return true;
        }
        private void verifyUser()
        {
            Config = AppConfig.LoadConfig();
            if (Password == Config.AuthenPassword)
            {
                CloseWindow();
                ConfigView view = new ConfigView();
                view.Show();
            }
            else
            {
                MessageBox.Show("Wrong password. Try again!");
            }
        }
        #endregion
        #region Command
        private ICommand _saveConfigCmd;
        public ICommand saveconfigCmd => _saveConfigCmd ?? (_saveConfigCmd = new RelayCommand(param => { saveConfig(); },p => CanClick()));

        private ICommand _checkDbConnectionCmd;
        public ICommand checkDbConnectionCmd => _checkDbConnectionCmd ?? (_checkDbConnectionCmd = new RelayCommand(param => { checkConnection(); }));
        private ICommand _loginCmd;
        public ICommand loginCmd => _loginCmd ?? (_loginCmd = new RelayCommand(param=> { verifyUser(); } ));
        #endregion
    }
}
