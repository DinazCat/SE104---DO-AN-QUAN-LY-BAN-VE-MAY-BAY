using System;
using System.Collections.Generic;
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
using Quan_Ly_Ban_Ve_May_Bay.Pages;
using Quan_Ly_Ban_Ve_May_Bay.View;

namespace Quan_Ly_Ban_Ve_May_Bay.Pages
{
    /// <summary>
    /// Interaction logic for MyBookings.xaml
    /// </summary>
    public partial class MyBookings : Page
    {
        BookingsDetail bookingsDetail;
        BookingsUpdate bookingsUpdate;
        public MyBookings()
        {
            InitializeComponent();
            bookingsDetail = new BookingsDetail();
            bookingsUpdate = new BookingsUpdate();
            //fTicket.Content= bookingsDetail;
            //bookingsUpdate.ShowDialog();
        }
    }
}
