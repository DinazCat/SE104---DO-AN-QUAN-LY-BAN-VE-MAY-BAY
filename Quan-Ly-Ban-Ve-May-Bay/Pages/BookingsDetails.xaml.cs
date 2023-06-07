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
using Quan_Ly_Ban_Ve_May_Bay.View;
using Quan_Ly_Ban_Ve_May_Bay.Model;
using System.Data.SqlClient;
using System.Data;

namespace Quan_Ly_Ban_Ve_May_Bay.Pages
{
    /// <summary>
    /// Interaction logic for BookingsDetail.xaml
    /// </summary>
    public partial class BookingsDetail : Page
    {
        private string MaVe;
        public BookingsDetail()
        {
            InitializeComponent();
        }

        public void ShowDetail(string mave, string user_id)
        {
            MaVe = mave;
            DataProvider.sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(
                "select [cb].MaChuyenBay, [cb].SanBayDi, [cb].SanBayDen, [cb].NgayKhoiHanh, [cb].ThoiGianXuatPhat, [cb].ThoiGIanDuKien, " +
                        "[v].MaVe, [v].SoGhe, [v].TenHK, [v].MaHK, [v].CMND, [v].SDT, " +
                        "[hv].TenHangVe, " +
                        "[hd].MaHD, [hd].NgayLap, [hd].ThanhTien, [hd].TinhTrang, [hd].MaTK, " +
                        "[s1].TenSanBay sbDi, [s2].TenSanBay sbDen " +
                "from [CHUYENBAY] [cb], [VE] [v], [HANGVE] [hv], [HOADON] [hd], [CTHD] [ct], [SANBAY] [s1], [SANBAY] [s2] " +
                "where  [hd].MaHD = [ct].MaHD and " +
                        "[v].MaVe = [ct].MaVe and " +
                        "[v].MaChuyenBay = [cb].MaChuyenBay and " +
                        "[hv].MaHangVe = [v].MaHangVe and " +
                        "[s1].MaSanBay = [cb].SanBayDi and " +
                        "[s2].MaSanBay = [cb].SanBayDen and " +
                        "[v].MaVe = @mave ", DataProvider.sqlConnection);
            sqlCommand.Parameters.Add("@mave", SqlDbType.NVarChar).Value = mave;
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    maCBTxt.Text = reader["MaChuyenBay"].ToString();
                    tuTxt.Text = reader["sbDi"].ToString();
                    denTxt.Text = reader["sbDen"].ToString();
                    gioBayTxt.Text = reader["NgayKhoiHanh"].ToString() + " " + reader["ThoiGianXuatPhat"].ToString();
                    tgBayTxt.Text = reader["ThoiGianDuKien"].ToString() + " phút";

                    maVeTxt.Text = mave;
                    soGheTxt.Text = reader["SoGhe"].ToString();
                    tenHangVeTxt.Text = reader["TenHangVe"].ToString();

                    tenHKTxt.Text = reader["TenHK"].ToString();
                    maHKTxt.Text = reader["MaHK"].ToString();
                    cmndTxt.Text = reader["CMND"].ToString();
                    sdtTxt.Text = reader["SDT"].ToString();

                    maHDTxt.Text = reader["MaHD"].ToString();
                    ngayLapTxt.Text = reader["NgayLap"].ToString();
                    tienTxt.Text = reader["ThanhTien"].ToString();
                    tinhTrangTxt.Text = reader["TinhTrang"].ToString();
                    maTKTxt.Text = reader["MaTK"].ToString();
                }
            }
            DataProvider.sqlConnection.Close();

            if (tinhTrangTxt.Text == "UNPAID")
            {
                if (MainWindow.curAccount.type == 1 || MainWindow.curAccount.type == 2)
                {
                    btnPay.Content = "Đã thanh toán";
                }
                btnPay.Visibility = Visibility.Visible;
                statusPaymentTxt.Visibility = Visibility.Visible; 
            }
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            BookingsPay bookingsPay = new BookingsPay();
            bookingsPay.ShowBill(maHDTxt.Text, ngayLapTxt.Text, maCBTxt.Text, gioBayTxt.Text, tienTxt.Text);
            bookingsPay.ShowDialog();
        }
    }
}