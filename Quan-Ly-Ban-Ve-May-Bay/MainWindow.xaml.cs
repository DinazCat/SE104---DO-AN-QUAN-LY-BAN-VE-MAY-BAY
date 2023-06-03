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
using Microsoft.Win32;
using System.Globalization;

namespace Quan_Ly_Ban_Ve_May_Bay
{

    public partial class MainWindow : Window
    {

        private Home home;
        private FlightsList flights;
        private FlightDetail flightDetail;
        public static Account curAccount = null;
        private AddInforHK addInforHK;
        private AllFlight allFlight;
        public MainWindow()
        {
            InitializeComponent();
            home = new Home();
            allFlight = new AllFlight();
            addInforHK = new AddInforHK();
            flights = new FlightsList();
            flightDetail = new FlightDetail();
            home.Search += Home_Search;
            flights.ShowDetail += Flight_ShowDetail;
            flights.Return += btnHome_Click;
            flightDetail.Return += FlightDetail_Return;
            
            //continue
            flightDetail.Continue += FlightDetail_Continue;
            //
            
            flights.Search += btnHome_Click;
            fContainer.Content = home;
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
            fContainer.Content = home;
        }

        private void Home_Search(object sender, RoutedEventArgs e)
        {
            flights.FlightSearched(home.Departure, home.Destination, home.Date, home.Quantity, home.FlightClass);
            fContainer.Content = flights;

        }

        private void Flight_ShowDetail(object sender, RoutedEventArgs e)
        {
            flightDetail.Show(flights.flightID, flights.airlineLogo, flights.time, flights.dateTimeDestination, flights.dateTimeDeparture, false);
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
                    tblLogin.Text = "Login";
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
            tblLogin.Text = "Logout";
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
            fContainer.Content = allFlight;
            allFlight.ShowDetail += AllFlight_ShowDetail;
        }

        private void AllFlight_ShowDetail(object sender, RoutedEventArgs e)
        {
            flightDetail.Show(allFlight.flightID, allFlight.airlineLogo, allFlight.time, allFlight.dateTimeDestination, allFlight.dateTimeDeparture, true);
            fContainer.Content = flightDetail;
        }
    }
}