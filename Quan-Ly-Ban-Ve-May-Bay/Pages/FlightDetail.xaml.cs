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

        public FlightDetail()
        {
            InitializeComponent();
            Dictionary<string, List<string>> D = new Dictionary<string, List<string>>();
            D.Add("A", new List<string>() { "a1", "a2"});
            D.Add("B", new List<string>() { "a1", "a2" });
            Transit_ListView.ItemsSource = D;
            List<string> strings = new List<string>() { "a", "b" };
            HangVe_ListView.ItemsSource = strings;
            SeatChart.ItemsSource = strings;
        }

    }
}
