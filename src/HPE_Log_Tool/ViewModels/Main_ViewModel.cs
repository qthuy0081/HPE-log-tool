using HPE_Log_Tool.Common;
using HPE_Log_Tool.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace HPE_Log_Tool.ViewModels
{
    public class Main_ViewModel : BaseViewModel
    {
        #region Fields
        private const string isOutCheckSmartCard = "InsertData - OUT_CheckSmartCard";
        private const string isOutCheckForceOpen = "InsertData - OUT_CheckForceOpen";
        private const string isOutCheckEtag = "InsertData - OUT_CheckEtag";
        #endregion

        #region Properties
        //private string _filePath = "C:/Users/thanh/Downloads/Log_MTC_2020/LogFolder/InsertTransaction_Log_20201020.txt";

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



        private Main_ViewModel _SelectedItem;
        public Main_ViewModel SelectedItem
        {
            get => _SelectedItem;
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    OnPropertyChanged();
                }
            }
        }

        private Visibility _tCSC = Visibility.Visible;
        public Visibility tCSC
        {
            get => _tCSC;
            set
            {
                if (_tCSC != value)
                {
                    _tCSC = value;
                    OnPropertyChanged();
                }
            }
        }

        private Visibility _tCFO;
        public Visibility tCFO
        {
            get => _tCFO;
            set
            {
                if (_tCFO != value)
                {
                    _tCFO = value;
                    OnPropertyChanged();
                }
            }
        }

        private Visibility _tCE;
        public Visibility tCE
        {
            get => _tCE;
            set
            {
                if (_tCE != value)
                {
                    _tCE = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<OUT_CheckSmartCard> _outCheckSmartCard;
        public ObservableCollection<OUT_CheckSmartCard> OUT_CheckSmartCards
        {
            get => _outCheckSmartCard;
            set
            {
                if (_outCheckSmartCard != value)
                {
                    _outCheckSmartCard = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<OUT_CheckForceOpen> _outCheckForceOpen;
        public ObservableCollection<OUT_CheckForceOpen> OUT_CheckForceOpens
        {
            get => _outCheckForceOpen;
            set
            {
                if (_outCheckForceOpen != value)
                {
                    _outCheckForceOpen = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<OUT_CheckEtag> _outCheckEtag;
        public ObservableCollection<OUT_CheckEtag> OUT_CheckEtags
        {
            get => _outCheckEtag;
            set
            {
                if (_outCheckEtag != value)
                {
                    _outCheckEtag = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<string> _tableList;
        public ObservableCollection<string> tableList
        {
            get => _tableList;
            set
            {
                if (_tableList != value)
                {
                    _tableList = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _selectedTable;
        public string SelectedTable
        {
            get => _selectedTable;
            set
            {
                if (_selectedTable != value)
                {
                    _selectedTable = value;
                    OnPropertyChanged();
                    ChangeTable(_selectedTable);
                }
            }
        }
        #endregion

        #region Methods
        public Main_ViewModel()
        {
            OUT_CheckSmartCards= new ObservableCollection<OUT_CheckSmartCard>();
            OUT_CheckForceOpens = new ObservableCollection<OUT_CheckForceOpen>();
            OUT_CheckEtags = new ObservableCollection<OUT_CheckEtag>();
            tableList = new ObservableCollection<string> { 
                "OUT_CheckSmartCard",
                "OUT_CheckForceOpen",
                "OUT_CheckEtag"
            };
            
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
                if (string.IsNullOrEmpty(IP))
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
                        string initDirect = string.Format(url, IP);
                        OpenFileDialog openFileDialog1 = new OpenFileDialog
                        {

                            InitialDirectory = initDirect,
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
            catch (Exception e)
            {

            }

        }

        // Read Log File 
        private void ReadLogFile()
        {
            try
            {
                // Clear all the list before reading log file
                OUT_CheckSmartCards.Clear();
                OUT_CheckForceOpens.Clear();
                OUT_CheckEtags.Clear();
                
                int startIndex;
                int endIndex;
                string json;
                string[] lines = File.ReadAllLines(Path);
                foreach (string line in lines)
                {
                    startIndex = line.IndexOf('{');
                    endIndex = line.IndexOf('}');
                    json = line.Substring(startIndex, (endIndex - startIndex) + 1);
                    if (line.Contains(isOutCheckSmartCard))
                    {
                        OUT_CheckSmartCard item = JsonConvert.DeserializeObject<OUT_CheckSmartCard>(json);
                        OUT_CheckSmartCards.Add(item);
                    }
                    else if (line.Contains(isOutCheckForceOpen))
                    {
                        OUT_CheckForceOpen item = JsonConvert.DeserializeObject<OUT_CheckForceOpen>(json);
                        OUT_CheckForceOpens.Add(item);
                    }
                    else if (line.Contains(isOutCheckEtag))
                    {
                        OUT_CheckEtag item = JsonConvert.DeserializeObject<OUT_CheckEtag>(json);
                        OUT_CheckEtags.Add(item);
                    }

                }
                MessageBox.Show("Load Succesfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Load Failed: " + ex.Message);
            }
        }

        // Bind datagrid by table 

        private void ChangeTable(string table)
        {
            switch (table)
            {
                case "OUT_CheckSmartCard":
                    {
                        tCSC = Visibility.Visible;
                        tCFO = Visibility.Hidden;
                        tCE = Visibility.Hidden;
                        break;
                    }
                case "OUT_CheckForceOpen":
                    {
                        tCFO = Visibility.Visible;
                        tCSC = Visibility.Hidden;
                        tCE = Visibility.Hidden;
                        break;
                    }
                case "OUT_CheckEtag":
                    {
                        tCE = Visibility.Visible;
                        tCSC = Visibility.Hidden;
                        tCFO = Visibility.Hidden;
                        break;
                    }
            }
            
        }

        
        #endregion

        #region Commands
        private ICommand _cmdReadLog;
        public ICommand cmdReadLog => _cmdReadLog ?? (_cmdReadLog = new RelayCommand(param => { ReadLogFile(); }, param => CanClick()));

        private ICommand _cmdCheck;
        public ICommand cmdCheck => _cmdCheck ?? (_cmdCheck = new RelayCommand(param => { Check(); }, param => CanClick()));

        private ICommand _cmdBrowse;
        public ICommand cmdBrowse => _cmdBrowse ?? (_cmdBrowse = new RelayCommand(param => { Browse(); }, param => CanBrowse()));
        #endregion
    }
}
