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
        private BookingsUpdate bookingsUpdate;
        private string MaVe;
        public BookingsDetail()
        {
            InitializeComponent();
        }

        //private void ReshowDetail(object sender, RoutedEventArgs e)
        //{
        //    ShowDetail(MaVe);
        //}

        public void ShowDetail(string mave)
        {
            MaVe = mave;
            DataProvider.sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(
                "select [cb].MaChuyenBay, [cb].SanBayDi, [cb].SanBayDen, [cb].NgayKhoiHanh, [cb].ThoiGianXuatPhat, [cb].ThoiGIanDuKien, " +
                        "[v].MaVe, [v].SoGhe, [v].TenHK, [v].MaHK, [v].CMND, [v].SDT, " +
                        "[hv].TenHangVe, " +
                        "[hd].MaHD, [hd].NgayLap, [hd].ThanhTien, [hd].TinhTrang, [hd].MaTK " +
                "from [CHUYENBAY] [cb], [VE] [v], [HANGVE] [hv], [HOADON] [hd], [CTHD] [ct] " +
                "where  [hd].MaHD = [ct].MaHD and " +
                        "[v].MaVe = [ct].MaVe and " +
                        "[v].MaChuyenBay = [cb].MaChuyenBay and " +
                        "[hv].MaHangVe = [v].MaHangVe and " +
                        "[v].MaVe = @mave ", DataProvider.sqlConnection);
            sqlCommand.Parameters.Add("@mave", SqlDbType.NVarChar).Value = mave;
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    maCBTxt.Text = reader["MaChuyenBay"].ToString();
                    tuTxt.Text = reader["SanBayDi"].ToString();
                    denTxt.Text = reader["SanBayDen"].ToString();
                    gioBayTxt.Text = reader["ThoiGianXuatPhat"].ToString() + " " + reader["NgayKhoiHanh"].ToString();
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
            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            bookingsUpdate = new BookingsUpdate();
            bookingsUpdate.ShowInforHK(MaVe);            
            bookingsUpdate.ShowDialog();
        }

    }
}
