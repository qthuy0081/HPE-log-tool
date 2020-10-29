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
        ContainerView container = new ContainerView();

        private string _ip;
        public string IP
        {
            get => _ip;
            set
            {
                if (_ip != value)
                {
                    _ip = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _path;
        public string Path
        {
            get => _path;
            set
            {
                if (_path != value)
                {
                    _path = value;
                    OnPropertyChanged();
                }
            }
        }


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

        private void Check()
        {       
            Utility.checkExist(Path);
        }
        private bool CanBrowse()
        {
            return true;
        }
        private void Browse()
        {
            
            try
            {
                string url = @"\\{0}";
                if(string.IsNullOrEmpty(IP))
                {
                    MessageBox.Show("IP address does not exist");
                    return;
                }
                bool rs = IPAddress.TryParse(IP, out IPAddress address); // chỗ này ko bị exception, nếu parse dc thì rs = true, address có kq
                if (rs)
                {
                    Ping ping = new Ping();
                    PingReply pong = ping.Send(address);
                    if (pong.Status != IPStatus.Success)

                    {                      
                        MessageBox.Show("IP address does not exist");
                    }
                    else
                    {
                        OpenFileDialog openFileDialog1 = new OpenFileDialog
                        {

                            InitialDirectory = string.Format(url, IP),
                            Title = "Browse Text Files",
                            CheckPathExists = true,
                            CheckFileExists = true,


                            DefaultExt = "txt",
                            Filter = "txt files (*.txt)|*.txt",
                            FilterIndex = 2,
                            RestoreDirectory = true,

                            ReadOnlyChecked = true,
                            ShowReadOnly = true
                        };

                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            Path = openFileDialog1.FileName;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect IP address");

                }
            }
            catch(Exception e)
            {

            }
            
        }
        #endregion

        #region Commands
        private ICommand _cmdLoadConfig;
        public ICommand cmdLoadConfig => _cmdLoadConfig ?? (_cmdLoadConfig = new RelayCommand(param => { ShowConfig(); }, param => CanClick()));

        private ICommand _cmdCheck;
        public ICommand cmdCheck => _cmdCheck ?? (_cmdCheck = new RelayCommand(param => { Check(); }, param => CanClick()));

        private ICommand _cmdBrowse;
        public ICommand cmdBrowse => _cmdBrowse ?? (_cmdBrowse = new RelayCommand(param => { Browse(); }, param => CanBrowse()));
        #endregion
    }
}
