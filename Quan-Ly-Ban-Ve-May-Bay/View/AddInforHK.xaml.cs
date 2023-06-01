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
    /// <summary>
    /// Interaction logic for AddInforHK.xaml
    /// </summary>
    public partial class AddInforHK : Window
    {
        ObservableCollection<Ve> ve = new ObservableCollection<Ve>();
        public AddInforHK()
        {
            InitializeComponent();
            veLV.ItemsSource = ve;

            maVeBox.Items.Add("9");
            maVeBox.Items.Add("22");
            maVeBox.Items.Add("30");


            maVeBox.SelectedIndex = 0;
            ngaylapHDTxt.Text = DateTime.Now.ToString();

            Random r = new Random();
            maHoaDonTxt.Text = r.Next(100000, 9999999).ToString();

            addData();

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public class Ve
        {
            public string mave { get; set; }
            public string soghe { get; set; }
            public string mahangve { get; set; }
            public string mahk { get; set; }
            public string tenhk { get; set; }
            public string cccd { get; set; }
            public string sdt { get; set; }

        }
        private void btnXacNhanVe_Click(object sender, RoutedEventArgs e)
        {
            if (tenHanhKhachTxt.Text == "" || cmndTxt.Text == "" || sdtTxt.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hành khách!");
            }
            else
            {
                if (ve.Count == 0)
                {
                    ve.Add(new Ve() { mave = maVeBox.SelectedItem.ToString(), soghe = soGheTxt.Text, mahangve = maHangVeTxt.Text, mahk = maHanhKhachText.Text, tenhk = tenHanhKhachTxt.Text, cccd = cmndTxt.Text, sdt = sdtTxt.Text });
                }
                else
                {
                    for (int i = 0; i < ve.Count; i++)
                    {
                        if (ve[i].mave == maVeBox.SelectedItem)
                        {
                            ve.Remove(ve[i]);
                            ve.Insert(i, new Ve() { mave = maVeBox.SelectedItem.ToString(), soghe = soGheTxt.Text, mahangve = maHangVeTxt.Text, mahk = maHanhKhachText.Text, tenhk = tenHanhKhachTxt.Text, cccd = cmndTxt.Text, sdt = sdtTxt.Text });
                        }
                        else
                        {
                            ve.Add(new Ve() { mave = maVeBox.SelectedItem.ToString(), soghe = soGheTxt.Text, mahangve = maHangVeTxt.Text, mahk = maHanhKhachText.Text, tenhk = tenHanhKhachTxt.Text, cccd = cmndTxt.Text, sdt = sdtTxt.Text });
                        }
                    }
                }
            }

        }

        private void maVeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tenHanhKhachTxt.Text = "";
            cmndTxt.Text = "";
            sdtTxt.Text = "";
            for (int i = 0; i < ve.Count; i++)
            {
                if (ve[i].mave == maVeBox.SelectedItem)
                {
                    tenHanhKhachTxt.Text = ve[i].tenhk;
                    cmndTxt.Text = ve[i].cccd;
                    sdtTxt.Text = ve[i].sdt;
                }
            }
        }

        private void veLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ve item_ve = veLV.SelectedItem as Ve;
            if (item_ve != null)
            {
                tenHanhKhachTxt.Text = item_ve.tenhk;
                cmndTxt.Text = item_ve.cccd;
                sdtTxt.Text = item_ve.sdt;
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

        private void addData()
        {
            String stringcm = "select SanBayDi, SanBayDen, NgayKhoiHanh, ThoiGianXuatPhat from [CHUYENBAY] [c] where [c].MaChuyenBay = " + maChuyenBayTxt.Text;
            SqlCommand sqlCommand = new SqlCommand(stringcm, DataProvider.sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            tuTxt.Text = ds.Tables[0].Rows[0][0].ToString();
            denTxt.Text = ds.Tables[0].Rows[0][1].ToString();
            ngayGiotxt.Text = ds.Tables[0].Rows[0][2].ToString() + " " + ds.Tables[0].Rows[0][3].ToString();
        }

        private void btnTTSau_Click(object sender, RoutedEventArgs e)
        {
            if (ve.Count < maVeBox.Items.Count)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin vé!");
            }
            else
            {
                String stringcm;
                String mahk, tenhk, cccd, sdt, tinhtrang;

                for (int i = 0; i < ve.Count; i++)
                {
                    stringcm = "update [VE] set MaHK = " + ve[i].mahk + ", TenHK = " + ve[i].tenhk +
                        ", SDT = " + ve[i].sdt + ", CMND = " + ve[i].cccd + ", TinhTrang = 'DaBan' where MaVe = " + ve[i].mave;
                    SqlCommand sqlCommand = new SqlCommand(stringcm, DataProvider.sqlConnection);
                }

                stringcm = "insert [HOADON] values (" + maHoaDonTxt.Text + ", " + ngaylapHDTxt.Text + ", " + tienTxt.Text + ", 'ChuaTT', "
                    + maTKTTTxt.Text + ")";

                MessageBox.Show("Đặt vé thành công! \nVui lòng thanh toán hóa đơn trước khi chuyến bay xuất phát! \n" +
                    "Phiếu đặt chỗ sẽ bị hủy nếu không được thanh toán trước giờ bay!");
            }
        }

        private void btnTTNgay_Click(object sender, RoutedEventArgs e)
        {
            if (ve.Count < maVeBox.Items.Count)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin vé!");
            }
            else
            {
                String stringcm;
                String mahk, tenhk, cccd, sdt, tinhtrang;

                for (int i = 0; i < ve.Count; i++)
                {
                    stringcm = "update [VE] set MaHK = " + ve[i].mahk + ", TenHK = " + ve[i].tenhk +
                        ", SDT = " + ve[i].sdt + ", CMND = " + ve[i].cccd + ", TinhTrang = 'DaBan' where MaVe = " + ve[i].mave;
                    SqlCommand sqlCommand = new SqlCommand(stringcm, DataProvider.sqlConnection);
                }

                stringcm = "insert [HOADON] values (" + maHoaDonTxt.Text + ", " + ngaylapHDTxt.Text + ", " + tienTxt.Text
                    + ", 'DaTT', " + maTKTTTxt.Text + ")";

                MessageBox.Show("Thanh toán vé thành công!");
            }
        }
    }
}
