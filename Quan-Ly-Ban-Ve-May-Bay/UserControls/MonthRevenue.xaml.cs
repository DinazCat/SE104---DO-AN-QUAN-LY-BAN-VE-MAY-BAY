using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Quan_Ly_Ban_Ve_May_Bay.UserControls
{
    /// <summary>
    /// Interaction logic for MonthRevenue.xaml
    /// </summary>
    public partial class MonthRevenue : UserControl
    {
        public MonthRevenue()
        {
            InitializeComponent();
            //ObservableCollection<string> list = new ObservableCollection<string>();

            //list.Add("T1-2023");
            //list.Add("T2-2023");
            //list.Add("T3-2023");
            //list.Add("T4-2023");
            //list.Add("T5-2023");
            //list.Add("T6-2023");
            //list.Add("T7-2023");
            List<int> months = new List<int>();
            for (int month = 1; month <= 12; month++)
            {
                months.Add(month);
            }
            cBoxMonth.ItemsSource = months;
            cBoxYear.SelectedIndex = 0;
            List<int> years = new List<int>();
            int currentYear = DateTime.Now.Year;
            for (int year = 2020; year <= currentYear; year++)
            {
                years.Add(year);
            }
            cBoxYear.ItemsSource = years;
            cBoxYear.SelectedIndex = 0;
            MonthSale Example = new MonthSale();

            Example.stt = "1";
            Example.chuyenbay = "VJ346";
            Example.sove = "1";
            Example.doanhthu = "2 tỷ";
            Example.tile = "10%";
            MonthRevenueTable.Items.Add(Example);
        }
        public class MonthSale
        {
            public string stt { get; set; }
            public string chuyenbay { get; set; }
            public string sove { get; set; }
            public string doanhthu { get; set; }
            public string tile { get; set; }
        }
    }
}
