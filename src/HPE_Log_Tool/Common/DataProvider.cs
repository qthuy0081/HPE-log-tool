namespace HPE_Log_Tool.Common
{
    using HPE_Log_Tool.Models;
    using ITD_Review_license__plates.Common;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Defines the <see cref="DataProvider" />.
    /// </summary>
    public class DataProvider
    {
        /// <summary>
        /// The GetShifts.
        /// </summary>
        /// <param name="isAll">The isAll<see cref="bool"/>.</param>
        /// <returns>The <see cref="List{LS_Shift}"/>.</returns>
        public static List<LS_Shift> GetShifts(bool isAll = false)
        {
            List<LS_Shift> shiftList = new List<LS_Shift>();
            try
            {
                AppConfig config = AppConfig.LoadConfig();
                string conn = DbHelper.GetConnectionString(config.CompareDB.DatabaseServer, config.CompareDB.DatabaseName, config.CompareDB.DatabaseUser, config.CompareDB.DatabasePassword, config.CompareDB.DatabaseTimeout.ToString());
                DbHelper dbHelper = new DbHelper(conn);
                if (dbHelper.CheckOpenConnection())
                {
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
            }
            catch (Exception e)
            {
                LogHelper.Error(e.Message);
            }
            return shiftList;
        }

        /// <summary>
        /// The filterMissingTrans_OutCheckSmartCard.
        /// </summary>
        /// <param name="list">The list<see cref="ObservableCollection{OUT_CheckSmartCard}"/>.</param>
        /// <param name="conn">The conn<see cref="string"/>.</param>
        /// <param name="startDate">The startDate<see cref="DateTime"/>.</param>
        /// <param name="endDate">The endDate<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="ObservableCollection{OUT_CheckSmartCard}"/>.</returns>
        public static ObservableCollection<OUT_CheckSmartCard> filterMissingTrans_OutCheckSmartCard(ObservableCollection<OUT_CheckSmartCard> list, string conn, DateTime startDate, DateTime endDate)
        {

            ObservableCollection<OUT_CheckSmartCard> result = new ObservableCollection<OUT_CheckSmartCard>();
            try
            {
                using (Model1 db = new Model1(conn))
                {
                    string table = getTableName("OUT_CheckSmartCard", startDate);
                    List<OUT_CheckSmartCard> mlist = db.Database.SqlQuery<OUT_CheckSmartCard>(String.Format("Select * from {0} where CheckDate >='{1}' and CheckDate <= '{2}'", table, startDate, endDate)).ToList();

                    foreach (OUT_CheckSmartCard trans in list)
                    {
                        if (!mlist.Any(t => t.TransactionID.Equals(trans.TransactionID)))
                        {
                            result.Add(trans);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.Error(e.Message);
            }


            return result;
        }

        /// <summary>
        /// The filterMissingTrans_OUT_CheckForceOpen.
        /// </summary>
        /// <param name="list">The list<see cref="ObservableCollection{OUT_CheckForceOpen}"/>.</param>
        /// <param name="conn">The conn<see cref="string"/>.</param>
        /// <param name="startDate">The startDate<see cref="DateTime"/>.</param>
        /// <param name="endDate">The endDate<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="ObservableCollection{OUT_CheckForceOpen}"/>.</returns>
        public static ObservableCollection<OUT_CheckForceOpen> filterMissingTrans_OUT_CheckForceOpen(ObservableCollection<OUT_CheckForceOpen> list, string conn, DateTime startDate, DateTime endDate)
        {
            ObservableCollection<OUT_CheckForceOpen> result = new ObservableCollection<OUT_CheckForceOpen>();
            try
            {
                using (Model1 db = new Model1(conn))
                {
                    string table = getTableName("OUT_CheckForceOpen", startDate);
                    List<OUT_CheckForceOpen> mlist = db.Database.SqlQuery<OUT_CheckForceOpen>(String.Format("Select * from {0} where CheckDate >='{1}' and CheckDate <= '{2}'", table, startDate, endDate)).ToList();
                    foreach (OUT_CheckForceOpen trans in list)
                    {
                        if (!mlist.Any(r => trans.OutCheckForceOpenID.Equals(r.OutCheckForceOpenID)))
                        {
                            result.Add(trans);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
            }

            return result;
        }

        /// <summary>
        /// The filterMissingTrans_OUT_CheckEtag.
        /// </summary>
        /// <param name="list">The list<see cref="ObservableCollection{OUT_CheckEtag}"/>.</param>
        /// <param name="conn">The conn<see cref="string"/>.</param>
        /// <param name="startDate">The startDate<see cref="DateTime"/>.</param>
        /// <param name="endDate">The endDate<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="ObservableCollection{OUT_CheckEtag}"/>.</returns>
        public static ObservableCollection<OUT_CheckEtag> filterMissingTrans_OUT_CheckEtag(ObservableCollection<OUT_CheckEtag> list, string conn, DateTime startDate, DateTime endDate)
        {
            ObservableCollection<OUT_CheckEtag> result = new ObservableCollection<OUT_CheckEtag>();
            try
            {
                using (Model1 db = new Model1(conn))
                {
                    try
                    {
                        string table = getTableName("OUT_CheckEtag", startDate);
                        List<OUT_CheckEtag> mlist = db.Database.SqlQuery<OUT_CheckEtag>(String.Format("Select * from {0} where CheckDate >='{1}' and CheckDate <= '{2}'", table, startDate, endDate)).ToList();
                        foreach (OUT_CheckEtag trans in list)
                        {
                            if (!mlist.Any(t => t.OutCheckEtagID.Equals(trans.OutCheckEtagID)))
                            {
                                result.Add(trans);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        LogHelper.Error(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.Error(e.Message);
            }


            return result;
        }

        /// <summary>
        /// The filterMissingTrans_InCheckSmartCard.
        /// </summary>
        /// <param name="list">The list<see cref="ObservableCollection{IN_CheckSmartCard}"/>.</param>
        /// <param name="conn">The conn<see cref="string"/>.</param>
        /// <param name="startDate">The startDate<see cref="DateTime"/>.</param>
        /// <param name="endDate">The endDate<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="ObservableCollection{IN_CheckSmartCard}"/>.</returns>
        public static ObservableCollection<IN_CheckSmartCard> filterMissingTrans_InCheckSmartCard(ObservableCollection<IN_CheckSmartCard> list, string conn, DateTime startDate, DateTime endDate)
        {
            ObservableCollection<IN_CheckSmartCard> result = new ObservableCollection<IN_CheckSmartCard>();
            try
            {
                using (Model1 db = new Model1(conn))
                {
                    string table = getTableName("IN_CheckSmartCard", startDate);
                    List<IN_CheckSmartCard> mlist = db.Database.SqlQuery<IN_CheckSmartCard>(String.Format("Select * from {0} where CheckDate >='{1}' and CheckDate <= '{2}'", table, startDate, endDate)).ToList();
                    foreach (IN_CheckSmartCard trans in list)
                    {

                        if (!mlist.Any(r => trans.InCheckSmartCardID.Equals(r.InCheckSmartCardID)))
                        {
                            result.Add(trans);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.Error(e.Message);
            }


            return result;
        }

        /// <summary>
        /// The filterMissingTrans_InCheckForceOpen.
        /// </summary>
        /// <param name="list">The list<see cref="ObservableCollection{IN_CheckForceOpen}"/>.</param>
        /// <param name="conn">The conn<see cref="string"/>.</param>
        /// <param name="startDate">The startDate<see cref="DateTime"/>.</param>
        /// <param name="endDate">The endDate<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="ObservableCollection{IN_CheckForceOpen}"/>.</returns>
        public static ObservableCollection<IN_CheckForceOpen> filterMissingTrans_InCheckForceOpen(ObservableCollection<IN_CheckForceOpen> list, string conn, DateTime startDate, DateTime endDate)
        {
            ObservableCollection<IN_CheckForceOpen> result = new ObservableCollection<IN_CheckForceOpen>();
            try
            {
                using (Model1 db = new Model1(conn))
                {
                    string table = getTableName("IN_CheckForceOpen", startDate);
                    List<IN_CheckForceOpen> mlist = db.Database.SqlQuery<IN_CheckForceOpen>(String.Format("Select * from {0} where CheckDate >='{1}' and CheckDate <= '{2}'", table, startDate, endDate)).ToList();
                    foreach (IN_CheckForceOpen trans in list)
                    {

                        if (!mlist.Any(r => trans.InCheckForceOpenID.Equals(r.InCheckForceOpenID)))
                        {
                            result.Add(trans);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.Error(e.Message);
            }
            return result;
        }

        /// <summary>
        /// The insertTransIntoDb.
        /// </summary>
        /// <param name="list">The list<see cref="ObservableCollection{OUT_CheckEtag}"/>.</param>
        /// <param name="conn">The conn<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public static Task<ObservableCollection<OUT_CheckEtag>> insert_OUT_CheckEtag(ObservableCollection<OUT_CheckEtag> list, string conn)
        {
            ObservableCollection<OUT_CheckEtag> transRemain = new ObservableCollection<OUT_CheckEtag>();
            return Task.Run(() =>
            {
                try
                {

                    foreach (OUT_CheckEtag tran in list)
                    {
                        using (Model1 db = new Model1(conn))
                        {
                            db.OUT_CheckEtag.Add(tran);
                            string json = JsonConvert.SerializeObject(tran);
                            try
                            {
                                db.SaveChanges();
                                LogHelper.Info(String.Format("InsertData - OUT_CheckEtag - Status: 1 - {0}", json));
                            }
                            catch (Exception e)
                            {
                                transRemain.Add(tran);
                                LogHelper.Error(String.Format("InsertData - OUT_CheckEtag - Status: 0 - {0} - Error: {1}", json, e.Message));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error(e.Message);
                }
                MessageBox.Show("Done!");
                return transRemain;
            }
            );
        }

        /// <summary>
        /// The insert_OUT_CheckSmartCard.
        /// </summary>
        /// <param name="list">The list<see cref="ObservableCollection{OUT_CheckSmartCard}"/>.</param>
        /// <param name="conn">The conn<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{ObservableCollection{OUT_CheckSmartCard}}"/>.</returns>
        public static Task<ObservableCollection<OUT_CheckSmartCard>> insert_OUT_CheckSmartCard(ObservableCollection<OUT_CheckSmartCard> list, string conn)
        {
            ObservableCollection<OUT_CheckSmartCard> transRemain = new ObservableCollection<OUT_CheckSmartCard>();
            return Task.Run(() =>
            {
                try
                {
                    foreach (OUT_CheckSmartCard tran in list)
                    {
                        using (Model1 db = new Model1(conn))
                        {
                            db.OUT_CheckSmartCard.Add(tran);
                            string json = JsonConvert.SerializeObject(tran);
                            try
                            {
                                db.SaveChanges();
                                LogHelper.Info(String.Format("InsertData - OUT_CheckSmartCard - Status: 1 - {0}", json));
                            }
                            catch (Exception e)
                            {
                                transRemain.Add(tran);

                                LogHelper.Error(String.Format("InsertData - OUT_CheckSmartCard - Status: 0 - {0} - Error: {1}", json, e));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error(e.Message);
                }
                MessageBox.Show("Done!");
                return transRemain;
            }
            );
        }
        public static Task<ObservableCollection<IN_CheckForceOpen>> insert_IN_CheckForceOpen(ObservableCollection<IN_CheckForceOpen> list, string conn)
        {
            ObservableCollection<IN_CheckForceOpen> transRemain = new ObservableCollection<IN_CheckForceOpen>();
            return Task.Run(() =>
            {
                try
                {
                    foreach (IN_CheckForceOpen tran in list)
                    {
                        using (Model1 db = new Model1(conn))
                        {
                            db.IN_CheckForceOpen.Add(tran);
                            string json = JsonConvert.SerializeObject(tran);
                            try
                            {
                                db.SaveChanges();
                                LogHelper.Info(String.Format("InsertData - IN_CheckForceOpen - Status: 1 - {0}", json));
                            }
                            catch (Exception e)
                            {
                                transRemain.Add(tran);

                                LogHelper.Error(String.Format("InsertData - IN_CheckForceOpen - Status: 0 - {0} - Error: {1}", json, e.Message));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error(e.Message);
                }
                MessageBox.Show("Done!");
                return transRemain;
            }
            );
        }
        public static Task<ObservableCollection<IN_CheckSmartCard>> insert_IN_CheckSmartCard(ObservableCollection<IN_CheckSmartCard> list, string conn)
        {
            ObservableCollection<IN_CheckSmartCard> transRemain = new ObservableCollection<IN_CheckSmartCard>();
            return Task.Run(() =>
            {
                try
                {
                    foreach (IN_CheckSmartCard tran in list)
                    {
                        using (Model1 db = new Model1(conn))
                        {
                            db.IN_CheckSmartCard.Add(tran);
                            string json = JsonConvert.SerializeObject(tran);
                            try
                            {
                                db.SaveChanges();
                                LogHelper.Info(String.Format("InsertData - IN_CheckSmartCard - Status: 1 - {0}", json));
                            }
                            catch (Exception e)
                            {
                                transRemain.Add(tran);

                                LogHelper.Error(String.Format("InsertData - IN_CheckSmartCard - Status: 0 - {0} - Error: {1}", json, e.Message));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error(e.Message);
                }
                MessageBox.Show("Done!");
                return transRemain;
            }
            );
        }

        public static Task<ObservableCollection<OUT_CheckForceOpen>> insert_OUT_CheckForceOpen(ObservableCollection<OUT_CheckForceOpen> list, string conn)
        {
            ObservableCollection<OUT_CheckForceOpen> transRemain = new ObservableCollection<OUT_CheckForceOpen>();
            return Task.Run(() =>
            {
                try
                {
                    foreach (OUT_CheckForceOpen tran in list)
                    {
                        using (Model1 db = new Model1(conn))
                        {
                            db.OUT_CheckForceOpen.Add(tran);
                            string json = JsonConvert.SerializeObject(tran);
                            try
                            {
                                db.SaveChanges();
                                LogHelper.Info(String.Format("InsertData - OUT_CheckForceOpen - Status: 1 - {0}", json));
                            }
                            catch (Exception e)
                            {
                                transRemain.Add(tran);

                                LogHelper.Error(String.Format("InsertData - OUT_CheckForceOpen - Status: 0 - {0} - Error: {1}", json, e.Message));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error(e.Message);
                }
                MessageBox.Show("Done!");
                return transRemain;
            }
            );
        }

        /// <summary>
        /// The insert_OUT_CheckSmartCard2.
        /// </summary>
        /// <param name="list">The list<see cref="ObservableCollection{OUT_CheckSmartCard}"/>.</param>
        /// <param name="conn">The conn<see cref="string"/>.</param>
        /// <returns>The <see cref="ObservableCollection{OUT_CheckSmartCard}"/>.</returns>


        /// <summary>
        /// The getTableName.
        /// </summary>
        /// <param name="table">The table<see cref="string"/>.</param>
        /// <param name="date">The date<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string getTableName(string table, DateTime date)
        {
            string year = date.Year.ToString();
            string month = date.Month.ToString();
            return String.Format("{0}@{1}{2}", table, year, month);
        }
    }
}
