using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ElectricityBoardBilling.Components
{
    public class DBHandler
    {
        public static SqlConnection GetConnection()
        {
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["EB_ConnectionString"].ConnectionString;
                return new SqlConnection(connStr);
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to create SQL connection. Check connection string and SQL Server status.", ex);
            }
        }
    }
}
