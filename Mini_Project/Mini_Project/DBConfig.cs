using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Mini_Project
{
    //these are used all over the project space
    public static class DBConfig
    {
        public static string ConnectionString = "Data Source=ICS-LT-7ZS9J84\\SQLEXPRESS;Initial Catalog=Mini_Project;" +
            "user id=sa;password=Jayavardhan@2003;";
        public static SqlConnection Connection = new SqlConnection(ConnectionString);
        public static SqlCommand Command;
        public static SqlDataReader Reader;

        public static void OpenConnection()
        {
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
        }

        public static void CloseConnection()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }

        public static void CloseReader()
        {
            if (Reader != null && !Reader.IsClosed)
                Reader.Close();
        }
    }
    
}


