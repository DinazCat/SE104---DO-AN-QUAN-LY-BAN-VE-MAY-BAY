using MaterialDesignColors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Quan_Ly_Ban_Ve_May_Bay
{
    /// <summary>
    /// Interaction logic for FlightsList.xaml
    /// </summary>
    public partial class FlightsList : Page
    {
        public event RoutedEventHandler ShowDetail;
        public FlightsList()
        {
            InitializeComponent();
            List<Flight> list = new List<Flight>();
            list.Add(new Flight("A", "A1", "A2"));
            list.Add(new Flight("B", "B1", "B2"));
            list.Add(new Flight("C", "C1", "C2"));
            list.Add(new Flight("D", "D1", "D2"));
            list.Add(new Flight("E", "E1", "E2"));
            FlightList.ItemsSource = list;
        }

        private void FlightList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFlight = (Flight)FlightList.SelectedItem;   
            if (selectedFlight != null) {
                ShowDetail?.Invoke(this, new RoutedEventArgs());
                FlightList.SelectedIndex = -1;
            }
        }
    }

}
