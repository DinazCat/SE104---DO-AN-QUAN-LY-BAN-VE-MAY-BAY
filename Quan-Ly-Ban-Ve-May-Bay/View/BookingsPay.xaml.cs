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
using Quan_Ly_Ban_Ve_May_Bay.Model;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace Quan_Ly_Ban_Ve_May_Bay.View
{
    /// <summary>
    /// Interaction logic for BookingsUpdate.xaml
    /// </summary>
    public partial class BookingsPay : Window
    {
        private string MaHD;
        public BookingsPay()
        {
            InitializeComponent();
        }

        public void ShowBill(string maHD, string ngaylap, string maCB, string giobay, string tien)
        {
            MaHD = maHD;
            maHDTxt.Text = maHD;
            ngayHDTxt.Text = ngaylap;
            tienTxt.Text = tien;
            tuyenTxt.Text = maCB;
            ngayGioTxt.Text = giobay;

            if (MainWindow.curAccount.type == 1 || MainWindow.curAccount.type == 2)
            {
                btnPay.Content = "Đã thanh toán";
            }

            DataProvider.sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(
                "select [v].MaVe, [hv].TenHangVe, [v].SoGhe, [v].TenHK " +
                "from [VE] [v], [HANGVE] [hv], [CTHD] [ct] " +
                "where [hv].MaHangVe = [v].MaHangVe and [ct].MaHD = @maHD and [v].MaVe = [ct].MaVe"
                , DataProvider.sqlConnection);
            sqlCommand.Parameters.Add("@maHD", SqlDbType.NVarChar).Value = maHD;
            SqlDataReader reader = sqlCommand.ExecuteReader();
            List<SymbolTicket> list_ticker = new List<SymbolTicket>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string mave = reader["MaVe"].ToString();
                    string soghe = reader["SoGhe"].ToString();
                    string hangve = reader["TenHangVe"].ToString();
                    string tenHK = reader["TenHK"].ToString();
                    list_ticker.Add(new SymbolTicket(mave, soghe, hangve, tenHK));
                }
            }
            DataProvider.sqlConnection.Close();
            lvTicket.ItemsSource = list_ticker;
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy thanh toán?","",MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn thanh toán?", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DataProvider.sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(
                    "update [VE] set TinhTrang = 'SOLD' " +
                    "where MaVe in ( select [v].MaVe from [CTHD] [ct], [VE] [v] " +
                                        "where [v].MaVe = [ct].MaVe and [ct].MaHD = @mahd)", DataProvider.sqlConnection);
                sqlCommand.Parameters.Add("@mahd", SqlDbType.NVarChar).Value = MaHD;
                sqlCommand.ExecuteNonQuery();
                DataProvider.sqlConnection.Close();

                DataProvider.sqlConnection.Open();
                sqlCommand = new SqlCommand(
                    "update [HOADON] set TinhTrang = 'PAID' " +
                    "where MaHD = @mahd", DataProvider.sqlConnection);
                sqlCommand.Parameters.Add("@mahd", SqlDbType.NVarChar).Value = MaHD;
                sqlCommand.ExecuteNonQuery();
                DataProvider.sqlConnection.Close();

                MessageBox.Show("Thanh toán hóa đơn thành công!");

                this.Close();
            }
        }
    }
}