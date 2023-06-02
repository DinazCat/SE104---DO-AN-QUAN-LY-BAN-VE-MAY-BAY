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
        private string machuyenbay= "N'QH001'";

        public AddInforHK()
        {
            InitializeComponent();

            list_mave.Add("9");
            maVeBox.ItemsSource = list_mave;

            Show(list_mave, machuyenbay);
            veLV.ItemsSource = ve; 
            maVeBox.SelectedIndex = 0;
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
                    "select [c].Gia from [CHUYENBAY] [c] where [c].MaChuyenBay = " + maChuyenBayTxt.Text, 
                    DataProvider.sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        int dongia = int.Parse(reader["Gia"].ToString());
                        if (item.FlightClass == "HV229365")
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

            foreach(Ticket item in ve)
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

        private void Show(List<string> list, string macb)
        {
            ngaylapHDTxt.Text = DateTime.Now.ToString("dd/mm/yyyy h:mm");
            Random r = new Random();
            maHoaDonTxt.Text = r.Next(100000, 9999999).ToString();
            maHanhKhachText.Text = r.Next(10000,99999999).ToString();

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
                        
            SqlCommand sqlcommand = new SqlCommand(
                "select [c].SanBayDi, [c].SanBayDen, [c].NgayKhoiHanh, [c].ThoiGianXuatPhat " +
                "from [CHUYENBAY] [c] where [c].MaChuyenBay = " + maChuyenBayTxt.Text,
                DataProvider.sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlcommand);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            tuTxt.Text = ds.Tables[0].Rows[0][0].ToString();
            denTxt.Text = ds.Tables[0].Rows[0][1].ToString();
            ngayGiotxt.Text = ds.Tables[0].Rows[0][2].ToString() + " " + ds.Tables[0].Rows[0][3].ToString();

            TienVe();
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

        private void cmndTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void sdtTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
            foreach(Ticket item in ve)
            {
                if (item.HkID == "" ) 
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
                sqlCommand.Parameters.Add("@ngay", SqlDbType.DateTime).Value = ngaylapHDTxt.Text;
                sqlCommand.Parameters.Add("@tien", SqlDbType.Int).Value = int.Parse(tienTxt.Text.ToString());
                sqlCommand.Parameters.Add("@matk", SqlDbType.NVarChar).Value = maTKTTTxt.Text;
                sqlCommand.ExecuteNonQuery();
                DataProvider.sqlConnection.Close();

                SaveVe();
                MessageBox.Show("Đặt vé thành công! \nVui lòng thanh toán hóa đơn trước khi chuyến bay xuất phát! \n" +
                    "Phiếu đặt chỗ sẽ bị hủy nếu không được thanh toán trước giờ bay!");
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
                sqlCommand.Parameters.Add("@ngay", SqlDbType.DateTime).Value = ngaylapHDTxt.Text;
                sqlCommand.Parameters.Add("@tien", SqlDbType.Int).Value = int.Parse(tienTxt.Text.ToString());
                sqlCommand.Parameters.Add("@matk", SqlDbType.NVarChar).Value = maTKTTTxt.Text;
                sqlCommand.ExecuteNonQuery();
                DataProvider.sqlConnection.Close();
                SaveVe();
                MessageBox.Show("Thanh toán vé thành công!");
            }
        }

        private void SaveVe()
        {
            foreach (Ticket item in ve)
            {
                DataProvider.sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand sqlCommand = new SqlCommand(
                    "update [VE] set MaHK = @mahk, TenHK = @tenhk, CMND = @cmnd, SDT = @sdt, TinhTrang = 'SOLD' " +
                    "where MaVe = @mave", DataProvider.sqlConnection);
                sqlCommand.Parameters.Add("@mahk", SqlDbType.NVarChar).Value = item.HkID;
                sqlCommand.Parameters.Add("@tenhk", SqlDbType.NVarChar).Value = item.HkName;
                sqlCommand.Parameters.Add("@cmnd", SqlDbType.NVarChar).Value = item.CMND;
                sqlCommand.Parameters.Add("@sdt", SqlDbType.NVarChar).Value = item.PhoneNumber;
                sqlCommand.Parameters.Add("@mave", SqlDbType.NVarChar).Value = item.TiketID;
                sqlCommand.ExecuteNonQuery();
                DataProvider.sqlConnection.Close();
            }
        }
    }
}
