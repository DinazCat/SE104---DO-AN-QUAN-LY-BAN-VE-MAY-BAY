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
using System.Windows.Shapes;

namespace Quan_Ly_Ban_Ve_May_Bay
{
    /// <summary>
    /// Interaction logic for FlightDetail.xaml
    /// </summary>
    public partial class FlightDetail : Page
    {
        public event RoutedEventHandler Return;
        public FlightDetail()
        {
            InitializeComponent();
            List<string> strings = new List<string>();
            List<string> strings1 = new List<string>();
            strings1.Add("A");
            strings1.Add("A");
            strings1.Add("A");
            strings1.Add("A");
            strings1.Add("A");

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

            SBTrungGian.ItemsSource = strings1;
            ClassesColor.ItemsSource = strings1;
            SeatsChart1.ItemsSource = strings;
            SeatsChart2.ItemsSource = strings;
            SeatsChart3.ItemsSource = strings;
            SeatsChart4.ItemsSource = strings;
            SeatsChart5.ItemsSource = strings;
            SeatsChart6.ItemsSource = strings;
        }
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            Return?.Invoke(this, new RoutedEventArgs());
        }
    }
}
