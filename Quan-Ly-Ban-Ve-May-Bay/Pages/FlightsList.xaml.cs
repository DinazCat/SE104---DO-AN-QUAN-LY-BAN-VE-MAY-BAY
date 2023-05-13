using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using Quan_Ly_Ban_Ve_May_Bay.Model;

namespace Quan_Ly_Ban_Ve_May_Bay
{
    /// <summary>
    /// Interaction logic for FlightsList.xaml
    /// </summary>
    public partial class FlightsList : Page
    {
        public event RoutedEventHandler ShowDetail;
        public event RoutedEventHandler Return;
        SqlConnection sqlConnection = new SqlConnection(@"Server=(local);Database=QuanLyBanVeMayBay;Trusted_Connection=Yes;");
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataAdapter adapter;
        DataSet ds;
        public event RoutedEventHandler Search;
        public FlightsList()
        {
            InitializeComponent();
        }
        public FlightsList(String departure, String destination, int quantity, String flightClass)
        {
            InitializeComponent();
            sqlConnection.Open();
        }


        private void FlightList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFlight = (Flight)FlightList.SelectedItem;   
            if (selectedFlight != null) {
                ShowDetail?.Invoke(this, new RoutedEventArgs());
                FlightList.SelectedIndex = -1;
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            Return?.Invoke(this, new RoutedEventArgs());
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search?.Invoke(this, new RoutedEventArgs());
        }
        private void addDataFlightList(String departure, String destination, int quantity, String flightClass)
        {
            sqlCommand = new SqlCommand(
            "select * from CHUYENBAY", sqlConnection);
            adapter = new SqlDataAdapter(sqlCommand);
            ds = new DataSet();
            adapter.Fill(ds);
            List<FlightClass> flights = new List<FlightClass>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
            }
            FlightList.ItemsSource = flights;
        }

    }

}
