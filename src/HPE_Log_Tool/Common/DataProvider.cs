using HPE_Log_Tool.Models;
using ITD_Review_license__plates.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPE_Log_Tool.Common
{
    public class DataProvider
    {
        public static List<LS_Shift> GetShifts(bool isAll = false)
        {
            List<LS_Shift> shiftList = new List<LS_Shift>();
            try
            {
                AppConfig config = AppConfig.LoadConfig();
                string conn = DbHelper.GetConnectionString(config.CompareDB.DatabaseServer, config.CompareDB.DatabaseName, config.CompareDB.DatabaseUser, config.CompareDB.DatabasePassword, config.CompareDB.DatabaseTimeout.ToString());
                using (Model1 db = new Model1(conn))
                {
                    shiftList = db.LS_Shift.ToList();
                    if (isAll)
                    {
                        LS_Shift shiftall = new LS_Shift();
                        shiftall.ShiftID = 0;
                        shiftall.Name = "All";
                        shiftList.Insert(0, shiftall);
                    }
                }
            }
            catch (Exception)
            {

            }
            return shiftList;

        }
        public static ObservableCollection<OUT_CheckSmartCard> filterMissingTrans_OutCheckSmartCard(ObservableCollection<OUT_CheckSmartCard> list, string conn)
        {
            ObservableCollection<OUT_CheckSmartCard> result = new ObservableCollection<OUT_CheckSmartCard>();
            using(Model1 db = new Model1(conn))
            {
                foreach (OUT_CheckSmartCard trans in list)
                {
                    bool isMissing = db.OUT_CheckSmartCard.Any(r => trans.TransactionID == r.TransactionID);
                    if (!isMissing)
                    {
                        result.Add(trans);
                    }
                }
            }
            
            return result;
        }
        public static ObservableCollection<OUT_CheckForceOpen> filterMissingTrans_OUT_CheckForceOpen(ObservableCollection<OUT_CheckForceOpen> list, string conn)
        {
            ObservableCollection<OUT_CheckForceOpen> result = new ObservableCollection<OUT_CheckForceOpen>();
            using (Model1 db = new Model1(conn))
            {
                foreach (OUT_CheckForceOpen trans in list)
                {
                    bool isMissing = db.OUT_CheckForceOpen.Any(r => trans.TransactionID == r.TransactionID);
                    if (!isMissing)
                    {
                        result.Add(trans);
                    }
                }
            }
            return result;
        }
        public static ObservableCollection<OUT_CheckEtag> filterMissingTrans_OUT_CheckEtag(ObservableCollection<OUT_CheckEtag> list, string conn)
        {
            ObservableCollection<OUT_CheckEtag> result = new ObservableCollection<OUT_CheckEtag>();
            using (Model1 db = new Model1(conn))
            {
                foreach (OUT_CheckEtag trans in list)
                {
                    bool isMissing = db.OUT_CheckSmartCard.Any(r => trans.OutCheckEtagID == r.OutCheckSmartCardID);
                    if (!isMissing)
                    {
                        result.Add(trans);
                    }
                }
            }

            return result;
        }


    }
}
