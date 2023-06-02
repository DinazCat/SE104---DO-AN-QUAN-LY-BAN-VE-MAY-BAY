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
using Quan_Ly_Ban_Ve_May_Bay.Model;
using static Quan_Ly_Ban_Ve_May_Bay.UserControls.Sanbay;
using static Quan_Ly_Ban_Ve_May_Bay.UserControls.Chuyenbay;

namespace Quan_Ly_Ban_Ve_May_Bay.View
{
    /// <summary>
    /// Interaction logic for AddSBTG.xaml
    /// </summary>
    public partial class AddSBTG : Window
    {
        DataGrid SanBayTG;
        string maCB;
        int thaotac;
        public AddSBTG(DataGrid sanBayTG, string maCB, int thaotac)
        {
            InitializeComponent();
            SanBayTG = sanBayTG;
            this.maCB = maCB;
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
                SBTGcBox.Items.Add(dr["TenSanBay"].ToString());
            }
            SBTGcBox.SelectedIndex = 0;
            if (thaotac == 1)
            {
                SBTGcBox.SelectedItem = AddChuyenbay.infotofix.tenSB;
                thoigiandungTxb.Text = AddChuyenbay.infotofix.TGdung;
                ghichuTxb.Text = AddChuyenbay.infotofix.ghichu;
            }
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public string tenSB, tgDung, GhiChu;

        public void loadDatatoSBTG()
        {
            string query = "SELECT * FROM SANBAYTRUNGGIAN WHERE MaChuyenBay=@macb";
            SqlParameter param1 = new SqlParameter("@macb", maCB);
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
                SanBayTG.Items.Add(sb);
                stt++;
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            tenSB = SBTGcBox.Text;
            tgDung = thoigiandungTxb.Text;
            GhiChu = ghichuTxb.Text;
            if (thaotac == 0)
            {
                string query = "SELECT * FROM SANBAYTRUNGGIAN where MaChuyenBay = @ma";
                SqlParameter param1 = new SqlParameter("@ma", maCB);
                DataTable dt;
                using (SqlDataReader reader = DataProvider.ExecuteReader(query, CommandType.Text, param1))
                {
                    dt = new DataTable();
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                }
                SanbayTG sb = new SanbayTG();
                sb.STT = (dt.Rows.Count + 1).ToString();
                sb.tenSB = tenSB;
                sb.TGdung = tgDung;
                sb.ghichu = GhiChu;
                SanBayTG.Items.Add(sb);
                SqlConnection con = DataProvider.sqlConnection;
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into SANBAYTRUNGGIAN values('" + tenSB + "',N'" + maCB + "',N'" + tgDung + "',N'" + GhiChu + "')", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();

                this.Close();
            }
            else
            {
                SqlConnection con = DataProvider.sqlConnection;
                con.Open();
                SqlCommand cmd = new SqlCommand("Update [SANBAYTRUNGGIAN] set SanBayTrungGian='" + tenSB + "',ThoiGianDung='" + tgDung + "', GhiChu='" + GhiChu + "' where MaChuyenBay='" + maCB + "' and SanBayTrungGian='" + AddChuyenbay.infotofix.tenSB + "'", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                SanBayTG.Items.Clear();
                loadDatatoSBTG();
                this.Close();
            }
        }
    }
}
