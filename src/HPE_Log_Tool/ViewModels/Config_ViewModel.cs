using HPE_Log_Tool.Common;
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
        private string _password;
        private Visibility _passwordVisibility;
        private Visibility _passwordVisibility1;
        private Visibility _passwordVisibility2;
        private bool _IsPasswordUnmask;
        private bool _IsPasswordUnmask1;
        private bool _IsPasswordUnmask2;
        private string _oldPassword;
        private string _newPassword;
        private string _confirmPassword;
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
                _newPassword = value;
                OnPropertyChanged();
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
        public Visibility PasswordVisibility
        {
         
        get => _passwordVisibility;
            set
            {
                if (_passwordVisibility != value)
                {   
                    
                    _passwordVisibility = value;
                    OnPropertyChanged();
                }
            }

        }
        public Visibility PasswordVisibility1
        {

            get => _passwordVisibility1;
            set
            {
                if (_passwordVisibility1!= value)
                {

                    _passwordVisibility1 = value;
                    OnPropertyChanged();
                }
            }

        }
        public Visibility PasswordVisibility2
        {

            get => _passwordVisibility2;
            set
            {
                if (_passwordVisibility2 != value)
                {

                    _passwordVisibility2 = value;
                    OnPropertyChanged();
                }
            }

        }
        public bool IsPasswordUnmask
        {

            get => _IsPasswordUnmask;
            set
            {
                if (_IsPasswordUnmask != value)
                {
                    _IsPasswordUnmask = value;
                    OnPropertyChanged();
                    if (_IsPasswordUnmask)
                    {
                        PasswordVisibility = Visibility.Visible;
                    }    
                    else
                    {
                        PasswordVisibility = Visibility.Collapsed;
                    }    
                }
            }

        }
        public bool IsPasswordUnmask1
        {

            get => _IsPasswordUnmask1;
            set
            {
                if (_IsPasswordUnmask1 != value)
                {
                    _IsPasswordUnmask1 = value;
                    OnPropertyChanged();
                    if (_IsPasswordUnmask1)
                    {
                        PasswordVisibility1 = Visibility.Visible;
                    }
                    else
                    {
                        PasswordVisibility1 = Visibility.Collapsed;
                    }
                }
            }

        }
        public bool IsPasswordUnmask2
        {

            get => _IsPasswordUnmask2;
            set
            {
                if (_IsPasswordUnmask2 != value)
                {
                    _IsPasswordUnmask2 = value;
                    OnPropertyChanged();
                    if (_IsPasswordUnmask2)
                    {
                        PasswordVisibility2 = Visibility.Visible;
                    }
                    else
                    {
                        PasswordVisibility2 = Visibility.Collapsed;
                    }
                }
            }

        }

        //Constructor
        public Config_ViewModel()
        {
            Config = AppConfig.LoadConfig();
            CompareDbConfig = Utility.DeepClone<ConfigModel>(_appConfig.CompareDB); 
            InsertDbConfig = Utility.DeepClone<ConfigModel>(_appConfig.InsertDB); 
            if (Config.AuthenPassword == null)
            {
                Config.AuthenPassword = "ITD2020";
                AppConfig.SaveConfig(Config);
            }
            PasswordVisibility = Visibility.Hidden; 
            PasswordVisibility1 = Visibility.Hidden; 
            PasswordVisibility2 = Visibility.Hidden; 
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
            
            _appConfig.InsertDB = InsertDbConfig;
            _appConfig.CompareDB = CompareDbConfig;
            bool ret = AppConfig.SaveConfig(Config);

            if(ret)
            {
                MessageBox.Show("Save config successfully!");
            } else
            {
                MessageBox.Show("Save config failed!");
            }   
        }
        private void checkConnection(ConfigModel config)
        {
            string connectionString = DbHelper.GetConnectionString(config.DatabaseServer, config.DatabaseName, config.DatabaseUser, config.DatabasePassword, config.DatabaseTimeout.ToString());
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
            if(String.IsNullOrEmpty(NewPassword))
               return false;
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
        private void changePassword()
        {
            if (OldPassword == Config.AuthenPassword && NewPassword == ConfirmPassword)
            {
                Config.AuthenPassword = NewPassword;
                if (AppConfig.SaveConfig(_appConfig))
                {
                    MessageBox.Show("Change password successfully!");
                }
            }
            else
            {
                MessageBox.Show("Failed");
            }
        }
        
        #endregion
        #region Command
        private ICommand _saveConfigCmd;
        public ICommand saveconfigCmd => _saveConfigCmd ?? (_saveConfigCmd = new RelayCommand(param => { saveConfig(); }));

        private ICommand _checkCompareDbConnectionCmd;
        public ICommand checkCompareDbConnectionCmd => _checkCompareDbConnectionCmd ?? (_checkCompareDbConnectionCmd = new RelayCommand(param => { checkConnection(CompareDbConfig); }));
        private ICommand _checkInsertDbConnectionCmd;
        public ICommand checkInsertDbConnectionCmd => _checkInsertDbConnectionCmd ?? (_checkInsertDbConnectionCmd = new RelayCommand(param => { checkConnection(InsertDbConfig); }));
        private ICommand _loginCmd;
        public ICommand loginCmd => _loginCmd ?? (_loginCmd = new RelayCommand(param=> { verifyUser(); } ));

        private ICommand _changePass;
        public ICommand changePass => _changePass ?? (_changePass = new RelayCommand(param => { changePassword(); }, p => CanClick()));
        #endregion
    }
}
