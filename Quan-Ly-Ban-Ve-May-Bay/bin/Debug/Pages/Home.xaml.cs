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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Quan_Ly_Ban_Ve_May_Bay.MainWindow;
using Quan_Ly_Ban_Ve_May_Bay.Model;

namespace Quan_Ly_Ban_Ve_May_Bay.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public string departure;
        public string destination;
        public string _date;
        public int quantity;
        public string flightClass;
        
        public event RoutedEventHandler Search;
        public Home()
        {
            InitializeComponent();
            date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            addDataToClass();
            addDataToCCBDeparture();
            addDataToCCBDestination();
        }
        public void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (cbbDeparture.Text == "" || cbbDestination.Text == "" || cbbQuantity.Text == "" || date.Text == "")
            {
                MessageBox.Show("Mời bạn chọn đầy đủ thông tin");
            }
            else
            {
                if (cbbDestination.Text == cbbDeparture.Text)
                {
                    MessageBox.Show("Không được chọn nơi đến trùng nơi xuất phát");
                }
                else
                {
                    departure = cbbDeparture.Text;
                    destination = cbbDestination.Text;
                    quantity = int.Parse(cbbQuantity.Text);
                    _date = date.Text;
                    flightClass = cbbClass.Text;
                    Search?.Invoke(this, new RoutedEventArgs());
                }
            }
        }
        private void addDataToCCBDestination()
        {
            SqlCommand sqlCommand = new SqlCommand(
            "select distinct Tinh from [SANBAY] [s], [CHUYENBAY] [c] where [s].MaSanBay = [c].SanBayDen", DataProvider.sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            List<string> listDestination = new List<string>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                listDestination.Add(dr[0].ToString());
            }
            cbbDestination.ItemsSource = listDestination;
        }
        private void addDataToCCBDeparture()
        {
            SqlCommand sqlCommand = new SqlCommand(
            "select distinct Tinh from [SANBAY] [s], [CHUYENBAY] [c] where [s].MaSanBay = [c].SanBayDi", DataProvider.sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            List<string> listDeparture = new List<string>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                listDeparture.Add(dr[0].ToString());
            }
            cbbDeparture.ItemsSource = listDeparture;
        }
        private void addDataToClass()
        {
            SqlCommand sqlCommand = new SqlCommand(
            "select TenHangVe from [HANGVE]", DataProvider.sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            List<string> listDeparture = new List<string>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                listDeparture.Add(dr[0].ToString());
            }
            cbbClass.ItemsSource = listDeparture;
        }
    }
}
