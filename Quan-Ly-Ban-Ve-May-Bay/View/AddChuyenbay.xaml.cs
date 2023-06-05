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
using Quan_Ly_Ban_Ve_May_Bay.ViewModel;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using Button = System.Windows.Controls.Button;
using DataGrid = System.Windows.Controls.DataGrid;
using System.Collections;

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
        List<QLHangVeClass> qLHangVeClassexist;
        Boolean isSave = false;
        public AddChuyenbay(DataGrid chuyenbayTable, int thaotac)
        {
            InitializeComponent();
            this.chuyenbayTable = chuyenbayTable;
            this.thaotac = thaotac;
            qLHangVeClass = new List<QLHangVeClass>();
            qLHangVeClassexist = new List<QLHangVeClass>();

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
                FromcBox.Items.Add(dr["MaSanBay"].ToString());
                TocBox.Items.Add(dr["MaSanBay"].ToString());
            }
            FromcBox.SelectedIndex = 0;
            TocBox.SelectedIndex = 0;
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
                    string[] thoigian = dr["NgayKhoiHanh"].ToString().Split('/');
                    int nam = int.Parse(thoigian[2]);
                    int thang = int.Parse(thoigian[1]);
                    int ngay = int.Parse(thoigian[0]);
                    Ngaypicker.SelectedDate = new DateTime(nam, thang, ngay);
                    gioTxb.Text = dr["ThoiGianXuatPhat"].ToString();
                    MaHcBox.SelectedItem = dr["MaHangMayBay"].ToString();
                    loaimaybayTxb.Text = dr["LoaiMayBay"].ToString();
                }
                machuyenbayTxb.Text = Chuyenbay.chuyenbaytofix.maCB;
                machuyenbayTxb.IsEnabled = false;
                FromcBox.Text = Chuyenbay.chuyenbaytofix.SBdi;
                TocBox.Text = Chuyenbay.chuyenbaytofix.SBden;
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
                    // qLHangVeClass.Add(HV);
                    qLHangVeClassexist.Add(HV);
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
            int SoSanBayTGtoida = 0;
            string q = "SELECT * FROM BANGTHAMSO";
            DataTable d;
            using (SqlDataReader reader = DataProvider.ExecuteReader(q, CommandType.Text))
            {
                d = new DataTable();
                if (reader.HasRows)
                {
                    d.Load(reader);
                    DataRow dr = d.Rows[1];
                    SoSanBayTGtoida = dr.Field<int>(1);
                }
            }
            int rowCount = SBTGTable.Items.Count;
            if (rowCount < SoSanBayTGtoida)
            {
                if (isSave == true)
                {
                    addSBTG = new AddSBTG(SBTGTable, machuyenbayTxb.Text, 0);
                    addSBTG.Show();
                }
                else
                {
                    MessageBox.Show("Vui lòng lưu thông tin chuyến bay trước. ", "Thông báo!");
                }

            }
            else
            {
                MessageBox.Show("Số sân bay trung gian đã đạt đến giới hạn quy định. ", "Thông báo!");
            }
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

            if (thaotac == 1 && hv.Mahangve != null)
            {
                string query = "SELECT * From VE where MaChuyenBay = @ma and MaHangVe = @mah";
                SqlParameter param1 = new SqlParameter("@ma", machuyenbayTxb.Text);
                SqlParameter param2 = new SqlParameter("@mah", hv.Mahangve);
                using (SqlDataReader reader = DataProvider.ExecuteReader(query, CommandType.Text, param1, param2))
                {
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Không thể xóa hạng vé này.", "Thông báo");
                        return;

                    }

                }
            }
            else
            {
                HangVeList.Items.Remove(hv);
                qLHangVeClass.Remove(hv);
            }

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
            SqlConnection con = DataProvider.sqlConnection;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
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
                cb.datetime = dr["NgayKhoiHanh"].ToString() + "-" + dr["ThoiGianXuatPhat"].ToString();
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
                int giave = 0;
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
                    giave = (int)(int.Parse(Gia) * phantram);
                }
                qLHangVeClass[i].Machuyenbay = MaCB;
                SqlConnection con = DataProvider.sqlConnection;
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd1 = new SqlCommand("Insert into [QuanLyHangVeChuyenBay] values('" + MaCB + "', N'" + qLHangVeClass[i].Mahangve + "',N'" + qLHangVeClass[i].Soluong + "')", con);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();
                    con.Close();

                    for (int j = 0; j < int.Parse(qLHangVeClass[i].Soluong); j++)
                    {
                        //string mave = j.ToString() + MaCB + qLHangVeClass[i].Mahangve;
                        Random rd = new Random();
                        int ID = rd.Next(100000, 999999);
                        string mave = ID.ToString() + j.ToString();
                        try
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            SqlCommand cmd2 = new SqlCommand("Insert into [VE] values('" + mave + "', N'" + qLHangVeClass[i].Machuyenbay + "',N'" + (j + 1).ToString() + "',N'" + "" + "',N'" + "" + "',N'" + "" + "',N'" + "" + "',N'" + "TRONG" + "',N'" + qLHangVeClass[i].Mahangve + "'," + giave + ")", con);
                            cmd2.CommandType = CommandType.Text;
                            cmd2.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            }
        }
        public string MaCB, Sanbaydi, Sanbayden, Ngay, Gio, TgBay, Gia, mahangMB, loaiMB;

        chuyenbayclass cb;
        void SaveCB()
        {
            DateTime? selectedDate = Ngaypicker.SelectedDate;
            if (selectedDate.HasValue)
            {
                Ngay = selectedDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            MaCB = machuyenbayTxb.Text;
            Sanbaydi = FromcBox.Text;
            Sanbayden = TocBox.Text;
            Gio = gioTxb.Text;
            TgBay = TGbayTxb.Text;
            Gia = GiaTxb.Text;
            mahangMB = MaHcBox.Text;
            loaiMB = loaimaybayTxb.Text;
            if (machuyenbayTxb.Text == "" || gioTxb.Text == "" || TGbayTxb.Text == "" || GiaTxb.Text == "" || MaHcBox.Text == "" || loaimaybayTxb.Text == "" || Ngay == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            int ThoiGianBayToiThieu = 0;
            string q = "SELECT * FROM BANGTHAMSO";
            DataTable d;
            using (SqlDataReader reader = DataProvider.ExecuteReader(q, CommandType.Text))
            {
                d = new DataTable();
                if (reader.HasRows)
                {
                    d.Load(reader);
                    DataRow dr = d.Rows[0];
                    ThoiGianBayToiThieu = dr.Field<int>(1);
                }
            }

            try
            {
                int p1 = int.Parse(Gia);
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập giá là một số nguyên.", "Dữ liệu không hợp lệ!");
                return;
            }
            try
            {
                int p2 = int.Parse(TgBay);
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập thời gian bay là một số nguyên.", "Dữ liệu không hợp lệ!");
                return;
            }
            if (int.Parse(TgBay) < ThoiGianBayToiThieu)
            {
                MessageBox.Show("Vui lòng nhập thời gian bay phải lớn hơn thời gian bay tối thiểu đã định. ", "Dữ liệu không hợp lệ!");
                return;

            }
            if (thaotac == 0)
            {
                string s = "SELECT * From CHUYENBAY WHERE MaChuyenBay = @ma";
                SqlParameter p = new SqlParameter("@ma", machuyenbayTxb.Text);
                using (SqlDataReader reader = DataProvider.ExecuteReader(s, CommandType.Text, p))
                {
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Mã chuyến bay đã tồn tại. ", "Dữ liệu không hợp lệ!");
                        return;
                    }
                }
            }
            if (thaotac == 0)
            {
                for (int i = 0; i < qLHangVeClass.Count - 1; i++)
                {
                    for (int j = i + 1; j < qLHangVeClass.Count; j++)
                    {
                        if (qLHangVeClass[i].Mahangve == qLHangVeClass[j].Mahangve)
                        {
                            MessageBox.Show("Xuất hiện tình trạng mã hạng vé trùng nhau. ", "Thông báo");
                            return;
                        }
                    }
                }
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
                SqlConnection con = DataProvider.sqlConnection;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Insert into [CHUYENBAY] values('" + MaCB + "',N'" + Sanbaydi + "',N'" + Sanbayden + "',N'" + Ngay + "',N'" + Gio + "',N'" + TgBay + "',N'" + mahangMB + "',N'" + loaiMB + "',N'" + Gia + "')", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                SaveHV_VE();
            }
            else
            {
                List<QLHangVeClass> mergedList = qLHangVeClass.Concat(qLHangVeClassexist).ToList();
                for (int i = 0; i < mergedList.Count - 1; i++)
                {
                    for (int j = i + 1; j < mergedList.Count; j++)
                    {
                        if (mergedList[i].Mahangve == mergedList[j].Mahangve)
                        {
                            MessageBox.Show("Xuất hiện tình trạng mã hạng vé trùng nhau. ", "Thông báo");
                            return;
                        }
                    }
                }
                SqlConnection con = DataProvider.sqlConnection;
                //con.Open();
                //SqlCommand cmd2 = new SqlCommand("Delete from VE where  MaChuyenBay=N'" + Chuyenbay.chuyenbaytofix.maCB + "'", con);
                //cmd2.CommandType = CommandType.Text;
                //cmd2.ExecuteReader();
                //con.Close();

                //con.Open();
                //SqlCommand cmd1 = new SqlCommand("Delete from QuanLyHangVeChuyenBay where  MaChuyenBay=N'" + Chuyenbay.chuyenbaytofix.maCB + "'", con);
                //cmd1.CommandType = CommandType.Text;
                //cmd1.ExecuteReader();
                //con.Close();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Update [CHUYENBAY] set MaChuyenBay = '" + MaCB + "',SanBayDi='" + Sanbaydi + "', SanBayDen='" + Sanbayden + "', NgayKhoiHanh='" + Ngay + "',ThoiGianXuatPhat='" + Gio + "', ThoiGianDuKien='" + TgBay + "', MaHangMayBay='" + mahangMB + "', LoaiMayBay=N'" + loaiMB + "', Gia='" + Gia + "' where MaChuyenBay='" + Chuyenbay.chuyenbaytofix.maCB + "'", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();

                SaveHV_VE();
                chuyenbayTable.Items.Clear();
                loadDatatoTable();
            }
            isSave = true;
        }
        private void Luu_Click(object sender, RoutedEventArgs e)
        {
            SaveCB();
        }

        private void Hoantat_Click(object sender, RoutedEventArgs e)
        {
            if (isSave == false)
            {
                MessageBoxResult result = (MessageBoxResult)MessageBox.Show("Bạn có muốn lưu chuyến bay", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    SaveCB();
                }
                else if (result == MessageBoxResult.No)
                {
                    this.Close();
                }
            }

            isSave = false;
            this.Close();

        }
    }
}
