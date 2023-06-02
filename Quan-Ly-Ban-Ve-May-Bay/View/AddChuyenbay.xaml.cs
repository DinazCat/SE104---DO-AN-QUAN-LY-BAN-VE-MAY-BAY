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
using System.Drawing;

namespace Quan_Ly_Ban_Ve_May_Bay.View
{
    /// <summary>
    /// Interaction logic for AddChuyenbay.xaml
    /// </summary>
    public partial class AddChuyenbay : Window
    {
        DataGrid chuyenbayTable;
        int thaotac;
        List<QLHangVeClass> qLHangVeClass;
        public AddChuyenbay(DataGrid chuyenbayTable, int thaotac)
        {
            InitializeComponent();
            this.chuyenbayTable = chuyenbayTable;
            this.thaotac = thaotac;
            qLHangVeClass = new List<QLHangVeClass>();

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
            query = "SELECT * FROM HANGMAYBAY";
            param1 = new SqlParameter("", "");
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
                MaHcBox.Items.Add(dr["MaHang"].ToString());
            }
            if (thaotac == 0)
            {
                QLHangVeClass hv = new QLHangVeClass();
                qLHangVeClass.Add(hv);
                HangVeList.Items.Add(hv);
            }

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
                    SBTGTable.Items.Add(sb);
                    stt2++;
                }
                int stt = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    string[] thoigian = dr["NgayKhoiHanh"].ToString().Split('.');
                    int nam = int.Parse(thoigian[2]);
                    int thang = int.Parse(thoigian[1]);
                    int ngay = int.Parse(thoigian[0]);
                    Ngaypicker.SelectedDate = new DateTime(nam, thang, ngay);
                    gioTxb.Text = dr["ThoiGianXuatPhat"].ToString();
                    MaHcBox.SelectedItem = dr["MaHangMayBay"].ToString();
                    loaimaybayTxb.Text = dr["LoaiMayBay"].ToString();
                }
                machuyenbayTxb.Text = Chuyenbay.chuyenbaytofix.maCB;
                FromcBox.SelectedItem = Chuyenbay.chuyenbaytofix.SBdi;
                TocBox.SelectedItem = Chuyenbay.chuyenbaytofix.SBden;
                TGbayTxb.Text = Chuyenbay.chuyenbaytofix.tgBay;
                GiaTxb.Text = Chuyenbay.chuyenbaytofix.Gia;

                query1 = "SELECT * FROM QuanLyHangVeChuyenBay WHERE MaChuyenBay=@macb";
                param2 = new SqlParameter("@macb", Chuyenbay.chuyenbaytofix.maCB);
                using (SqlDataReader reader = DataProvider.ExecuteReader(query1, CommandType.Text, param2))
                {
                    dt2 = new DataTable();
                    if (reader.HasRows)
                    {
                        dt2.Load(reader);
                    }
                }
                foreach (DataRow dr in dt2.Rows)
                {
                    QLHangVeClass HV = new QLHangVeClass();
                    HV.Mahangve = dr["MaHangVe"].ToString();
                    HV.Machuyenbay = dr["MaChuyenBay"].ToString();
                    HV.Soluong = dr["SoLuong"].ToString();
                    HV.Gia = (int)dr["Gia"];
                    qLHangVeClass.Add(HV);
                    HangVeList.Items.Add(HV);
                }

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
                SBTGTable.Items.Add(sb);
                stt++;
            }
        }

        private void ThemHV_Click(object sender, RoutedEventArgs e)
        {
            QLHangVeClass hv = new QLHangVeClass();
            qLHangVeClass.Add(hv);
            HangVeList.Items.Add(hv);
        }
        private void XoaHV_Click(object sender, RoutedEventArgs e)
        {
            QLHangVeClass hv = (sender as Button).DataContext as QLHangVeClass;
            HangVeList.Items.Remove(hv);
            qLHangVeClass.Remove(hv);
        }

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
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9LVKB7T\SQLEXPRESS;Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True");
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
                cb.Gia = dr["Gia"].ToString();
                chuyenbayTable.Items.Add(cb);
                stt++;
            }
        }
        private void SaveHV_VE()
        {
            for (int i = 0; i < qLHangVeClass.Count; i++)
            {
                string query1 = "SELECT * FROM HANGVE WHERE MaHangVe=@ma";
                SqlParameter param2 = new SqlParameter("@ma", qLHangVeClass[i].Mahangve);
                DataTable dt1;
                using (SqlDataReader reader = DataProvider.ExecuteReader(query1, CommandType.Text, param2))
                {
                    dt1 = new DataTable();
                    if (reader.HasRows)
                    {
                        dt1.Load(reader);
                    }
                }
                foreach (DataRow dr in dt1.Rows)
                {
                    double phantram = int.Parse(dr["TyLe"].ToString()) / 100.0;
                    qLHangVeClass[i].Gia = (int)(int.Parse(Gia) * phantram);
                }
                qLHangVeClass[i].Machuyenbay = MaCB;
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9LVKB7T\SQLEXPRESS;Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True");
                con.Open();
                SqlCommand cmd1 = new SqlCommand("Insert into [QuanLyHangVeChuyenBay] values('" + MaCB + "', N'" + qLHangVeClass[i].Mahangve + "'," + qLHangVeClass[i].Gia + ",N'" + qLHangVeClass[i].Soluong + "')", con);
                cmd1.CommandType = CommandType.Text;
                cmd1.ExecuteNonQuery();
                con.Close();
                for (int j = 0; j < int.Parse(qLHangVeClass[i].Soluong); j++)
                {
                    string mave = j.ToString() + MaCB + qLHangVeClass[i].Mahangve;
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("Insert into [VE] values('" + mave + "', N'" + qLHangVeClass[i].Machuyenbay + "',N'" + "" + "',N'" + "" + "',N'" + "" + "',N'" + "" + "',N'" + "" + "',N'" + "còn trống" + "',N'" + qLHangVeClass[i].Mahangve + "')", con);
                    cmd2.CommandType = CommandType.Text;
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }

            }
        }
        public string MaCB, Sanbaydi, Sanbayden, Ngay, Gio, TgBay, Gia, mahangMB, loaiMB;
        chuyenbayclass cb;
        private void Hoantat_Click(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDate = Ngaypicker.SelectedDate;
            if (selectedDate.HasValue)
            {
                Ngay = selectedDate.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            MaCB = machuyenbayTxb.Text;
            Sanbaydi = FromcBox.Text;
            Sanbayden = TocBox.Text;
            Gio = gioTxb.Text;
            TgBay = TGbayTxb.Text;
            Gia = GiaTxb.Text;
            mahangMB = MaHcBox.Text;
            loaiMB = loaimaybayTxb.Text;
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
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9LVKB7T\SQLEXPRESS;Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into [CHUYENBAY] values('" + MaCB + "',N'" + Sanbaydi + "',N'" + Sanbayden + "',N'" + Ngay + "',N'" + Gio + "',N'" + TgBay + "',N'" + mahangMB + "',N'" + loaiMB + "',N'" + Gia + "')", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                SaveHV_VE();
                this.Close();
            }
            else
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9LVKB7T\SQLEXPRESS;Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True");
                con.Open();
                SqlCommand cmd2 = new SqlCommand("Delete from VE where  MaChuyenBay=N'" + Chuyenbay.chuyenbaytofix.maCB + "'", con);
                cmd2.CommandType = CommandType.Text;
                cmd2.ExecuteReader();
                con.Close();

                con.Open();
                SqlCommand cmd1 = new SqlCommand("Delete from QuanLyHangVeChuyenBay where  MaChuyenBay=N'" + Chuyenbay.chuyenbaytofix.maCB + "'", con);
                cmd1.CommandType = CommandType.Text;
                cmd1.ExecuteReader();
                con.Close();

                con.Open();
                SqlCommand cmd = new SqlCommand("Update [CHUYENBAY] set MaChuyenBay = '" + MaCB + "',SanBayDi='" + Sanbaydi + "', SanBayDen='" + Sanbayden + "', NgayKhoiHanh='" + Ngay + "',ThoiGianXuatPhat='" + Gio + "', ThoiGianDuKien='" + TgBay + "', MaHangMayBay='" + mahangMB + "', LoaiMayBay=N'" + loaiMB + "', Gia='" + Gia + "' where MaChuyenBay='" + Chuyenbay.chuyenbaytofix.maCB + "'", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();

                SaveHV_VE();
                chuyenbayTable.Items.Clear();
                loadDatatoTable();
                this.Close();
            }

        }
    }
}
