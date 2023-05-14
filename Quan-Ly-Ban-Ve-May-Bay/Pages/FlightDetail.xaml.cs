using Quan_Ly_Ban_Ve_May_Bay.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Quan_Ly_Ban_Ve_May_Bay
{
    /// <summary>
    /// Interaction logic for FlightDetail.xaml
    /// </summary>
    public partial class FlightDetail : Page
    {
        List<FlightClass> flight_classes;
        public event RoutedEventHandler Return;

        //SqlConnection sqlConnection = new SqlConnection(@"Server=(local);Database=QuanLyBanVeMayBay;Trusted_Connection=Yes;");
        //SqlCommand sqlCommand = new SqlCommand();
        //SqlDataAdapter adapter;
        //DataSet ds;

        public FlightDetail()
        {
            InitializeComponent();
            flight_classes = new List<FlightClass>();
            flight_classes.Add(new FlightClass("Đã đặt", "#FF95988E"));
            List<string> strings = new List<string>();
            //sqlConnection.Open();
            //addDataToClassColor();
            //ds = null;
            //adapter.Dispose();
            //sqlConnection.Close();
            //sqlConnection.Dispose();

            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");
            strings.Add("A");


            //SBTrungGian.ItemsSource = strings;
            //if (SBTrungGian.ItemsSource == null)
            //{
            //    SBTrungGianView.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    SBTrungGianView.Visibility = Visibility.Visible;
            //}
            SeatsChart1.ItemsSource = strings;
            SeatsChart2.ItemsSource = strings;
            SeatsChart3.ItemsSource = strings;
            SeatsChart4.ItemsSource = strings;
            SeatsChart5.ItemsSource = strings;
            ClassesColor.ItemsSource = flight_classes;
            SeatsChart6.ItemsSource = strings;
        }
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            Return?.Invoke(this, new RoutedEventArgs());
        }
        //public void addDataToClassColor()
        //{ 
        //    sqlCommand = new SqlCommand(
        //    "select * from HANGVE order by TenHangVe asc", sqlConnection);
        //    adapter = new SqlDataAdapter(sqlCommand);
        //    ds = new DataSet();
        //    adapter.Fill(ds);
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        flight_classes.Add(new FlightClass(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString()));
        //    }
        //    ClassesColor.ItemsSource = flight_classes;

        //}
    }
}
