using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Data;
using Quan_Ly_Ban_Ve_May_Bay.Model;

namespace Quan_Ly_Ban_Ve_May_Bay.View
{
    public partial class AddInforHK : Window
    {
        List<Ticket> ve = new List<Ticket>();
        List<string> list_mave = new List<string>();
        DateTime ngayHD = new DateTime();

        public AddInforHK()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnXacNhanVe_Click(object sender, RoutedEventArgs e)
        {
            if (tenHanhKhachTxt.Text == "" || cmndTxt.Text == "" || sdtTxt.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hành khách!");
            }
            else
            {
                foreach (Ticket item in ve)
                {
                    if (item.TiketID == maVeBox.SelectedItem.ToString())
                    {
                        item.HkID = maHanhKhachText.Text;
                        item.HkName = tenHanhKhachTxt.Text;
                        item.CMND = cmndTxt.Text;
                        item.PhoneNumber = sdtTxt.Text;
                    }
                }
            }
        }

        private void TienVe()
        {
            int tien = 0;
            foreach (Ticket item in ve)
            {
                DataProvider.sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(
                    "select [c].Gia from [CHUYENBAY] [c] where [c].MaChuyenBay = @macb",
                    DataProvider.sqlConnection);
                sqlCommand.Parameters.Add("@macb", SqlDbType.NVarChar).Value = maChuyenBayTxt.Text;
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        int dongia = int.Parse(reader["Gia"].ToString());
                        if (item.FlightClass != "HV229365")
                        {
                            tien += (dongia * 105) / 100;
                        }
                        else
                            tien += dongia;
                    }
                }
                DataProvider.sqlConnection.Close();
            }
            tienTxt.Text = tien.ToString();
        }
        private void maVeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tenHanhKhachTxt.Clear();
            cmndTxt.Clear();
            sdtTxt.Clear();

            foreach (Ticket item in ve)
            {
                if (item.TiketID == maVeBox.SelectedItem.ToString())
                {
                    tenHanhKhachTxt.Text = item.HkName;
                    cmndTxt.Text = item.CMND;
                    sdtTxt.Text = item.PhoneNumber;
                    soGheTxt.Text = item.SeatNumber.ToString();
                    maHangVeTxt.Text = item.FlightClass;
                }
            }
        }

        private void NewHD_NewHK()
        {
            DataProvider.sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("select [h].* from HOADON [h] order by MaHD desc", DataProvider.sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    int mahd = int.Parse(reader["MaHD"].ToString()) + 1;
                    maHoaDonTxt.Text = mahd.ToString();
                }
            }
            else maHoaDonTxt.Text = "100";
            DataProvider.sqlConnection.Close();

            DataProvider.sqlConnection.Open();
            sqlCommand = new SqlCommand("select [v].* from VE [v] order by MaHK desc", DataProvider.sqlConnection);
            reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                if (reader["MaHK"].ToString() != "")
                {
                    int mahk = int.Parse(reader["MaHK"].ToString()) + 1;
                    maHanhKhachText.Text = mahk.ToString();
                }
                else maHanhKhachText.Text = "100";
            }
            DataProvider.sqlConnection.Close();
        }
        private void veLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ticket item = veLV.SelectedItem as Ticket;
            if (item != null)
            {
                tenHanhKhachTxt.Text = item.HkName;
                cmndTxt.Text = item.CMND;
                sdtTxt.Text = item.PhoneNumber;
                soGheTxt.Text = item.SeatNumber.ToString();
                maHangVeTxt.Text = item.FlightClass;
                maVeBox.SelectedItem = item.TiketID;
            }
        }

        public void Show(string macb, List<string> list, string userID)
        {
            NewHD_NewHK();
            ngayHD = DateTime.Now;
            ngaylapHDTxt.Text = ngayHD.ToString("dd/MM/yyyy h:mm");
            maTKTTTxt.Text = userID;
            maVeBox.ItemsSource = list;
            maChuyenBayTxt.Text = macb;

            foreach (string mave in list)
            {
                DataProvider.sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(" select [v].* from [VE] [v] where [v].MaVe = @mave",
                    DataProvider.sqlConnection);
                sqlCommand.Parameters.Add("@mave", SqlDbType.NVarChar).Value = mave;
                SqlDataReader reader = sqlCommand.ExecuteReader();
                Ticket ticket = new Ticket();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        string mv = reader["MaVe"].ToString();
                        int sg = int.Parse(reader["SoGhe"].ToString());
                        string mhv = reader["MaHangVe"].ToString();
                        string tt = reader["TinhTrang"].ToString();
                        string mhk = reader["MaHK"].ToString();
                        string thk = reader["TenHK"].ToString();
                        string cmnd = reader["CMND"].ToString();
                        string sdt = reader["SDT"].ToString();
                        ticket = new Ticket(mv, mhv, sg, tt, mhk, thk, cmnd, sdt);
                    }
                }
                ve.Add(ticket);
                DataProvider.sqlConnection.Close();
            }
            veLV.ItemsSource = ve;


            DataProvider.sqlConnection.Open();
            SqlCommand sqlcommand = new SqlCommand(
                " select [c].SanBayDi, [c].SanBayDen, [c].NgayKhoiHanh, [c].ThoiGianXuatPhat " +
                "from [CHUYENBAY] [c] where [c].MaChuyenBay = @macb",
                DataProvider.sqlConnection);
            sqlcommand.Parameters.Add("@macb", SqlDbType.NVarChar).Value = macb;
            SqlDataReader rd = sqlcommand.ExecuteReader();
            if (rd.HasRows)
            {
                if (rd.Read())
                {
                    tuTxt.Text = rd["SanBayDi"].ToString();
                    denTxt.Text = rd["SanBayDen"].ToString();
                    ngayGiotxt.Text = rd["ThoiGianXuatPhat"].ToString() + " " + rd["NgayKhoiHanh"].ToString();
                }
            }            
            DataProvider.sqlConnection.Close();

            maVeBox.SelectedIndex = 0;
            TienVe();
        }

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tenHanhKhachTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private bool CheckInfor()
        {
            foreach (Ticket item in ve)
            {
                if (item.HkID == "")
                    return false;
            }
            return true;
        }

        private void btnTTSau_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckInfor())
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin vé!");
            }
            else
            {
                DataProvider.sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand sqlCommand = new SqlCommand(
                    "insert into [HOADON] values (@mahd, @ngay, @tien, 'UNPAID', @matk)", DataProvider.sqlConnection);
                sqlCommand.Parameters.Add("@mahd", SqlDbType.NVarChar).Value = maHoaDonTxt.Text;
                sqlCommand.Parameters.Add("@ngay", SqlDbType.DateTime).Value = ngayHD;
                sqlCommand.Parameters.Add("@tien", SqlDbType.Int).Value = int.Parse(tienTxt.Text.ToString());
                sqlCommand.Parameters.Add("@matk", SqlDbType.NVarChar).Value = maTKTTTxt.Text;
                sqlCommand.ExecuteNonQuery();
                DataProvider.sqlConnection.Close();

                SaveVe("BOOKED",maHoaDonTxt.Text);
                MessageBox.Show("Đặt vé thành công! \nVui lòng thanh toán hóa đơn trước khi chuyến bay xuất phát! \n" +
                    "Phiếu đặt chỗ sẽ bị hủy nếu không được thanh toán trước giờ bay!");
                Finish();
            }
        }

        private void btnTTNgay_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckInfor())
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin vé!");
            }
            else
            {
                DataProvider.sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand sqlCommand = new SqlCommand(
                    "insert into [HOADON] values (@mahd, @ngay, @tien, 'PAID', @matk)", DataProvider.sqlConnection);
                sqlCommand.Parameters.Add("@mahd", SqlDbType.NVarChar).Value = maHoaDonTxt.Text;
                sqlCommand.Parameters.Add("@ngay", SqlDbType.DateTime).Value = ngayHD;
                sqlCommand.Parameters.Add("@tien", SqlDbType.Int).Value = int.Parse(tienTxt.Text.ToString());
                sqlCommand.Parameters.Add("@matk", SqlDbType.NVarChar).Value = maTKTTTxt.Text;
                sqlCommand.ExecuteNonQuery();
                DataProvider.sqlConnection.Close();
                SaveVe("SOLD",maHoaDonTxt.Text);
                MessageBox.Show("Thanh toán vé thành công!");
                Finish();
            }
        }

        private void SaveVe(String tinhtrang, string mahd)
        {
            foreach (Ticket item in ve)
            {
                DataProvider.sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(
                    "update [VE] set MaHK = @mahk, TenHK = @tenhk, CMND = @cmnd, SDT = @sdt, TinhTrang = @tt " +
                    "where MaVe = @mave", DataProvider.sqlConnection);
                sqlCommand.Parameters.Add("@mahk", SqlDbType.NVarChar).Value = item.HkID;
                sqlCommand.Parameters.Add("@tenhk", SqlDbType.NVarChar).Value = item.HkName;
                sqlCommand.Parameters.Add("@cmnd", SqlDbType.NVarChar).Value = item.CMND;
                sqlCommand.Parameters.Add("@sdt", SqlDbType.NVarChar).Value = item.PhoneNumber;
                sqlCommand.Parameters.Add("@mave", SqlDbType.NVarChar).Value = item.TiketID;
                sqlCommand.Parameters.Add("@tt", SqlDbType.NVarChar).Value = tinhtrang;
                sqlCommand.ExecuteNonQuery();
                DataProvider.sqlConnection.Close();

                DataProvider.sqlConnection.Open();
                SqlCommand sql = new SqlCommand(
                    "insert into [CTHD] values(@macthd, @mahd, @mave)", DataProvider.sqlConnection);
                sql.Parameters.Add("@macthd", SqlDbType.NVarChar).Value = item.TiketID;
                sql.Parameters.Add("@mahd", SqlDbType.NVarChar).Value = mahd;
                sql.Parameters.Add("@mave", SqlDbType.NVarChar).Value = item.TiketID;
                sql.ExecuteNonQuery();
                DataProvider.sqlConnection.Close();
            }
        }

        private void Finish()
        {
            this.Close();            
        }
    }
}