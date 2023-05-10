using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Ve_May_Bay.Model
{
    internal class DataProvider
    {
        private static string connectionStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True";
        // SqlConnection connect = new SqlConnection(conn);
        public static SqlDataReader ExecuteReader(String commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connectionStr);

            SqlCommand cmd = new SqlCommand(commandText, conn);

            cmd.CommandType = commandType;
            cmd.Parameters.AddRange(parameters);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
    }
}
