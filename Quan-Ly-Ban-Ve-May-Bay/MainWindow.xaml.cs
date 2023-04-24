using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace Quan_Ly_Ban_Ve_May_Bay
{

    public partial class MainWindow : Window
    {
        static string strConnection = @"Data Source=LAPTOP-68IV7KVK\SQLEXPRESS;Initial Catalog=Data;Integrated Security=True";
        SqlConnection sqlConnection = new SqlConnection(strConnection);
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataAdapter adapter;
        DataSet ds;
        SqlDataReader reader;
        public MainWindow()
        {
            InitializeComponent();
            bindcombo();
        }

        public List<destination> des { get; set; }

        private void bindcombo()
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("select * from ccb_destination", sqlConnection);
            adapter = new SqlDataAdapter(sqlCommand);
            ds = new DataSet();
            adapter.Fill(ds, "ccb_destination");
            destination des = new destination();
            IList<destination> destinations = new List<destination>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                destinations.Add(new destination
                {
                    code = dr[0].ToString(),
                    city = dr[1].ToString()
                });
            }
            ds = null;
            adapter.Dispose();
            sqlConnection.Close();
            sqlConnection.Dispose();
            cbb_from.ItemsSource = destinations;
            cbb_from.DisplayMemberPath = "city";
            cbb_to.ItemsSource = destinations;
            cbb_to.DisplayMemberPath = "city";

        }

        
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            FlightsList flightWindow = new FlightsList();
            flightWindow.Show();
        }

        public class destination
        {
            public string code { get; set; }
            public string city { get; set; }
        }
    }
}
