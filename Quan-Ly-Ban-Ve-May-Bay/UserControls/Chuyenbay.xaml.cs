using Quan_Ly_Ban_Ve_May_Bay.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Quan_Ly_Ban_Ve_May_Bay.UserControls.Sanbay;
using Quan_Ly_Ban_Ve_May_Bay.Model;

namespace Quan_Ly_Ban_Ve_May_Bay.UserControls
{
    /// <summary>
    /// Interaction logic for Chuyenbay.xaml
    /// </summary>
    public partial class Chuyenbay : UserControl
    {
        private readonly QLCB_SB q;
        public Chuyenbay()
        {
            InitializeComponent();
            //loadDatatoTable();
        }
        public Chuyenbay(QLCB_SB q)
        {
            InitializeComponent();
            this.q = q;
            //loadDatatoTable();
        }
        public void loadDatatoTable()
        {
            string query = "SELECT * FROM CHUYENBAY";
            SqlParameter param1 = new SqlParameter("", "");
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
                CBTable.Items.Add(cb);
                stt++;
            }
        }
        DataTable dt;
        public class chuyenbayclass
        {
            public string STT { get; set; }
            public string maCB { get; set; }
            public string SBdi { get; set; }
            public string SBden { get; set; }
            public string datetime { get; set; }
            public string tgBay { get; set; }
            public string Gia { get; set; }
        }
        public class SanbayTG
        {
            public string STT { get; set; }
            public string tenSB { get; set; }
            public string TGdung { get; set; }
            public string TGden { get; set; }
            public string ghichu { get; set; }
        }
        AddChuyenbay addChuyenbay;
        private void Them_Click(object sender, RoutedEventArgs e)
        {
            addChuyenbay = new AddChuyenbay(CBTable, 0);
            addChuyenbay.Show();
        }

        private void Xoa_Click(object sender, RoutedEventArgs e)
        {
            chuyenbayclass info = CBTable.SelectedItem as chuyenbayclass;
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from CHUYENBAY where MaChuyenBay=N'" + info.maCB + "'", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteReader();
            con.Close();
            con.Open();
            SqlCommand cmd2 = new SqlCommand("Delete from  SANBAYTRUNGGIAN where MaChuyenBay=N'" + info.maCB + "'", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteReader();
            con.Close();
            CBTable.Items.Clear();
            loadDatatoTable();

        }
        QLCB_SB qLCB_SB;
        public static chuyenbayclass chuyenbaytofix;
        private void Sua_Click(object sender, RoutedEventArgs e)
        {
            chuyenbaytofix = CBTable.SelectedItem as chuyenbayclass;
            if (chuyenbaytofix != null)
            {
                AddChuyenbay cb = new AddChuyenbay(CBTable, 1);
                cb.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng bạn muốn sửa");
            }
        }

        private void CBTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //q.sanbaydetail.Visibility = Visibility.Hidden;
            //q.Cbaydetail.Visibility = Visibility.Visible;
            chuyenbayclass info = CBTable.SelectedItem as chuyenbayclass;
            if (info != null)
            {
                q.machuyenbayTxb.Text = info.maCB;
                q.fromtoTxb.Text = info.SBdi + "-" + info.SBden;
                q.datetimeTxb.Text = info.datetime;
                string query = "SELECT * FROM CHUYENBAY where MaChuyenBay = @ma";
                SqlParameter param1 = new SqlParameter("@ma", info.maCB);
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

                    q.H1H2Txb.Text = dr["SLGheHang1"].ToString() + "-" + dr["SLGheHang2"].ToString();
                }
            }
        }
    }
}
