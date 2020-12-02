using HPE_Log_Tool.Common;
using HPE_Log_Tool.Models;
using HPE_Log_Tool.Views;
using ITD_Review_license__plates.Common;
using MoreLinq;
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
    public class MainUserControl_ViewModel : BaseViewModel
    {
        #region Fields
        private const string isOutCheckSmartCard = "WriteLogPrepareCommitReceipt - OUT_CheckSmartCard";
        private const string isOutCheckForceOpen = "WriteLogPrepareCommitReceipt - OUT_CheckForceOpen";
        private const string isOutCheckEtag = "WriteLogPrepareCommitReceipt - OUT_CheckEtag";
        private const string isInCheckSmartCard = "InsertData - IN_CheckSmartCard";
        private const string isInCheckForceOpen = "InsertData - IN_CheckForceOpen";
        private const string fileNameIN = "\\LogFolder\\InsertTransaction_Log_";
        private const string fileNameOUT = "\\Logs\\TraceLog_";
        private const string fileExt = ".txt";
        private DateTime startTime;
        private DateTime endTime;
        private AppConfig config;
        string insertDbConn = "";
        string compareDbConn = "";
        

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
                    Path = $"\\\\{IP}{AppConfig.path}" ;
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
        private int _rowCount = 0;
        
        public int RowCount
        {
            get => _rowCount;
            set
            {
                if(_rowCount != value)
                {
                    _rowCount = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _timeOff = 30;
        public int Timeoff
        {
            get => _timeOff;
            set
            {
                if (_timeOff != value)
                {
                    _timeOff = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _isMissingTrans;
        public bool IsMissingTrans
        {
            get => _isMissingTrans;
            set
            {
                if(_isMissingTrans != value)
                {
                    _isMissingTrans = value;
                    OnPropertyChanged();
                    ChangeShift();
                    
                }
            }
        }

        private MainUserControl_ViewModel _SelectedItem;
        public MainUserControl_ViewModel SelectedItem
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

        private Visibility _tICS;
        public Visibility tICS
        {
            get => _tICS;
            set
            {
                if (_tICS != value)
                {
                    _tICS = value;
                    OnPropertyChanged();
                }
            }
        }

        private Visibility _tICFO;
        public Visibility tICFO
        {
            get => _tICFO;
            set
            {
                if (_tICFO != value)
                {
                    _tICFO = value;
                    OnPropertyChanged();
                }
            }
        }
        private Visibility _tDup;
        public Visibility tDup
        {
            get => _tDup;
            set
            {
                if (_tDup != value)
                {
                    _tDup = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<IN_CheckSmartCard> _inCheckSmartCard;
        public ObservableCollection<IN_CheckSmartCard> IN_CheckSmartCards
        {
            get => _inCheckSmartCard;
            set
            {
                if (_inCheckSmartCard != value)
                {
                    _inCheckSmartCard = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<IN_CheckSmartCard> _inCheckSmartCardFiltered;
        public ObservableCollection<IN_CheckSmartCard> IN_CheckSmartCardsFiltered
        {
            get => _inCheckSmartCardFiltered;
            set
            {
                if (_inCheckSmartCardFiltered != value)
                {
                    _inCheckSmartCardFiltered = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<IN_CheckForceOpen> _inCheckForceOpen;
        public ObservableCollection<IN_CheckForceOpen> IN_CheckForceOpens
        {
            get => _inCheckForceOpen;
            set
            {
                if (_inCheckForceOpen != value)
                {
                    _inCheckForceOpen = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<IN_CheckForceOpen> _inCheckForceOpenFiltered;
        public ObservableCollection<IN_CheckForceOpen> IN_CheckForceOpensFiltered
        {
            get => _inCheckForceOpenFiltered;
            set
            {
                if (_inCheckForceOpenFiltered != value)
                {
                    _inCheckForceOpenFiltered = value;
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

        private ObservableCollection<OUT_CheckSmartCard> _outCheckSmartCardFiltered;
        public ObservableCollection<OUT_CheckSmartCard> OUT_CheckSmartCardsFiltered
        {
            get => _outCheckSmartCardFiltered;
            set
            {
                if (_outCheckSmartCardFiltered != value)
                {
                    _outCheckSmartCardFiltered = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<OUT_CheckForceOpen> _outCheckForceOpenFiltered;
        public ObservableCollection<OUT_CheckForceOpen> OUT_CheckForceOpensFiltered
        {
            get => _outCheckForceOpenFiltered;
            set
            {
                if (_outCheckForceOpenFiltered != value)
                {
                    _outCheckForceOpenFiltered = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<OUT_CheckEtag> _outCheckEtagFiltered;
        public ObservableCollection<OUT_CheckEtag> OUT_CheckEtagsFiltered
        {
            get => _outCheckEtagFiltered;
            set
            {
                if (_outCheckEtagFiltered != value)
                {
                    _outCheckEtagFiltered = value;
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
        private LS_Shift _shift;
        public LS_Shift Shift
        {
            get => _shift;
            set
            {
                if (_shift != value)
                {
                    _shift = value;
                    OnPropertyChanged();
                    ChangeShift();
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
                    ChangeShift();
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
        //IsCheckedBooleanProperty
        private bool _IsCheckedBooleanProperty;
        public bool IsCheckedBooleanProperty
        {
            get => _IsCheckedBooleanProperty;
            set
            {
                if (_IsCheckedBooleanProperty != value)
                {
                    _IsCheckedBooleanProperty = value;
                    OnPropertyChanged();
                    ChangeShift();
                }
            }
        }
        #endregion

        #region Methods
        public MainUserControl_ViewModel()
        {
            config = AppConfig.LoadConfig();
            insertDbConn = DbHelper.GetConnectionString(config.InsertDB.DatabaseServer, config.InsertDB.DatabaseName, config.InsertDB.DatabaseUser, config.InsertDB.DatabasePassword, config.InsertDB.DatabaseTimeout.ToString());
            compareDbConn = DbHelper.GetConnectionString(config.CompareDB.DatabaseServer, config.CompareDB.DatabaseName, config.CompareDB.DatabaseUser, config.CompareDB.DatabasePassword, config.CompareDB.DatabaseTimeout.ToString());
            OUT_CheckSmartCards = new ObservableCollection<OUT_CheckSmartCard>();
            OUT_CheckForceOpens = new ObservableCollection<OUT_CheckForceOpen>();
            OUT_CheckEtags = new ObservableCollection<OUT_CheckEtag>();
            OUT_CheckSmartCardsFiltered = new ObservableCollection<OUT_CheckSmartCard>();
            OUT_CheckForceOpensFiltered = new ObservableCollection<OUT_CheckForceOpen>();
            OUT_CheckEtagsFiltered = new ObservableCollection<OUT_CheckEtag>();
            IN_CheckSmartCards = new ObservableCollection<IN_CheckSmartCard>();
            IN_CheckSmartCardsFiltered = new ObservableCollection<IN_CheckSmartCard>();
            IN_CheckForceOpens = new ObservableCollection<IN_CheckForceOpen>();
            IN_CheckForceOpensFiltered = new ObservableCollection<IN_CheckForceOpen>();
            filePaths = new List<string>();
            tableList = new ObservableCollection<string> { 
                "OUT_CheckSmartCard",
                "OUT_CheckForceOpen",
                "OUT_CheckEtag",
                "IN_CheckSmartCard",
                "IN_CheckForceOpen"
            };
            shiftList = DataProvider.GetShifts(true);
            Shift = shiftList.FirstOrDefault();
            SelectedTable = tableList.FirstOrDefault();
            SelectedDate = new DateTime(2020, 10, 20);
            Path = "\\TLS";
        }


        private bool CanClick()
        {
            return true;
        }

        private void Check()
        {
            Utility.checkExist(Path, IP);
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
                    //MessageBox.Show("IP address is empty");
                    //return;
                    var fbd = new FolderBrowserDialog
                    {                       
                    };

                    DialogResult result = fbd.ShowDialog();
                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        //InsertTransaction_Log_20201020
                        Path = fbd.SelectedPath;
                    }
                }
                else
                {
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

                                Path = fbd.SelectedPath;
                            }
                        }
                    }
                }
              
                //else
                //{
                //    MessageBox.Show("Incorrect IP address");

                //}
            }
            catch (Exception ex)
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
            string currentOutDate = @"\\" + IP + Path + fileNameOUT + SelectedDate.ToString("yyyy-MM-dd") + ".log";
            string tomorrowOutDate = @"\\" + IP + Path + fileNameOUT + SelectedDate.ToString("yyyy-MM-dd") + ".log";
            string currentInDate = @"\\" + IP + Path + fileNameIN + toDateStamp(SelectedDate) + fileExt;
            string tomorrowInDate = @"\\" + IP + Path + fileNameIN + toDateStamp(SelectedDate.AddDays(1)) + fileExt;
            string[] lines;
            int startIndex;
            int endIndex;
            string json;
            // Lấy được chuỗi tương ứng rồi, ez        
            // Trước khi lọc thì phải clear cái grid view đã nè
            ClearData();
            filePaths.Add(currentInDate);
            filePaths.Add(tomorrowInDate);
            filePaths.Add(currentOutDate);
            filePaths.Add(tomorrowOutDate);
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
                            if (line.Contains(isInCheckSmartCard))
                            {
                                startIndex = line.IndexOf('{');
                                endIndex = line.IndexOf('}');
                                json = line.Substring(startIndex, (endIndex - startIndex) + 1);
                                IN_CheckSmartCard item = JsonConvert.DeserializeObject<IN_CheckSmartCard>(json);
                                IN_CheckSmartCards.Add(item);
                            }
                            else if (line.Contains(isInCheckForceOpen))
                            {
                                startIndex = line.IndexOf('{');
                                endIndex = line.IndexOf('}');
                                json = line.Substring(startIndex, (endIndex - startIndex) + 1);
                                IN_CheckForceOpen item = JsonConvert.DeserializeObject<IN_CheckForceOpen>(json);
                                IN_CheckForceOpens.Add(item);
                            }
                            else if (line.Contains(isOutCheckSmartCard))
                            {
                                startIndex = line.IndexOf('{');
                                endIndex = line.IndexOf('}');
                                json = line.Substring(startIndex, (endIndex - startIndex) + 1);
                                OUT_CheckSmartCard item = JsonConvert.DeserializeObject<OUT_CheckSmartCard>(json);
                                OUT_CheckSmartCards.Add(item);
                            }
                            else if (line.Contains(isOutCheckForceOpen))
                            {
                                startIndex = line.IndexOf('{');
                                endIndex = line.IndexOf('}');
                                json = line.Substring(startIndex, (endIndex - startIndex) + 1);
                                OUT_CheckForceOpen item = JsonConvert.DeserializeObject<OUT_CheckForceOpen>(json);
                                OUT_CheckForceOpens.Add(item);
                            }
                            else if (line.Contains(isOutCheckEtag))
                            {
                                startIndex = line.IndexOf('{');
                                endIndex = line.IndexOf('}');
                                json = line.Substring(startIndex, (endIndex - startIndex) + 1);
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
               
            }
            FirstLoad();
            MessageBox.Show("Load dữ liệu thành công!");
            // Xong rồi nè ahihi
        }

        private void FirstLoad()
        {
            startTime = SelectedDate.Add(new TimeSpan(6, 30, 00));
            endTime = SelectedDate.AddDays(1).Add(new TimeSpan(6, 29, 59));
            OUT_CheckSmartCardsFiltered = new ObservableCollection<OUT_CheckSmartCard>(from item in OUT_CheckSmartCards where item.CheckDate >= startTime && item.CheckDate <= endTime orderby item.CheckDate select item);
            OUT_CheckForceOpensFiltered = new ObservableCollection<OUT_CheckForceOpen>(from item in OUT_CheckForceOpens where item.CheckDate >= startTime && item.CheckDate <= endTime orderby item.CheckDate select item);
            OUT_CheckEtagsFiltered = new ObservableCollection<OUT_CheckEtag>(from item in OUT_CheckEtags where item.CheckDate >= startTime && item.CheckDate <= endTime orderby item.CheckDate select item);
            IN_CheckSmartCardsFiltered = new ObservableCollection<IN_CheckSmartCard>(from item in IN_CheckSmartCards where item.CheckDate >= startTime && item.CheckDate <= endTime orderby item.CheckDate select item);
            IN_CheckForceOpensFiltered = new ObservableCollection<IN_CheckForceOpen>(from item in IN_CheckForceOpens where item.CheckDate >= startTime && item.CheckDate <= endTime orderby item.CheckDate select item);
            CountLogRow();
        }
        
        private void FilterTransByTime(int shift = 0)
        {
            switch (SelectedTable)
            {
                // IN_Check Section
                case "IN_CheckSmartCard":
                    {
                        IN_CheckSmartCardsFiltered = new ObservableCollection<IN_CheckSmartCard>(from item in IN_CheckSmartCards where item.CheckDate >= startTime && item.CheckDate <= endTime && (shift == 0 || shift == item.ShiftID) orderby item.CheckDate select item);
                        if (IsMissingTrans)
                        {
                            IN_CheckSmartCardsFiltered = DataProvider.filterMissingTrans_InCheckSmartCard(IN_CheckSmartCardsFiltered, compareDbConn);
                        }
                        DuplicatedSmartID(IsCheckedBooleanProperty);
                        break;
                    }
                case "IN_CheckForceOpen":
                    {
                        IN_CheckForceOpensFiltered = new ObservableCollection<IN_CheckForceOpen>(from item in IN_CheckForceOpens where item.CheckDate >= startTime && item.CheckDate <= endTime && (shift == 0 || shift == item.ShiftID) orderby item.CheckDate select item);
                        if (IsMissingTrans)
                        {
                            IN_CheckForceOpensFiltered = DataProvider.filterMissingTrans_InCheckForceOpen(IN_CheckForceOpensFiltered, compareDbConn);
                        }
                        DuplicatedSmartID(IsCheckedBooleanProperty);
                        break;
                    }
                case "OUT_CheckSmartCard":
                    {
                        OUT_CheckSmartCardsFiltered = new ObservableCollection<OUT_CheckSmartCard>(from item in OUT_CheckSmartCards where item.CheckDate >= startTime && item.CheckDate <= endTime && (shift == 0 || shift == item.ShiftID) orderby item.CheckDate select item);
                        if (IsMissingTrans)
                        {
                            OUT_CheckSmartCardsFiltered = DataProvider.filterMissingTrans_OutCheckSmartCard(OUT_CheckSmartCardsFiltered, compareDbConn);
                        }
                        DuplicatedSmartID(IsCheckedBooleanProperty);
                        break;
                    }
                case "OUT_CheckForceOpen":
                    {
                        OUT_CheckForceOpensFiltered = new ObservableCollection<OUT_CheckForceOpen>(from item in OUT_CheckForceOpens where item.CheckDate >= startTime && item.CheckDate <= endTime && (shift == 0 || shift == item.ShiftID) orderby item.CheckDate select item);
                        if (IsMissingTrans)
                        {
                            OUT_CheckForceOpensFiltered = DataProvider.filterMissingTrans_OUT_CheckForceOpen(OUT_CheckForceOpensFiltered, compareDbConn);
                        }
                        DuplicatedSmartID(IsCheckedBooleanProperty);
                        break;
                    }
                case "OUT_CheckEtag":
                    {
                        OUT_CheckEtagsFiltered = new ObservableCollection<OUT_CheckEtag>(from item in OUT_CheckEtags where item.CheckDate >= startTime && item.CheckDate <= endTime && (shift == 0 || shift == item.ShiftID) orderby item.CheckDate select item);
                        if (IsMissingTrans)
                        {
                            OUT_CheckEtagsFiltered = DataProvider.filterMissingTrans_OUT_CheckEtag(OUT_CheckEtagsFiltered, compareDbConn);
                        }
                        DuplicatedSmartID(IsCheckedBooleanProperty);
                        break;
                    }
            }
        }

        // Change shift
        private void ChangeShift()
        {
            
            if(_shift != null)
            switch (Shift.Name)
            {
               
                case "All":
                    {
                        // 6:30:00 today -> 6:30:00 tomorrow
                        startTime = SelectedDate.Add(new TimeSpan(6, 30, 00));
                        endTime = startTime.AddDays(1).AddMinutes(Timeoff);
                        FilterTransByTime();
                        break;
                    }
                case "1":
                    {
                        // 6:30:00 to 11:29:59 
                        startTime = SelectedDate.Add(new TimeSpan(6, 30, 00));          
                        endTime = SelectedDate.Add(new TimeSpan(11, 29, 59)).AddMinutes(Timeoff);
                        FilterTransByTime(1);
                        break;
                    }
                case "2":
                    {
                        // 11:30:00 to 17:59:59
                        startTime = SelectedDate.Add(new TimeSpan(11, 30, 00));
                        endTime = SelectedDate.Add(new TimeSpan(17, 59, 59)).AddMinutes(Timeoff);
                        FilterTransByTime(2);
                        break;
                    }
                case "3":
                    {
                        // 18:00:00 today -> 6:29:59 tomorrow
                        startTime = SelectedDate.Add(new TimeSpan(18, 00, 00));
                        endTime = SelectedDate.AddDays(1).Add(new TimeSpan(6, 29, 59)).AddMinutes(Timeoff);
                        FilterTransByTime(3);
                        break;
                    }
            }
            CountLogRow();
        }

        // Bind datagrid by table 

        private void ChangeTable(string table)
        {
            switch (table)
            {
                case "IN_CheckSmartCard":
                    {
                        tICS = Visibility.Visible;
                        tCSC = Visibility.Hidden;
                        tCFO = Visibility.Hidden;
                        tCE = Visibility.Hidden;
                        tICFO = Visibility.Hidden;
                        tDup = Visibility.Visible;
                        break;
                    }
                case "IN_CheckForceOpen":
                    {
                        tICFO = Visibility.Visible;
                        tCFO = Visibility.Hidden;
                        tCSC = Visibility.Hidden;
                        tCE = Visibility.Hidden;
                        tDup = Visibility.Hidden;
                        tICS = Visibility.Hidden;
                        IsCheckedBooleanProperty = false;
                        break;
                    }
                case "OUT_CheckSmartCard":
                    {
                        tCSC = Visibility.Visible;
                        tCFO = Visibility.Hidden;
                        tCE = Visibility.Hidden;
                        tICS = Visibility.Hidden;
                        tICFO = Visibility.Hidden;
                        tDup = Visibility.Visible;
                        break;
                    }
                case "OUT_CheckForceOpen":
                    {
                        tCFO = Visibility.Visible;
                        tCSC = Visibility.Hidden;
                        tCE = Visibility.Hidden;
                        tDup = Visibility.Hidden;
                        tICS = Visibility.Hidden;
                        tICFO = Visibility.Hidden;
                        IsCheckedBooleanProperty = false;
                        break;
                    }
                case "OUT_CheckEtag":
                    {
                        tCE = Visibility.Visible;
                        tCSC = Visibility.Hidden;
                        tICS = Visibility.Hidden;
                        tICFO = Visibility.Hidden;
                        tDup = Visibility.Hidden;
                        IsCheckedBooleanProperty = false;
                        break;
                    }
            }
            CountLogRow();
            
        }

        private void CountLogRow()
        {
            switch(SelectedTable)
            {
                case "OUT_CheckSmartCard":
                    RowCount = OUT_CheckSmartCardsFiltered.Count;
                    break;
                case "OUT_CheckForceOpen":
                    RowCount = OUT_CheckForceOpensFiltered.Count;
                    break;
                case "OUT_CheckEtag":
                    RowCount = OUT_CheckEtagsFiltered.Count;
                    break;
                case "IN_CheckSmartCard":
                    RowCount = IN_CheckSmartCardsFiltered.Count;
                    break;
                case "IN_CheckForceOpen":
                    RowCount = IN_CheckForceOpensFiltered.Count;
                    break;
            }
        }
        // Load Config Form
       
        private void DuplicatedSmartID(bool IsCheckedBooleanProperty)
        {
            if(IsCheckedBooleanProperty)
            {
                switch(SelectedTable)
                {
                    case "OUT_CheckSmartCard":
                        var dup = OUT_CheckSmartCardsFiltered.GroupBy(x => x.SmartCardID).Where(g => g.Count() > 1).Select(x => x.Key);
                        var rs = OUT_CheckSmartCardsFiltered.Where(w => dup.Contains(w.SmartCardID));
                        OUT_CheckSmartCardsFiltered = new ObservableCollection<OUT_CheckSmartCard>(rs);
                        break;
                    case "IN_CheckSmartCard":
                        var dup2 = IN_CheckSmartCardsFiltered.GroupBy(x => x.SmartCardID).Where(g => g.Count() > 1).Select(x => x.Key);
                        var rs2 = IN_CheckSmartCardsFiltered.Where(w => dup2.Contains(w.SmartCardID));
                        IN_CheckSmartCardsFiltered = new ObservableCollection<IN_CheckSmartCard>(rs2);
                        break;
                }                                                                           
            }    
        }
        private void InsertLog()
        {
            ActionView view = new ActionView();

            if (view.ShowDialog() == true)
            {
                insertDbConn = DbHelper.GetIpConnectionString(IP, config.InsertDB.DatabaseName, config.InsertDB.DatabaseUser, config.InsertDB.DatabasePassword, config.InsertDB.DatabaseTimeout.ToString());
                DbHelper dbHelper = new DbHelper(insertDbConn);
                if(!dbHelper.CheckOpenConnection())
                {
                    MessageBox.Show("Connect db failed!");
                    return;
                }
                try
                {
                    using (Model1 db = new Model1(insertDbConn))
                    {
                        switch (SelectedTable)
                        {
                            case "IN_CheckSmartCard":
                                {
                                    if (IN_CheckSmartCardsFiltered.Count < 1)
                                        MessageBox.Show("Không có dữ liệu để thêm!");
                                    else
                                        db.IN_CheckSmartCard.AddRange(IN_CheckSmartCardsFiltered);
                                    break;
                                }
                            case "IN_CheckForceOpen":
                                {
                                    if (IN_CheckForceOpensFiltered.Count < 1)
                                        MessageBox.Show("Không có dữ liệu để thêm!");
                                    else
                                        db.IN_CheckForceOpen.AddRange(IN_CheckForceOpensFiltered);
                                    break;
                                }
                            case "OUT_CheckSmartCard":
                                {
                                    if (OUT_CheckSmartCardsFiltered.Count < 1)
                                        MessageBox.Show("Không có dữ liệu để thêm!");
                                    else
                                        db.OUT_CheckSmartCard.AddRange(OUT_CheckSmartCardsFiltered);
                                    break;
                                }
                            case "OUT_CheckForceOpen":
                                {
                                    if (OUT_CheckForceOpensFiltered.Count < 1)
                                        MessageBox.Show("Không có dữ liệu để thêm!");
                                    else
                                        db.OUT_CheckForceOpen.AddRange(OUT_CheckForceOpensFiltered);
                                    break;
                                }
                            case "OUT_CheckEtag":
                                {
                                    if (OUT_CheckEtagsFiltered.Count < 1)
                                        MessageBox.Show("Không có dữ liệu để thêm!");
                                    else
                                        db.OUT_CheckEtag.AddRange(OUT_CheckEtagsFiltered);
                                    break;
                                }
                        }
                        if (db.SaveChanges() > 0)
                        {
                            OUT_CheckSmartCardsFiltered.Clear();
                            OUT_CheckEtagsFiltered.Clear();
                            OUT_CheckForceOpensFiltered.Clear();
                            IN_CheckSmartCardsFiltered.Clear();
                            IN_CheckForceOpensFiltered.Clear();
                            CountLogRow();
                            MessageBox.Show("Thêm dữ liệu thành công!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thêm dữ liệu thất bại!, Lỗi: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Thêm dữ liệu không thành công, vui lòng thử lại!");
            }

        }
        
        #endregion

        #region Commands
        private ICommand _cmdInsertLog;
        public ICommand cmdInsertLog => _cmdInsertLog ?? (_cmdInsertLog = new RelayCommand(param => { InsertLog(); }, param => CanClick()));

        private ICommand _cmdReadLog;
        public ICommand cmdReadLog => _cmdReadLog ?? (_cmdReadLog = new RelayCommand(param => { ReadLogFile(); }, param => CanClick()));

        private ICommand _cmdCheck;
        public ICommand cmdCheck => _cmdCheck ?? (_cmdCheck = new RelayCommand(param => { Check(); }, param => CanClick()));

        private ICommand _cmdBrowse;
        public ICommand cmdBrowse => _cmdBrowse ?? (_cmdBrowse = new RelayCommand(param => { Browse(); }, param => CanBrowse()));

        private ICommand _checkDup;
        public ICommand checkDup => _checkDup ?? (_checkDup = new RelayCommand(param => { DuplicatedSmartID(IsCheckedBooleanProperty); }));
        #endregion
    }
}
