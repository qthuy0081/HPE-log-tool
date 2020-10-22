using System;

using System.Data;
using System.Data.SqlClient;


namespace ITD_Review_license__plates.Common
{
    public class DbHelper
    {
        #region Fields

        private const string CONN_STRING = "Server={0}; Database={1}; User={2}; Password={3};Connect Timeout={4};";
        private readonly string connectionString;

        #endregion Fields

        #region Constructor

        public DbHelper(string strConnection)
        {
            connectionString = strConnection;
            _instance = this;
        }

        private static DbHelper _instance;
        public static DbHelper GetInstance()
        {
            return _instance;
        }
        #endregion

        #region Method
        /// <summary>
        /// Hàm thực thi command
        /// </summary>
        /// <param name="pCommand"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(SqlCommand pCommand)
        {
            int numRows = 0;
            try
            {
                if (pCommand == null)
                {
                    return 0;
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    pCommand.Connection = connection;
                    numRows = pCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error("DbLibrary.ExecuteNonQuery(DbCommand)", ex);
            }
            finally
            {
                if (pCommand != null)
                {
                    pCommand.Dispose();
                }
            }
            return numRows;
        }


        /// <summary>
        /// Hàm thực thi command trả về DataTable
        /// </summary>
        /// <param name="pCommand"></param>
        /// <returns></returns>
        public DataTable GetDataTable(SqlCommand pCommand)
        {
            DataTable table = null;
            try
            {
                if (pCommand == null)
                {
                    return null;
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    pCommand.Connection = connection;
                    SqlDataAdapter adapter = new SqlDataAdapter
                    {
                        SelectCommand = pCommand
                    };
                    table = new DataTable();
                    adapter.Fill(table);
                    adapter.Dispose();
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error("DbLibrary.GetDataTable(DbCommand)", ex);
            }
            finally
            {
                if (pCommand != null)
                {
                    pCommand.Dispose();
                }
            }
            return table;
        }

        /// <summary>
        /// Hàm thực thi SqlCommand
        /// </summary>
        /// <param name="pCommand"></param>
        /// <returns></returns>
        public object ExecuteScalar(SqlCommand pCommand)
        {
            object value = null;
            try
            {
                if (pCommand == null)
                {
                    return null;
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    pCommand.Connection = connection;
                    value = pCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error("DbLibrary.ExecuteScalar(DbCommand)", ex);
            }
            finally
            {
                if (pCommand != null)
                {
                    pCommand.Dispose();
                }
            }
            return value;
        }

        public bool CheckOpenConnection() //hàm check connection
        {
            bool value = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    value = connection.State == ConnectionState.Open;
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error("DbLibrary.OpenConnection()", ex);
            }

            return value;
        }

        #endregion

        // tạo ra connectionstring đúng cấu trúc 
        public static string GetConnectionString(string server, string database, string userID, string password, string timeout)
        {
            return string.Format(CONN_STRING, server, database, userID, password, timeout);
        }
    }
}
