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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Diagnostics;
using System.Globalization;

namespace Quan_Ly_Ban_Ve_May_Bay
{
    /// <summary>
    /// Interaction logic for FlightsList.xaml
    /// </summary>
    public partial class FlightsList : Page
    {
        public int Stop;
        public event RoutedEventHandler ShowDetail;
        public event RoutedEventHandler Return;
       
        public event RoutedEventHandler Search;
        public FlightsList()
        {
            InitializeComponent();
        }

        //set up flightList Screen
        public void FlightSearched(string departure, string destination, string date, int quantity, string flightClass)
        {
            infoSearch.Text = departure + " -> " + destination + " | " + date + " | " + flightClass + " | " + quantity + " người";
            DataProvider.sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(
             "select [c].*, Logo, TenHang,(select count(*) from [SANBAYTRUNGGIAN] [sbtg] where [sbtg].MaChuyenBay = [c].MaChuyenBay) SoSBTG from [CHUYENBAY] [c], [SANBAY] [s1], [SANBAY] [s2], [HANGMAYBAY] [hmb] " +
             "where NgayKhoiHanh=@date " +
             "and [c].SanBayDi=[s1].MaSanBay " +
             "and [c].SanBayDen=[s2].MaSanBay " +
             "and [c].MaHangMayBay=[hmb].MaHang " +
             "and [s1].Tinh=@departure " +
             "and [s2].Tinh=@destination " +
             "and MaChuyenBay in (select MaChuyenBay from [VE] [v], [HANGVE] [hv] " +
                                   " where TinhTrang = N'Trong' and [v].MaHangVe = [hv].MaHangVe " +
                                   "and TenHangVe=@flightClass " +
                                   "group by MaChuyenBay " +
                                   "having count(MaVe)>=@quantity)",

            DataProvider.sqlConnection);
            sqlCommand.Parameters.Add("@date", SqlDbType.NVarChar).Value = date;
            sqlCommand.Parameters.Add("@departure", SqlDbType.NVarChar).Value = departure;
            sqlCommand.Parameters.Add("@destination", SqlDbType.NVarChar).Value = destination;
            sqlCommand.Parameters.Add("@flightClass", SqlDbType.NVarChar).Value = flightClass;
            sqlCommand.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;
            SqlDataReader reader = sqlCommand.ExecuteReader();
            List<Flight> flight_list = new List<Flight>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string flightID = reader["MaChuyenBay"].ToString();
                    string airlineLogo = reader["Logo"].ToString();
                    string airlineName = reader["TenHang"].ToString();
                    string airportDepartureName = reader["SanBayDi"].ToString();
                    string airportDestinationName = reader["SanBayDen"].ToString();
                    string timeDeparture = reader["ThoiGianXuatPhat"].ToString();
                    string time = reader["ThoiGianDuKien"].ToString();
                    TimeSpan khoangtg = TimeSpan.FromMinutes(double.Parse(time));

                    TimeSpan tgXuatPhat = TimeSpan.ParseExact(timeDeparture, @"hh\:mm", CultureInfo.InvariantCulture);

                    string timeDestination = (khoangtg + tgXuatPhat).ToString(@"hh\:mm");
                    if (int.Parse(time) > 60)
                    {
                        time = khoangtg.ToString(@"hh\:mm");
                    }
                    string stop = reader["SoSBTG"].ToString();
                    string price = reader["Gia"].ToString();
                    flight_list.Add(new Flight(flightID, airlineLogo, airlineName, airportDepartureName, airportDestinationName, timeDestination, timeDeparture, time, stop, price));   
                }
            }
            DataProvider.sqlConnection.Close();

            FlightList.ItemsSource = flight_list;
            if (flight_list.Count == 0)
            {
                isHavingData.Text = "Không tìm thấy chuyến bay thích hợp";
            }
            else
            {
                isHavingData.Text = "Tất cả những chuyến bay đã tìm thấy";
            }
        }


        private void FlightList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFlight = (Flight)FlightList.SelectedItem;   
            if (selectedFlight != null) {
                Stop = int.Parse(selectedFlight.Stop);
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
        

    }

}
