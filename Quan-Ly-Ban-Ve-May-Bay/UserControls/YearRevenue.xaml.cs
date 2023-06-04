using Quan_Ly_Ban_Ve_May_Bay.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
            List<int> years = new List<int>();
            int currentYear = DateTime.Now.Year;
            for (int year = 2020; year <= currentYear; year++)
            {
                years.Add(year);
            }
            cBox.ItemsSource = years;
            cBox.SelectedIndex = 0;
        }
        public class YearSale
        {
            public string sochuyenbay { get; set; }
            public string thang { get; set; }
            public string doanhthu { get; set; }
            public string tile { get; set; }
        }
        DataTable dt;
        private void loadData()
        {
            YearRevenueTable.Items.Clear();
            string query =
                "SELECT SUBSTRING(C.NgayKhoiHanh, 4, 2) AS Thang, " +
                    "(SELECT COUNT(DISTINCT C1.MaChuyenBay) " +
                        "FROM CHUYENBAY C1 WHERE SUBSTRING(C1.NgayKhoiHanh, 4, 2) = SUBSTRING(C.NgayKhoiHanh, 4, 2) AND SUBSTRING(C1.NgayKhoiHanh, 7, 4) = @Year) AS SoChuyenBay, " +
                    "SUM(V.GiaVe) AS DoanhThu, " +
                    "(SUM(CAST(V.GiaVe AS decimal(18, 2))) * 100) / " +
                        "(SELECT SUM(GiaVe) FROM VE WHERE SUBSTRING(C.NgayKhoiHanh, 7, 4) = @Year AND TinhTrang = 'SOLD') AS TyLe " +
                "FROM CHUYENBAY C JOIN VE V ON C.MaChuyenBay = V.MaChuyenBay " +
                "WHERE SUBSTRING(C.NgayKhoiHanh, 7, 4) = @Year AND TinhTrang = 'SOLD' " +
                "GROUP BY SUBSTRING(C.NgayKhoiHanh, 4, 2), C.NgayKhoiHanh " +
                "ORDER BY SUBSTRING(C.NgayKhoiHanh, 4, 2) ASC";
            SqlParameter param1 = new SqlParameter("@Year", int.Parse(cBox.SelectedItem.ToString()));
            try
            {
                
                using (SqlDataReader reader = DataProvider.ExecuteReader(query, CommandType.Text, param1))
                {
                    dt = new DataTable();
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                }

                int sum = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    YearSale ys = new YearSale();
                    ys.thang = dr[0].ToString();
                    ys.sochuyenbay = dr[1].ToString();
                    ys.doanhthu = dr[2].ToString();
                    ys.tile = dr[3].ToString();
                    YearRevenueTable.Items.Add(ys);
                    sum += dr.Field<int>(2);
                }
                tb_total.Text = sum.ToString();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        private void cbox_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadData();
        }
        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = null;
            Microsoft.Office.Interop.Excel.Workbook wb = null;
            object missing = Type.Missing;
            Microsoft.Office.Interop.Excel.Worksheet ws = null;
            Microsoft.Office.Interop.Excel.Range rng = null;



            excel = new Microsoft.Office.Interop.Excel.Application();
            wb = excel.Workbooks.Add();
            ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
            ws.Columns.AutoFit();
            ws.Columns.EntireColumn.ColumnWidth = 25;


            for (int i = 0; i < YearRevenueTable.Columns.Count; i++)
            {
                ws.Cells[1, i + 1].Value = YearRevenueTable.Columns[i].Header;
                for (int j = 0; j < YearRevenueTable.Items.Count; j++)
                {
                    var cellValue = (YearRevenueTable.Columns[i].GetCellContent(YearRevenueTable.Items[j]) as TextBlock).Text;
                    ws.Cells[j + 2, i + 1].Value = cellValue;
                }
            }

            excel.Visible = true;
            wb.Activate();
        }
    }
}
