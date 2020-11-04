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
        private const string fileName = "\\InsertTransaction_Log_";
        private const string fileExt = ".txt";
        private string[] filePathss;
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

        private List<LS_Shift> _shiftList;
        public List<LS_Shift> shiftList
        {
            get => _shiftList;
            set
            {
                if (_shiftList != value)
                {
                    _shiftList = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<string> _filePaths;
        public List<string> filePaths
        {
            get => _filePaths;
            set
            {
                if (_filePaths != value)
                {
                    _filePaths = value;
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

        private string _selectedShift;
        public string SelectedShift
        {
            get => _selectedShift;
            set
            {
                if (_selectedShift != value)
                {
                    _selectedShift = value;
                    OnPropertyChanged();
                    //ChangeShift(_selectedShift);
                }
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged();  
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
            filePaths = new List<string>();
            tableList = new ObservableCollection<string> { 
                "OUT_CheckSmartCard",
                "OUT_CheckForceOpen",
                "OUT_CheckEtag"
            };
            shiftList = new List<LS_Shift>();
            SelectedTable = tableList.FirstOrDefault();
            SelectedDate = new DateTime(2020, 10, 20);
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
                    MessageBox.Show("IP address is empty");
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
                        var fbd = new FolderBrowserDialog
                        {
                            SelectedPath = string.Format(url, IP)
                        };
                        
                        DialogResult result = fbd.ShowDialog();
                        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                        {
                            //InsertTransaction_Log_20201020
                             filePathss = Directory.GetFiles(fbd.SelectedPath).Where(w => w.Contains("InsertTransaction_Log_")).ToArray();
                             Path = fbd.SelectedPath;
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

        private void ClearData()
        {
            OUT_CheckSmartCards.Clear();
            OUT_CheckForceOpens.Clear();
            OUT_CheckEtags.Clear();
            filePaths.Clear();
        }

        private string toDateStamp(DateTime Date)
        {
            string date = Date.ToString("yyyyMMdd");
            return date;
        }

        private void ReadLogFile()
        {
            // Giờ có sẵn 1 list path của các fileLog đó rồi nè
            // Xong từ ngày cái mình lấy ra cái đuôi rồi lấy cái file tương ứng theo ngày đó (Ex: 20/10/2020 -> InsertTransaction_Log_20201020)
            // Với ngày hôm trước lẫn hôm sau luôn, thành ra phải đọc 3 file( Hôm trước, ngày được chọn, hôm sau)
            string previousDate = Path + fileName + toDateStamp(SelectedDate.AddDays(-1)) + fileExt;
            string currentDate = Path + fileName + toDateStamp(SelectedDate) + fileExt;
            string tomorrowDate = Path + fileName + toDateStamp(SelectedDate.AddDays(1)) + fileExt;
            string[] lines;
            int startIndex;
            int endIndex;
            string json;
            // Lấy được chuỗi tương ứng rồi, ez        
            // Trước khi lọc thì phải clear cái grid view đã nè
            ClearData();
            filePaths.Add(currentDate);
            filePaths.Add(tomorrowDate);
            // Clear xong thì load lại lên nè
            // Cái rồi đọc hết ra rồi mới lọc theo ca
            foreach (string filePath in filePaths)
            {
                if (File.Exists(filePath))
                {
                    try
                    {  
                        lines = File.ReadAllLines(filePath);
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
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Load Failed: " + ex.Message);
                    }
                }
                else
                    MessageBox.Show("Không tồn tại file log cho file tại đường dẫn :" + filePath);
            }
                MessageBox.Show("Load dữ liệu thành công!");
            // Xong rồi nè ahihi
        }
       

        // Change shift
        private void ChangeShift(string shift)
        {
            switch (shift)
            {
                case "All":
                    {
                        // 6:30:00 today -> 6:30:00 tomorrow
                        
                        break;
                    }
                case "1":
                    {
                        // 6:30:00 to 11:29:59 
                        break;
                    }
                case "2":
                    {
                        // 11:30:30 to 17:59:59
                        break;
                    }
                case "3":
                    {
                        // 18:00:00 today -> 6:29:59 tomorrow
                        break;
                    }
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


        // Load Config Form
        private void ShowConfig()
        {
            InitialPasswordView ip = new InitialPasswordView();
            ip.ShowDialog();
        }

        #endregion

        #region Commands
        private ICommand _cmdReadLog;
        public ICommand cmdReadLog => _cmdReadLog ?? (_cmdReadLog = new RelayCommand(param => { ReadLogFile(); }, param => CanClick()));

        private ICommand _cmdCheck;
        public ICommand cmdCheck => _cmdCheck ?? (_cmdCheck = new RelayCommand(param => { Check(); }, param => CanClick()));

        private ICommand _cmdBrowse;
        public ICommand cmdBrowse => _cmdBrowse ?? (_cmdBrowse = new RelayCommand(param => { Browse(); }, param => CanBrowse()));

        private ICommand _cmdLoadConfig;
        public ICommand cmdLoadConfig => _cmdLoadConfig ?? (_cmdLoadConfig = new RelayCommand(param => { ShowConfig(); }, param => CanClick()));
        #endregion
    }
}
