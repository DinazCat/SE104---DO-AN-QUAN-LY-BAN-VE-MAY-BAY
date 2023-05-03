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

namespace Quan_Ly_Ban_Ve_May_Bay
{

    public partial class MainWindow : Window
    {
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
    }
}
