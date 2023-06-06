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
using Quan_Ly_Ban_Ve_May_Bay.Pages;
using Quan_Ly_Ban_Ve_May_Bay.View;
using Quan_Ly_Ban_Ve_May_Bay.UserControls;
using Quan_Ly_Ban_Ve_May_Bay.Model;
using System.Windows.Threading;
using System.Globalization;

namespace Quan_Ly_Ban_Ve_May_Bay
{

    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();    
        private Home home;
        private FlightsList flights;
        private FlightDetail flightDetail;
        public static Account curAccount = null;
        private AddInforHK addInforHK;
        private AllFlight allFlight;
        private MyBookings myBookings;
        public MainWindow()
        {
            InitializeComponent();
            //hủy vé tự động
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();

            //main code
            home = new Home();
            allFlight = new AllFlight();
            addInforHK = new AddInforHK();
            flightDetail = new FlightDetail();
            home.Search += Home_Search;

            flights = new FlightsList();
            fContainer.Content = home;
        }

        private void Flights_Return(object sender, RoutedEventArgs e)
        {
            fContainer.Content = home;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                DataProvider.sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(
                    "select [c].NgayKhoiHanh, [c].ThoiGianXuatPhat, [v].MaVe " +
                    "from [CHUYENBAY] [c], [VE] [v] " +
                    "where [c].MaChuyenBay = [v].MaChuyenBay and [v].TinhTrang = 'BOOKED'"
                    , DataProvider.sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<string> ticket_des = new List<string>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string strtime = reader["NgayKhoiHanh"].ToString() + " " + reader["ThoiGianXuatPhat"].ToString();
                        DateTime flighttime = DateTime.ParseExact(strtime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                        TimeSpan time = flighttime - DateTime.Now;

                        if (time.Days < 1)
                        {
                            ticket_des.Add(reader["MaVe"].ToString());
                        }
                    }
                }
                DataProvider.sqlConnection.Close();


                if (ticket_des.Count > 0)
                {
                    foreach (string ticket in ticket_des)
                    {
                        DataProvider.sqlConnection.Open();
                        sqlCommand = new SqlCommand(
                            "update [VE] set TinhTrang = 'TRONG', TenHK = NULL, CMND = NULL, SDT = NULL, MaHK = NULL" +
                            " where MaVe = @mave ", DataProvider.sqlConnection);
                        sqlCommand.Parameters.Add("@mave", SqlDbType.NVarChar).Value = ticket;
                        sqlCommand.ExecuteNonQuery();
                        DataProvider.sqlConnection.Close();
                    }

                    if (fContainer.Content == flightDetail)
                    {
                        string flightID = flightDetail.flight_ID.Text;
                        string airlineLogo = flightDetail.airline_logo;
                        TimeSpan time = (TimeSpan)flightDetail.tb_Time.DataContext;
                        DateTime timedes = (DateTime)flightDetail.sp_timeDestination.DataContext;
                        DateTime timedepar = (DateTime)flightDetail.sp_timeDeparture.DataContext;

                        flightDetail = new FlightDetail();
                        flightDetail.Show(flightID,airlineLogo,time, timedes, timedepar, false);
                        flightDetail.Return += FlightDetail_Return;
                        flightDetail.Continue += FlightDetail_Continue;
                        fContainer.Content = flightDetail;
                    }
                    else if (fContainer.Content == myBookings)
                    {
                        myBookings = new MyBookings();
                        myBookings.MyTicket(curAccount.id);
                        fContainer.Content = myBookings;
                    }
                }
            }
            catch (Exception ex) { }
            

        }

        private void AddInforHK_GoToHomeScreen(object sender, RoutedEventArgs e)
        {
            home = new Home();
            fContainer.Content = home;
            home.Search += Home_Search;
        }

        private void FlightDetail_Return(object sender, RoutedEventArgs e)
        {
            if(flightDetail.isAllFlight == true)
            {
                btnAllFlight_Click(sender, e);
            }
            else {
                Home_Search(sender, e);
            }
        }

        private void FlightDetail_Continue(object sender, RoutedEventArgs e)
        {
            if (curAccount != null)
            {
                if (addInforHK == null)
                {
                    addInforHK = new AddInforHK();
                }
                else
                {
                    addInforHK.Close();
                    addInforHK = new AddInforHK();
                }
                addInforHK.GoToHomeScreen += AddInforHK_GoToHomeScreen;
                addInforHK.Show(flightDetail.flightID, flightDetail.TicketID_Chosen, curAccount.id);
                addInforHK.Show();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Vui lòng đăng nhập trước khi đặt vé!");
                if (result == MessageBoxResult.OK)
                {
                    Login login = new Login();
                    login.redirectSignup += Login_redirectSignup;
                    login.loginSuccess += Login_loginSuccess;
                    fContainer.Content = login;
                }
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            home = new Home();
            home.Search += Home_Search;
            fContainer.Content = home;
        }

        private void Home_Search(object sender, RoutedEventArgs e)
        {
            flights = new FlightsList();
            flights.Return += Flights_Return; 
            flights.ShowDetail += Flight_ShowDetail;
            flights.FlightSearched(home.Departure, home.Destination, home.Date, home.Quantity, home.FlightClass);
            fContainer.Content = flights;

        }

        private void Flight_ShowDetail(object sender, RoutedEventArgs e)
        {
            flightDetail = new FlightDetail();
            flightDetail.Show(flights.flightID, flights.airlineLogo, flights.time, flights.dateTimeDestination, flights.dateTimeDeparture, false);
            flightDetail.Return += FlightDetail_Return;
            flightDetail.Continue += FlightDetail_Continue;
            fContainer.Content = flightDetail;
        }
        private void AdminAccessBtn(object sender, RoutedEventArgs e)
        {
            AdminRight adminRight = new AdminRight();
            fContainer.Content = adminRight;
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.curAccount == null)
            {
                Login login = new Login();
                login.redirectSignup += Login_redirectSignup;
                login.loginSuccess += Login_loginSuccess;
                fContainer.Content = login;
            }
            else
            {
                if (MessageBox.Show("Bạn có chắc muốn đăng xuất khỏi tài khoản này?", "Đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    MainWindow.curAccount = null;
                    tblLogin.Text = "Đăng nhập";
                    BitmapImage bitmap = new BitmapImage(new Uri("/Images/login.png", UriKind.Relative));
                    imgLogin.Source = bitmap;
                    btn_UserManagement.Visibility = Visibility.Collapsed;
                    btn_SalesmanRight.Visibility = Visibility.Collapsed;
                    btnHome_Click(sender, e);
                }
            }
        }

        private void Login_loginSuccess(object sender, RoutedEventArgs e)
        {
            btnHome_Click(sender, e);
            tblLogin.Text = "Đăng xuất";
            BitmapImage bitmap = new BitmapImage(new Uri("/Images/logout.png", UriKind.Relative));
            imgLogin.Source = bitmap;
            if (MainWindow.curAccount.type == 1)
            {
                btn_UserManagement.Visibility = Visibility.Visible;
                btn_SalesmanRight.Visibility = Visibility.Visible;
            }
            if (MainWindow.curAccount.type == 2)
            {
                btn_SalesmanRight.Visibility = Visibility.Visible;
            }
        }

        private void Login_redirectSignup(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.redirectLogin += Register_redirectLogin;
            fContainer.Content = register;
        }

        private void Register_redirectLogin(object sender, RoutedEventArgs e)
        {
            loginBtn_Click(sender, e);
        }

        private void userManagement_Click(object sender, RoutedEventArgs e)
        {
            UserManagement userManagement = new UserManagement();
            fContainer.Content = userManagement;
        }

        private void btnAllFlight_Click(object sender, RoutedEventArgs e)
        {
            allFlight = new AllFlight();
            fContainer.Content = allFlight;
            allFlight.ShowDetail += AllFlight_ShowDetail;
        }

        private void AllFlight_ShowDetail(object sender, RoutedEventArgs e)
        {
            flightDetail = new FlightDetail();
            flightDetail.Return += FlightDetail_Return;
            flightDetail.Continue += FlightDetail_Continue;
            flightDetail.Show(allFlight.flightID, allFlight.airlineLogo, allFlight.time, allFlight.dateTimeDestination, allFlight.dateTimeDeparture, true);
            fContainer.Content = flightDetail;
        }
        private void setting_click(object sender, RoutedEventArgs e)
        {
            if (curAccount != null)
            {
                Setting setting = new Setting();
                fContainer.Content = setting;
            }
        }
        private void contactsUs_click(object sender, RoutedEventArgs e)
        {
            ContactUs contactUs = new ContactUs();
            fContainer.Content = contactUs;

        }

        private void btnMyBookings_Click(object sender, RoutedEventArgs e)
        {

            myBookings = new MyBookings();
            if (curAccount == null)
            {
                myBookings.NoLogin();
            }
            else
            {
                BookingsPay bookingsPay = new BookingsPay();
                myBookings.MyTicket(curAccount.id);

            }
            fContainer.Content = myBookings;
        }
    }
}