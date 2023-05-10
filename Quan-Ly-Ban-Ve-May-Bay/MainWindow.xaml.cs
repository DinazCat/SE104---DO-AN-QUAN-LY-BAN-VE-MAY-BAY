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
using Quan_Ly_Ban_Ve_May_Bay.UserControls;
using Quan_Ly_Ban_Ve_May_Bay.Model;
using Microsoft.Win32;

namespace Quan_Ly_Ban_Ve_May_Bay
{

    public partial class MainWindow : Window
    {
        public static Account curAccount;
        public MainWindow()
        {
            InitializeComponent();
            Home home = new Home();
            home.Search += Home_Search;
            fContainer.Content = home;
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Search += Home_Search;
            fContainer.Content = home; 
        }

        private void Home_Search(object sender, RoutedEventArgs e)
        {
            FlightsList flight = new FlightsList();
            fContainer.Content = flight;
            flight.ShowDetail += Flight_ShowDetail;
        }

        private void Flight_ShowDetail(object sender, RoutedEventArgs e)
        {
            FlightDetail flightDetai = new FlightDetail();
            fContainer.Content = flightDetai;

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
                MainWindow.curAccount = null;
                tblLogin.Text = "Login";
                BitmapImage bitmap = new BitmapImage(new Uri("/Images/login.png", UriKind.Relative));
                imgLogin.Source = bitmap;
                btn_UserManagement.Visibility = Visibility.Collapsed;
                btn_SalesmanRight.Visibility = Visibility.Collapsed;
                btnHome_Click(sender, e);
            }
        }

        private void Login_loginSuccess(object sender, RoutedEventArgs e)
        {
            btnHome_Click(sender, e);
            tblLogin.Text = "Logout";
            BitmapImage bitmap = new BitmapImage(new Uri("/Images/logout.png", UriKind.Relative));
            imgLogin.Source = bitmap;
            if(MainWindow.curAccount.type == 1){
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
            Register register= new Register();
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
    }
}
