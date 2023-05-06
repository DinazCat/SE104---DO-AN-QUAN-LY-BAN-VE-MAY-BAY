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
using System.Windows.Shapes;
using static Quan_Ly_Ban_Ve_May_Bay.UserControls.Chuyenbay;
using Quan_Ly_Ban_Ve_May_Bay.UserControls;
using System.Globalization;

namespace Quan_Ly_Ban_Ve_May_Bay.View
{
    /// <summary>
    /// Interaction logic for AddChuyenbay.xaml
    /// </summary>
    public partial class AddChuyenbay : Window
    {
        DataGrid chuyenbayTable;
        int thaotac;
        public AddChuyenbay(DataGrid chuyenbayTable, int thaotac)
        {
            InitializeComponent();
            this.chuyenbayTable = chuyenbayTable;
            this.thaotac = thaotac;

            string query = "SELECT * FROM SANBAY";
            SqlParameter param1 = new SqlParameter("", "");
            DataTable dt;
            using (SqlDataReader reader = DataProvider.ExecuteReader(query, CommandType.Text, param1))
            {
                dt = new DataTable();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
            }
            foreach (DataRow dr in dt.Rows)
            {
                FromcBox.Items.Add(dr["TenSanBay"].ToString());
                TocBox.Items.Add(dr["TenSanBay"].ToString());
            }
            FromcBox.SelectedIndex = 0;

            if (thaotac == 1)
            {
                query = "SELECT * FROM CHUYENBAY where MaChuyenBay = @macb";
                param1 = new SqlParameter("@macb", Chuyenbay.chuyenbaytofix.maCB);
                using (SqlDataReader reader = DataProvider.ExecuteReader(query, CommandType.Text, param1))
                {
                    dt = new DataTable();
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                }
                string query1 = "SELECT * FROM SANBAYTRUNGGIAN WHERE MaChuyenBay=@macb";
                SqlParameter param2 = new SqlParameter("@macb", Chuyenbay.chuyenbaytofix.maCB);
                DataTable dt2;
                using (SqlDataReader reader = DataProvider.ExecuteReader(query1, CommandType.Text, param2))
                {
                    dt2 = new DataTable();
                    if (reader.HasRows)
                    {
                        dt2.Load(reader);
                    }
                }
                int stt2 = 1;
                foreach (DataRow dr in dt2.Rows)
                {

                    SanbayTG sb = new SanbayTG();
                    sb.STT = stt2.ToString();
                    sb.tenSB = dr["SanBayTrungGian"].ToString();
                    sb.TGdung = dr["ThoiGianDung"].ToString();
                    sb.ghichu = dr["GhiChu"].ToString();
                    sb.TGden = dr["ThoiGianDen"].ToString();
                    SBTGTable.Items.Add(sb);
                    stt2++;
                }
                int stt = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    SLH1Txb.Text = dr["SLGheHang1"].ToString();
                    SLH2Txb.Text = dr["SLGheHang2"].ToString();
                    string[] thoigian = dr["NgayKhoiHanh"].ToString().Split('.');
                    int nam = int.Parse(thoigian[2]);
                    int thang = int.Parse(thoigian[1]);
                    int ngay = int.Parse(thoigian[0]);
                    Ngaypicker.SelectedDate = new DateTime(nam, thang, ngay);
                    gioTxb.Text = dr["ThoiGianXuatPhat"].ToString();
                    mamaybayTxb.Text = dr["MaMayBay"].ToString();
                }
                machuyenbayTxb.Text = Chuyenbay.chuyenbaytofix.maCB;
                FromcBox.SelectedItem = Chuyenbay.chuyenbaytofix.SBdi;
                TocBox.SelectedItem = Chuyenbay.chuyenbaytofix.SBden;
                TGbayTxb.Text = Chuyenbay.chuyenbaytofix.tgBay;
                GiaTxb.Text = Chuyenbay.chuyenbaytofix.Gia;

            }
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        AddSBTG addSBTG;
        private void AddSBTG_Click(object sender, RoutedEventArgs e)
        {
            addSBTG = new AddSBTG(SBTGTable, machuyenbayTxb.Text, 0);
            addSBTG.Show();
        }
        public void loadDatatoSBTG(string MaCB)
        {
            string query = "SELECT * FROM SANBAYTRUNGGIAN WHERE MaChuyenBay=@macb";
            SqlParameter param1 = new SqlParameter("@macb", MaCB);
            DataTable dt;
            using (SqlDataReader reader = DataProvider.ExecuteReader(query, CommandType.Text, param1))
            {
                dt = new DataTable();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
            }
            int stt = 1;
            foreach (DataRow dr in dt.Rows)
            {

                SanbayTG sb = new SanbayTG();
                sb.STT = stt.ToString();
                sb.tenSB = dr["SanBayTrungGian"].ToString();
                sb.TGdung = dr["ThoiGianDung"].ToString();
                sb.ghichu = dr["GhiChu"].ToString();
                sb.TGden = dr["ThoiGianDen"].ToString();
                SBTGTable.Items.Add(sb);
                stt++;
            }
        }
        public string MaCB, MaMB, Sanbaydi, Sanbayden, Ngay, Gio, TgBay, Gia, SLH1, SLH2;
        public static SanbayTG infotofix;
        private void SuaSBTG_Click(object sender, RoutedEventArgs e)
        {
            infotofix = SBTGTable.SelectedItem as SanbayTG;
            if (infotofix != null)
            {
                addSBTG = new AddSBTG(SBTGTable, machuyenbayTxb.Text, 1);
                addSBTG.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng bạn muốn sửa");
            }

        }

        private void XoaSBTG_Click(object sender, RoutedEventArgs e)
        {
            SanbayTG info = SBTGTable.SelectedItem as SanbayTG;
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from SANBAYTRUNGGIAN where SanBayTrungGian=N'" + info.tenSB + "' and MaChuyenBay=N'" + machuyenbayTxb.Text + "'", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteReader();
            con.Close();
            SBTGTable.Items.Clear();
            loadDatatoSBTG(machuyenbayTxb.Text);
        }
        public void loadDatatoTable()
        {
            string query = "SELECT * FROM CHUYENBAY";
            SqlParameter param1 = new SqlParameter("", "");
            DataTable dt;
            using (SqlDataReader reader = DataProvider.ExecuteReader(query, CommandType.Text, param1))
            {
                dt = new DataTable();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
            }
            int stt = 1;
            foreach (DataRow dr in dt.Rows)
            {

                chuyenbayclass cb = new chuyenbayclass();
                cb.STT = stt.ToString();
                cb.maCB = dr["MaChuyenBay"].ToString();
                cb.SBdi = dr["SanBayDi"].ToString();
                cb.SBden = dr["SanBayDen"].ToString();
                cb.datetime = dr["NgayKhoiHanh"].ToString() + dr["ThoiGianXuatPhat"].ToString();
                cb.tgBay = dr["ThoiGianDuKien"].ToString();
                cb.Gia = dr["GiaVe"].ToString();
                chuyenbayTable.Items.Add(cb);
                stt++;
            }
        }
        chuyenbayclass cb;
        private void Hoantat_Click(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDate = Ngaypicker.SelectedDate;
            if (selectedDate.HasValue)
            {
                Ngay = selectedDate.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            MaCB = machuyenbayTxb.Text;
            MaMB = mamaybayTxb.Text;
            Sanbaydi = FromcBox.Text;
            Sanbayden = TocBox.Text;
            Gio = gioTxb.Text;
            TgBay = TGbayTxb.Text;
            Gia = GiaTxb.Text;
            SLH1 = SLH1Txb.Text;
            SLH2 = SLH2Txb.Text;
            if (thaotac == 0)
            {
                string query = "SELECT * From CHUYENBAY";
                SqlParameter param1 = new SqlParameter("", "");
                DataTable dt;
                using (SqlDataReader reader = DataProvider.ExecuteReader(query, CommandType.Text, param1))
                {
                    dt = new DataTable();
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                }
                cb = new chuyenbayclass();
                cb.STT = (dt.Rows.Count + 1).ToString();
                cb.maCB = MaCB;
                cb.SBdi = Sanbaydi;
                cb.SBden = Sanbayden;
                cb.datetime = Ngay + "-" + Gio;
                cb.tgBay = TgBay;
                cb.Gia = Gia;
                chuyenbayTable.Items.Add(cb);
                SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into [CHUYENBAY] values('" + MaCB + "', N'" + MaMB + "',N'" + Sanbaydi + "',N'" + Sanbayden + "',N'" + Ngay + "',N'" + Gio + "',N'" + TgBay + "',N'" + SLH1 + "',N'" + SLH2 + "',N'" + Gia + "')", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                this.Close();
            }
            else
            {
                SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("Update [CHUYENBAY] set MaChuyenBay = '" + MaCB + "',MaMayBay = '" + MaMB + "' ,SanBayDi='" + Sanbaydi + "', SanBayDen='" + Sanbayden + "', NgayKhoiHanh='" + Ngay + "',ThoiGianXuatPhat='" + Gio + "', ThoiGianDuKien='" + TgBay + "', SLGheHang1='" + SLH1 + "', SLGheHang2='" + SLH2 + "', GiaVe='" + Gia + "' where MaChuyenBay='" + Chuyenbay.chuyenbaytofix.maCB + "'", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                chuyenbayTable.Items.Clear();
                loadDatatoTable();
                this.Close();
            }

        }
    }
}
