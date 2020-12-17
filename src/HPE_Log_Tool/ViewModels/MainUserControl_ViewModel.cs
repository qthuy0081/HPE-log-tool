namespace HPE_Log_Tool.ViewModels
{
    using HPE_Log_Tool.Common;
    using HPE_Log_Tool.Models;
    using HPE_Log_Tool.Views;
    using ITD_Review_license__plates.Common;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Input;
    using MessageBox = System.Windows.MessageBox;

    /// <summary>
    /// Defines the <see cref="MainUserControl_ViewModel" />.
    /// </summary>
    public class MainUserControl_ViewModel : BaseViewModel
    {
        /// <summary>
        /// Defines the isOutCheckSmartCard.
        /// </summary>
        private const string isOutCheckSmartCard = "WriteLogPrepareCommitReceipt - OUT_CheckSmartCard";

        /// <summary>
        /// Defines the isOutCheckForceOpen.
        /// </summary>
        private const string isOutCheckForceOpen = "WriteLogPrepareCommitReceipt - OUT_CheckForceOpen";

        /// <summary>
        /// Defines the isOutCheckEtag.
        /// </summary>
        private const string isOutCheckEtag = "WriteLogPrepareCommitReceipt - OUT_CheckEtag";

        /// <summary>
        /// Defines the isInCheckSmartCard.
        /// </summary>
        private const string isInCheckSmartCard = "InsertData - IN_CheckSmartCard";

        /// <summary>
        /// Defines the isInCheckForceOpen.
        /// </summary>
        private const string isInCheckForceOpen = "InsertData - IN_CheckForceOpen";

        /// <summary>
        /// Defines the fileNameIN.
        /// </summary>
        private const string fileNameIN = "\\LogFolder\\InsertTransaction_Log_";

        /// <summary>
        /// Defines the fileNameOUT.
        /// </summary>
        private const string fileNameOUT = "\\Logs\\TraceLog_";

        /// <summary>
        /// Defines the fileExt.
        /// </summary>
        private const string fileExt = ".txt";

        /// <summary>
        /// Defines the startTime.
        /// </summary>
        private DateTime startTime;

        /// <summary>
        /// Defines the endTime.
        /// </summary>
        private DateTime endTime;

        /// <summary>
        /// Defines the config.
        /// </summary>
        private AppConfig config;

        /// <summary>
        /// Defines the insertDbConn.
        /// </summary>
        internal string insertDbConn = "";

        /// <summary>
        /// Defines the compareDbConn.
        /// </summary>
        internal string compareDbConn = "";

        /// <summary>
        /// Defines the _ip.
        /// </summary>
        private string _ip;

        /// <summary>
        /// Gets or sets the IP.
        /// </summary>
        public string IP
        {
            get => _ip;
            set
            {
                if (_ip != value)
                {
                    _ip = value;
                    Path = $"\\\\{IP}{AppConfig.path}";
                    OnPropertyChanged();

                }
            }
        }

        /// <summary>
        /// Defines the _path.
        /// </summary>
        private string _path;

        /// <summary>
        /// Gets or sets the Path.
        /// </summary>
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

        /// <summary>
        /// Defines the _rowCount.
        /// </summary>
        private int _rowCount = 0;

        /// <summary>
        /// Gets or sets the RowCount.
        /// </summary>
        public int RowCount
        {
            get => _rowCount;
            set
            {
                if (_rowCount != value)
                {
                    _rowCount = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Defines the _insertButtonContent.
        /// </summary>
        private string _insertButtonContent = "Insert";

        /// <summary>
        /// Gets or sets the InsertButtonContent.
        /// </summary>
        public string InsertButtonContent
        {
            get => _insertButtonContent;
            set
            {
                if (_insertButtonContent != value)
                {
                    _insertButtonContent = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Defines the _timeOff.
        /// </summary>
        private int _timeOff = 30;

        /// <summary>
        /// Gets or sets the Timeoff.
        /// </summary>
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

        /// <summary>
        /// Defines the _isMissingTrans.
        /// </summary>
        private bool _isMissingTrans;

        /// <summary>
        /// Gets or sets a value indicating whether IsMissingTrans.
        /// </summary>
        public bool IsMissingTrans
        {
            get => _isMissingTrans;
            set
            {
                if (_isMissingTrans != value)
                {
                    _isMissingTrans = value;
                    OnPropertyChanged();
                    ChangeShift();

                }
            }
        }

        /// <summary>
        /// Defines the _SelectedItem.
        /// </summary>
        private MainUserControl_ViewModel _SelectedItem;

        /// <summary>
        /// Gets or sets the SelectedItem.
        /// </summary>
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

        /// <summary>
        /// Defines the _tCSC.
        /// </summary>
        private Visibility _tCSC = Visibility.Visible;

        /// <summary>
        /// Gets or sets the tCSC.
        /// </summary>
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

        /// <summary>
        /// Defines the _tCFO.
        /// </summary>
        private Visibility _tCFO;

        /// <summary>
        /// Gets or sets the tCFO.
        /// </summary>
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

        /// <summary>
        /// Defines the _tCE.
        /// </summary>
        private Visibility _tCE;

        /// <summary>
        /// Gets or sets the tCE.
        /// </summary>
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

        /// <summary>
        /// Defines the _tICS.
        /// </summary>
        private Visibility _tICS;

        /// <summary>
        /// Gets or sets the tICS.
        /// </summary>
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

        /// <summary>
        /// Defines the _tICFO.
        /// </summary>
        private Visibility _tICFO;

        /// <summary>
        /// Gets or sets the tICFO.
        /// </summary>
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

        /// <summary>
        /// Defines the _tDup.
        /// </summary>
        private Visibility _tDup;

        /// <summary>
        /// Gets or sets the tDup.
        /// </summary>
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

        /// <summary>
        /// Defines the _inCheckSmartCard.
        /// </summary>
        private ObservableCollection<IN_CheckSmartCard> _inCheckSmartCard;

        /// <summary>
        /// Gets or sets the IN_CheckSmartCards.
        /// </summary>
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

        /// <summary>
        /// Defines the _inCheckSmartCardFiltered.
        /// </summary>
        private ObservableCollection<IN_CheckSmartCard> _inCheckSmartCardFiltered;

        /// <summary>
        /// Gets or sets the IN_CheckSmartCardsFiltered.
        /// </summary>
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

        /// <summary>
        /// Defines the _inCheckForceOpen.
        /// </summary>
        private ObservableCollection<IN_CheckForceOpen> _inCheckForceOpen;

        /// <summary>
        /// Gets or sets the IN_CheckForceOpens.
        /// </summary>
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

        /// <summary>
        /// Defines the _inCheckForceOpenFiltered.
        /// </summary>
        private ObservableCollection<IN_CheckForceOpen> _inCheckForceOpenFiltered;

        /// <summary>
        /// Gets or sets the IN_CheckForceOpensFiltered.
        /// </summary>
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

        /// <summary>
        /// Defines the _outCheckSmartCard.
        /// </summary>
        private ObservableCollection<OUT_CheckSmartCard> _outCheckSmartCard;

        /// <summary>
        /// Gets or sets the OUT_CheckSmartCards.
        /// </summary>
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

        /// <summary>
        /// Defines the _outCheckForceOpen.
        /// </summary>
        private ObservableCollection<OUT_CheckForceOpen> _outCheckForceOpen;

        /// <summary>
        /// Gets or sets the OUT_CheckForceOpens.
        /// </summary>
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

        /// <summary>
        /// Defines the _outCheckEtag.
        /// </summary>
        private ObservableCollection<OUT_CheckEtag> _outCheckEtag;

        /// <summary>
        /// Gets or sets the OUT_CheckEtags.
        /// </summary>
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

        /// <summary>
        /// Defines the _outCheckSmartCardFiltered.
        /// </summary>
        private ObservableCollection<OUT_CheckSmartCard> _outCheckSmartCardFiltered;

        /// <summary>
        /// Gets or sets the OUT_CheckSmartCardsFiltered.
        /// </summary>
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

        /// <summary>
        /// Defines the _outCheckForceOpenFiltered.
        /// </summary>
        private ObservableCollection<OUT_CheckForceOpen> _outCheckForceOpenFiltered;

        /// <summary>
        /// Gets or sets the OUT_CheckForceOpensFiltered.
        /// </summary>
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

        /// <summary>
        /// Defines the _outCheckEtagFiltered.
        /// </summary>
        private ObservableCollection<OUT_CheckEtag> _outCheckEtagFiltered;

        /// <summary>
        /// Gets or sets the OUT_CheckEtagsFiltered.
        /// </summary>
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

        /// <summary>
        /// Defines the _tableList.
        /// </summary>
        private ObservableCollection<string> _tableList;

        /// <summary>
        /// Gets or sets the tableList.
        /// </summary>
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

        /// <summary>
        /// Defines the _shiftList.
        /// </summary>
        private List<LS_Shift> _shiftList;

        /// <summary>
        /// Gets or sets the shiftList.
        /// </summary>
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

        /// <summary>
        /// Defines the _shift.
        /// </summary>
        private LS_Shift _shift;

        /// <summary>
        /// Gets or sets the Shift.
        /// </summary>
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

        /// <summary>
        /// Defines the _filePaths.
        /// </summary>
        private List<string> _filePaths;

        /// <summary>
        /// Gets or sets the filePaths.
        /// </summary>
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

        /// <summary>
        /// Defines the _selectedTable.
        /// </summary>
        private string _selectedTable;

        /// <summary>
        /// Gets or sets the SelectedTable.
        /// </summary>
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

        /// <summary>
        /// Defines the _selectedShift.
        /// </summary>
        private string _selectedShift;

        /// <summary>
        /// Gets or sets the SelectedShift.
        /// </summary>
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

        /// <summary>
        /// Defines the _selectedDate.
        /// </summary>
        private DateTime _selectedDate;

        /// <summary>
        /// Gets or sets the SelectedDate.
        /// </summary>
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

        /// <summary>
        /// Defines the _IsCheckedBooleanProperty.
        /// </summary>
        private bool _IsCheckedBooleanProperty;

        /// <summary>
        /// Gets or sets a value indicating whether IsCheckedBooleanProperty.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="MainUserControl_ViewModel"/> class.
        /// </summary>
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
            Path = AppConfig.path;
        }

        /// <summary>
        /// The CanClick.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool CanClick()
        {
            return InsertButtonContent == "Insert";
        }

        /// <summary>
        /// The Check.
        /// </summary>
        private void Check()
        {
            Utility.checkExist(Path);
        }

        /// <summary>
        /// The CanBrowse.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool CanBrowse()
        {
            return true;
        }

        /// <summary>
        /// The Browse.
        /// </summary>
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

                
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// The ClearData.
        /// </summary>
        private void ClearData()
        {
            OUT_CheckSmartCards.Clear();
            OUT_CheckForceOpens.Clear();
            OUT_CheckEtags.Clear();
            filePaths.Clear();
        }

        /// <summary>
        /// The toDateStamp.
        /// </summary>
        /// <param name="Date">The Date<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string toDateStamp(DateTime Date)
        {
            string date = Date.ToString("yyyyMMdd");
            return date;
        }

        /// <summary>
        /// The ReadLogFile.
        /// </summary>
        private void ReadLogFile()
        {
            // Giờ có sẵn 1 list path của các fileLog đó rồi nè
            // Xong từ ngày cái mình lấy ra cái đuôi rồi lấy cái file tương ứng theo ngày đó (Ex: 20/10/2020 -> InsertTransaction_Log_20201020)
            string currentOutDate = Path + fileNameOUT + SelectedDate.ToString("yyyy-MM-dd") + ".log";
            string tomorrowOutDate = Path + fileNameOUT + SelectedDate.AddDays(1).ToString("yyyy-MM-dd") + ".log";
            string currentInDate = Path + fileNameIN + toDateStamp(SelectedDate) + fileExt;
            string tomorrowInDate = Path + fileNameIN + toDateStamp(SelectedDate.AddDays(1)) + fileExt;
            string[] lines;
            int startIndex;
            int endIndex;
            string json;
            // Lấy được chuỗi tương ứng rồi       
            // Trước khi lọc thì phải clear cái grid view 
            ClearData();
            filePaths.Add(currentInDate);
            filePaths.Add(tomorrowInDate);
            filePaths.Add(currentOutDate);
            filePaths.Add(tomorrowOutDate);

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
                        return;
                    }
                }

            }
            FirstLoad();
            MessageBox.Show("Load dữ liệu thành công!");
        }

        /// <summary>
        /// The FirstLoad.
        /// </summary>
        private void FirstLoad()
        {
            startTime = SelectedDate.Add(new TimeSpan(6, 30, 00));
            endTime = SelectedDate.AddDays(1).Add(new TimeSpan(6, 29, 59)).AddMinutes(Timeoff);
            OUT_CheckSmartCardsFiltered = new ObservableCollection<OUT_CheckSmartCard>(from item in OUT_CheckSmartCards where item.CheckDate >= startTime && item.CheckDate <= endTime orderby item.CheckDate select item);
            OUT_CheckForceOpensFiltered = new ObservableCollection<OUT_CheckForceOpen>(from item in OUT_CheckForceOpens where item.CheckDate >= startTime && item.CheckDate <= endTime orderby item.CheckDate select item);
            OUT_CheckEtagsFiltered = new ObservableCollection<OUT_CheckEtag>(from item in OUT_CheckEtags where item.CheckDate >= startTime && item.CheckDate <= endTime orderby item.CheckDate select item);
            IN_CheckSmartCardsFiltered = new ObservableCollection<IN_CheckSmartCard>(from item in IN_CheckSmartCards where item.CheckDate >= startTime && item.CheckDate <= endTime orderby item.CheckDate select item);
            IN_CheckForceOpensFiltered = new ObservableCollection<IN_CheckForceOpen>(from item in IN_CheckForceOpens where item.CheckDate >= startTime && item.CheckDate <= endTime orderby item.CheckDate select item);
            CountLogRow();
        }

        /// <summary>
        /// The FilterTransByTime.
        /// </summary>
        /// <param name="shift">The shift<see cref="int"/>.</param>
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
                            IN_CheckSmartCardsFiltered = DataProvider.filterMissingTrans_InCheckSmartCard(IN_CheckSmartCardsFiltered, compareDbConn, startTime, endTime);
                        }
                        DuplicatedSmartID(IsCheckedBooleanProperty);
                        break;
                    }
                case "IN_CheckForceOpen":
                    {
                        IN_CheckForceOpensFiltered = new ObservableCollection<IN_CheckForceOpen>(from item in IN_CheckForceOpens where item.CheckDate >= startTime && item.CheckDate <= endTime && (shift == 0 || shift == item.ShiftID) orderby item.CheckDate select item);
                        if (IsMissingTrans)
                        {
                            IN_CheckForceOpensFiltered = DataProvider.filterMissingTrans_InCheckForceOpen(IN_CheckForceOpensFiltered, compareDbConn, startTime, endTime);
                        }
                        DuplicatedSmartID(IsCheckedBooleanProperty);
                        break;
                    }
                case "OUT_CheckSmartCard":
                    {
                        OUT_CheckSmartCardsFiltered = new ObservableCollection<OUT_CheckSmartCard>(from item in OUT_CheckSmartCards where item.CheckDate >= startTime && item.CheckDate <= endTime && (shift == 0 || shift == item.ShiftID) orderby item.CheckDate select item);
                        if (IsMissingTrans)
                        {
                            OUT_CheckSmartCardsFiltered = DataProvider.filterMissingTrans_OutCheckSmartCard(OUT_CheckSmartCardsFiltered, compareDbConn, startTime, endTime);
                        }
                        DuplicatedSmartID(IsCheckedBooleanProperty);
                        break;
                    }
                case "OUT_CheckForceOpen":
                    {
                        OUT_CheckForceOpensFiltered = new ObservableCollection<OUT_CheckForceOpen>(from item in OUT_CheckForceOpens where item.CheckDate >= startTime && item.CheckDate <= endTime && (shift == 0 || shift == item.ShiftID) orderby item.CheckDate select item);
                        if (IsMissingTrans)
                        {
                            OUT_CheckForceOpensFiltered = DataProvider.filterMissingTrans_OUT_CheckForceOpen(OUT_CheckForceOpensFiltered, compareDbConn, startTime, endTime);
                        }
                        DuplicatedSmartID(IsCheckedBooleanProperty);
                        break;
                    }
                case "OUT_CheckEtag":
                    {
                        OUT_CheckEtagsFiltered = new ObservableCollection<OUT_CheckEtag>(from item in OUT_CheckEtags where item.CheckDate >= startTime && item.CheckDate <= endTime && (shift == 0 || shift == item.ShiftID) orderby item.CheckDate select item);
                        if (IsMissingTrans)
                        {

                            OUT_CheckEtagsFiltered = DataProvider.filterMissingTrans_OUT_CheckEtag(OUT_CheckEtagsFiltered, compareDbConn, startTime, endTime);
                        }
                        DuplicatedSmartID(IsCheckedBooleanProperty);
                        break;
                    }
            }
        }

        /// <summary>
        /// The ChangeShift.
        /// </summary>
        private void ChangeShift()
        {

            if (_shift != null)
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

        /// <summary>
        /// The ChangeTable.
        /// </summary>
        /// <param name="table">The table<see cref="string"/>.</param>
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

        /// <summary>
        /// The CountLogRow.
        /// </summary>
        private void CountLogRow()
        {
            switch (SelectedTable)
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

        /// <summary>
        /// The DuplicatedSmartID.
        /// </summary>
        /// <param name="IsCheckedBooleanProperty">The IsCheckedBooleanProperty<see cref="bool"/>.</param>
        private void DuplicatedSmartID(bool IsCheckedBooleanProperty)
        {
            if (IsCheckedBooleanProperty)
            {
                switch (SelectedTable)
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

        /// <summary>
        /// The InsertLog.
        /// </summary>
        private async void InsertLog()
        {
            ActionView view = new ActionView();

            if (view.ShowDialog() == true)
            {
                insertDbConn = DbHelper.GetIpConnectionString(IP, config.InsertDB.DatabaseName, config.InsertDB.DatabaseUser, config.InsertDB.DatabasePassword, config.InsertDB.DatabaseTimeout.ToString());
                DbHelper dbHelper = new DbHelper(insertDbConn);
                if (!dbHelper.CheckOpenConnection())
                {
                    MessageBox.Show("Connect db failed!");
                    return;
                }
                InsertButtonContent = "Inserting...! Please wait";
                switch (SelectedTable)
                {
                    case "IN_CheckSmartCard":
                        {
                            if (IN_CheckSmartCardsFiltered.Count < 1)
                                MessageBox.Show("Không có dữ liệu để thêm!");
                            else
                                IN_CheckSmartCardsFiltered = await DataProvider.insert_IN_CheckSmartCard(IN_CheckSmartCards, insertDbConn);
                            break;
                        }
                    case "IN_CheckForceOpen":
                        {
                            if (IN_CheckForceOpensFiltered.Count < 1)
                                MessageBox.Show("Không có dữ liệu để thêm!");
                            else
                                IN_CheckForceOpensFiltered = await DataProvider.insert_IN_CheckForceOpen(IN_CheckForceOpensFiltered, insertDbConn);
                            break;
                        }
                    case "OUT_CheckSmartCard":
                        {
                            if (OUT_CheckSmartCardsFiltered.Count < 1)
                                MessageBox.Show("Không có dữ liệu để thêm!");
                            else
                                OUT_CheckSmartCardsFiltered = await DataProvider.insert_OUT_CheckSmartCard(OUT_CheckSmartCardsFiltered, insertDbConn);
                            break;
                        }
                    case "OUT_CheckForceOpen":
                        {
                            if (OUT_CheckForceOpensFiltered.Count < 1)
                                MessageBox.Show("Không có dữ liệu để thêm!");
                            else
                                OUT_CheckForceOpensFiltered = await DataProvider.insert_OUT_CheckForceOpen(OUT_CheckForceOpensFiltered, insertDbConn);
                            break;
                        }
                    case "OUT_CheckEtag":
                        {
                            if (OUT_CheckEtagsFiltered.Count < 1)
                                MessageBox.Show("Không có dữ liệu để thêm!");
                            else
                            {
                                OUT_CheckEtagsFiltered = await DataProvider.insert_OUT_CheckEtag(OUT_CheckEtagsFiltered, insertDbConn);
                            }
                            break;
                        }
                }
                InsertButtonContent = "Insert";
            }
            else
            {
                MessageBox.Show("Thêm dữ liệu không thành công, vui lòng thử lại!");
            }
            CountLogRow();
        }

        /// <summary>
        /// Defines the _cmdInsertLog.
        /// </summary>
        private ICommand _cmdInsertLog;

        /// <summary>
        /// Gets the cmdInsertLog.
        /// </summary>
        public ICommand cmdInsertLog => _cmdInsertLog ?? (_cmdInsertLog = new RelayCommand(param => { InsertLog(); }, param => CanClick()));

        /// <summary>
        /// Defines the _cmdReadLog.
        /// </summary>
        private ICommand _cmdReadLog;

        /// <summary>
        /// Gets the cmdReadLog.
        /// </summary>
        public ICommand cmdReadLog => _cmdReadLog ?? (_cmdReadLog = new RelayCommand(param => { ReadLogFile(); }, param => CanClick()));

        /// <summary>
        /// Defines the _cmdCheck.
        /// </summary>
        private ICommand _cmdCheck;

        /// <summary>
        /// Gets the cmdCheck.
        /// </summary>
        public ICommand cmdCheck => _cmdCheck ?? (_cmdCheck = new RelayCommand(param => { Check(); }, param => CanClick()));

        /// <summary>
        /// Defines the _cmdBrowse.
        /// </summary>
        private ICommand _cmdBrowse;

        /// <summary>
        /// Gets the cmdBrowse.
        /// </summary>
        public ICommand cmdBrowse => _cmdBrowse ?? (_cmdBrowse = new RelayCommand(param => { Browse(); }, param => CanBrowse()));

        /// <summary>
        /// Defines the _checkDup.
        /// </summary>
        private ICommand _checkDup;

        /// <summary>
        /// Gets the checkDup.
        /// </summary>
        public ICommand checkDup => _checkDup ?? (_checkDup = new RelayCommand(param => { DuplicatedSmartID(IsCheckedBooleanProperty); }));
    }
}
