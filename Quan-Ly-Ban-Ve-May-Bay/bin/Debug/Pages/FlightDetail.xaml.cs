using Quan_Ly_Ban_Ve_May_Bay.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Quan_Ly_Ban_Ve_May_Bay.Converter;
using System.Globalization;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Documents;

namespace Quan_Ly_Ban_Ve_May_Bay
{
    /// <summary>
    /// Interaction logic for FlightDetail.xaml
    /// </summary>
    public partial class FlightDetail : Page
    {
        List<FlightClass> flight_classes;
        public event RoutedEventHandler Return;
        public event RoutedEventHandler Continue;
        public bool isAllFlight;

        public FlightDetail()
        {
            InitializeComponent();
            flight_classes = new List<FlightClass>();
            flight_classes.Add(new FlightClass("Đã đặt", "#FF95988E"));
            List<string> strings = new List<string>();
            addDataToClassColor();

            sp_timeDeparture.DataContext = new DateTime();
            sp_timeDestination.DataContext = new DateTime();
            tb_Time.DataContext = new TimeSpan();

            ClassesColor.ItemsSource = flight_classes;
        }
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            Return?.Invoke(this, new RoutedEventArgs());
        }
        public void addDataToClassColor()
        {
            SqlCommand sqlCommand = new SqlCommand(
            "select * from [HANGVE] order by TenHangVe asc", DataProvider.sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            
            ClassesColor.ItemsSource = flight_classes;
            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                flight_classes.Add(new FlightClass(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), DataProvider.colorList[i]));
            }
            ClassesColor.ItemsSource = flight_classes;

        }
        //show detail
        public void Show(string flightID, string airlineLogo, TimeSpan time, DateTime dateTimeDestination, DateTime dateTimeDeparture, bool _isAllFlight)
        {
            isAllFlight = _isAllFlight;
            flight_ID.Text = flightID;
            sp_timeDeparture.DataContext = dateTimeDeparture;
            sp_timeDestination.DataContext = dateTimeDestination;
            tb_Time.DataContext = time;

            ImageSource imgSource = new BitmapImage(new Uri(airlineLogo, UriKind.Relative));
            image_Logo.Source = imgSource;

            DataProvider.sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(
             "select [c].*, [s1].Tinh TinhSBDi, [s2].Tinh TinhSBDen, [s1].TenSanBay TenSBDi, [s2].TenSanBay TenSBDen, TenHang from [CHUYENBAY] [c], [SANBAY] [s1], [SANBAY] [s2], [HANGMAYBAY] [hmb] " +
             "where [c].MaChuyenBay=@flightID " +
             "and [c].SanBayDi = [s1].MaSanBay " +
             "and [c].SanBayDen = [s2].MaSanBay " +
             "and [c].MaHangMayBay = [hmb].MaHang",
            DataProvider.sqlConnection);
            sqlCommand.Parameters.Add("@flightID", SqlDbType.NVarChar).Value = flightID;
            SqlDataReader reader = sqlCommand.ExecuteReader();
            string[] flight = new string[8];
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    string airlineName = reader["TenHang"].ToString();
                    string airportDepartureID = reader["SanBayDi"].ToString();
                    string airportDestinationID = reader["SanBayDen"].ToString();
                    string airportDepartureName = reader["TenSBDi"].ToString();
                    string airportDestinationName = reader["TenSBDen"].ToString();
                    string airportDepartureCity = reader["TinhSBDi"].ToString();
                    string airportDestinationCity = reader["TinhSBDen"].ToString();
                    string aircraftType = reader["LoaiMayBay"].ToString();
                    flight = new string[] { airlineName, airportDepartureID, airportDestinationID, airportDepartureName, airportDestinationName, airportDepartureCity, airportDestinationCity, aircraftType };
                }
            }
            DataProvider.sqlConnection.Close();
            tb_airportDeparture.Text = flight[5] + "(" + flight[3] + ")";
            tb_airportDepartureID.Text = flight[1];
            tb_airportDestination.Text = flight[6] + "(" + flight[4] + ")";
            tb_airportDestinationID.Text = flight[2];
            tb_airlineName.Text = flight[0];
            tb_aircraftType.Text = flight[7];
            addDataToSBTrungGian(flightID);
        }

        //đổ dữ liệu cho sân bay trung gian
        private void addDataToSBTrungGian(string flightID)
        {
            DataProvider.sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(
             "select [s].TenSanBay, [sbtg].ThoiGianDung from [SANBAYTRUNGGIAN] [sbtg], [SANBAY] [s]" +
             "where [sbtg].MaChuyenBay=@flightID " +
             "and [sbtg].SanBayTrungGian = [s].MaSanBay",
            DataProvider.sqlConnection);
            sqlCommand.Parameters.Add("@flightID", SqlDbType.NVarChar).Value = flightID;
            SqlDataReader reader = sqlCommand.ExecuteReader();
            List<SBTrungGian> listSBTrungGian = new List<SBTrungGian>();
            if (reader.HasRows)
            {
                while(reader.Read())
                {
                    string airportName = reader["TenSanBay"].ToString();
                    TimeSpan timeStop = TimeSpan.FromMinutes(double.Parse(reader["ThoiGianDung"].ToString()));
                    listSBTrungGian.Add(new SBTrungGian(airportName, timeStop));
                }
            }
            DataProvider.sqlConnection.Close();
            if (listSBTrungGian.Count == 0)
            {
                SBTrungGianView.Visibility = Visibility.Collapsed;
            }
            else
            {
                SBTrungGianView.Visibility = Visibility.Visible;
                ic_SBTrungGian.ItemsSource = listSBTrungGian;
            }
            addDataToSeat(flightID);
        }

        //add data for ticket
        List<Ticket> tickets1 = new List<Ticket>();
        List<Ticket> tickets2 = new List<Ticket>();
        List<Ticket> tickets3 = new List<Ticket>();
        List<Ticket> tickets4 = new List<Ticket>();
        List<Ticket> tickets5 = new List<Ticket>();
        List<Ticket> tickets6 = new List<Ticket>();
        private void addDataToSeat(string flightID)
        {
            tickets1 = new List<Ticket>();
            tickets2 = new List<Ticket>();
            tickets3 = new List<Ticket>();
            tickets4 = new List<Ticket>();
            tickets5 = new List<Ticket>();
            TicketID_Chosen = new List<string>();
            tickets6 = new List<Ticket>();
            for (int i = 1; i < flight_classes.Count; i++)
            {
                DataProvider.sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(
                 "select [v].MaVe, [v].MaHangVe, [v].SoGhe, [v].TinhTrang from [VE] [v], [HANGVE] [hv]" +
                 "where [v].MaChuyenBay=@flightID " +
                 "and [v].MaHangVe = [hv].MaHangVe " +
                 "and [hv].MaHangVe=@flightClassID " +
                 "order by cast(SoGhe as int) asc",
                DataProvider.sqlConnection);
                sqlCommand.Parameters.Add("@flightID", SqlDbType.NVarChar).Value = flightID;
                sqlCommand.Parameters.Add("@flightClassID", SqlDbType.NVarChar).Value = flight_classes[i].ClassId;
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string ticketID = reader["MaVe"].ToString();
                        string flightClass = reader["MaHangVe"].ToString();
                        int seatNumber = int.Parse(reader["SoGhe"].ToString());
                        string status = reader["TinhTrang"].ToString();
                        string color;
                        if (status == "TRONG")
                        {
                            color = flight_classes[i].ClassColor;
                        }
                        else
                        {
                            color = "#FF95988E";
                            // enable = false;
                        }

                        switch (seatNumber % 6)
                        {
                            case 1:
                                tickets1.Add(new Ticket(ticketID, flightClass, seatNumber, status, color));
                                break;
                            case 2:
                                tickets2.Add(new Ticket(ticketID, flightClass, seatNumber, status, color));
                                break;
                            case 3:
                                tickets3.Add(new Ticket(ticketID, flightClass, seatNumber, status, color));
                                break;
                            case 4:
                                tickets4.Add(new Ticket(ticketID, flightClass, seatNumber, status, color));
                                break;
                            case 5:
                                tickets5.Add(new Ticket(ticketID, flightClass, seatNumber, status, color));
                                break;
                            case 0:
                                tickets6.Add(new Ticket(ticketID, flightClass, seatNumber, status, color));
                                break;
                        }
                    }
                }
                DataProvider.sqlConnection.Close();
            }


            SeatsChart1.ItemsSource = tickets1;
            SeatsChart2.ItemsSource = tickets2;
            SeatsChart3.ItemsSource = tickets3;
            SeatsChart4.ItemsSource = tickets4;
            SeatsChart5.ItemsSource = tickets5;
            SeatsChart6.ItemsSource = tickets6;
        }

        public List<string> TicketID_Chosen = new List<string>();
        public string flightID;
        private void BtnChose_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Ticket ticket = (Ticket)button.DataContext;
            //SolidColorBrush colorgray = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF95988E");
            if (ticket.Color != "#FF95988E")
            {
                if (button.Background != Brushes.Green)
                {
                    button.Background = Brushes.Green;
                    TicketID_Chosen.Add(ticket.TiketID);
                }
                else
                {
                    TicketID_Chosen.Remove(ticket.TiketID);
                    button.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ticket.Color);
                    button.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ticket.Color);
                }
            }
        }

        private void btnCont_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand(
             "select GiaTri from [BANGTHAMSO] " +
             "where convert(varchar, TenThamSo)='ThoiGianChamNhatChoPhepDatVe'", DataProvider.sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            List<string> listDestination = new List<string>();
            string _tgChamNhatDatVe = ds.Tables[0].Rows[0][0].ToString();
            TimeSpan tgChamNhatDatVe = TimeSpan.FromHours(double.Parse(_tgChamNhatDatVe));
            DateTime dateTimeDeparture = (DateTime)sp_timeDeparture.DataContext;
            DateTime dateTimeNow = DateTime.Now;
            if (TicketID_Chosen.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn vé muốn đặt!");
            }
            else
            {
                if (dateTimeDeparture - dateTimeNow - tgChamNhatDatVe > TimeSpan.Zero)
                {
                    flightID = flight_ID.Text;
                    Continue?.Invoke(this, new RoutedEventArgs());
                }
                else
                {
                    MessageBox.Show("Vé phải được đặt chậm nhất trước giờ khởi hành là: " + _tgChamNhatDatVe + " giờ");
                }
            }
        }
    }
}