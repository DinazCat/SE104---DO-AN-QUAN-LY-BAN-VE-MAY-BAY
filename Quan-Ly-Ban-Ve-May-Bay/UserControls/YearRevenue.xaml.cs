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

namespace Quan_Ly_Ban_Ve_May_Bay.UserControls
{
    /// <summary>
    /// Interaction logic for YearRevenue.xaml
    /// </summary>
    public partial class YearRevenue : UserControl
    {
        public YearRevenue()
        {
            InitializeComponent();
            cBox.Items.Add("2020");
            cBox.Items.Add("2021");
            cBox.Items.Add("2022");
            cBox.SelectedIndex = 0;
            YearSale Example = new YearSale();
            Example.stt = "1";
            Example.sochuyenbay = "50 C";
            Example.thang = "T1";
            Example.doanhthu = "50 tỷ";
            Example.tile = "10%";
            YearRevenueTable.Items.Add(Example);
        }
        public class YearSale
        {
            public string stt { get; set; }
            public string sochuyenbay { get; set; }
            public string thang { get; set; }
            public string doanhthu { get; set; }
            public string tile { get; set; }
        }
    }
}
