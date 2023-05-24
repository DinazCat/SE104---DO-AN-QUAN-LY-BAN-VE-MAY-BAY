
using Quan_Ly_Ban_Ve_May_Bay.Model;
using Quan_Ly_Ban_Ve_May_Bay.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using static Quan_Ly_Ban_Ve_May_Bay.UserControls.Sanbay;

namespace Quan_Ly_Ban_Ve_May_Bay.View
{
    /// <summary>
    /// Interaction logic for AddSanBay.xaml
    /// </summary>
    public partial class AddSanBay : Window
    {
        DataGrid SanbayDataGrid;
        int thaotac;
        public AddSanBay(DataGrid sanbayDataGrid, int thaotac)
        {
            InitializeComponent();
            SanbayDataGrid = sanbayDataGrid;
            this.thaotac = thaotac;
            if (thaotac == 1)
            {
                masanbayTxb.Text = Sanbay.sanbaytofix.maSB;
                tensanbayTxb.Text = Sanbay.sanbaytofix.tenSB;
                tinhTxb.Text = Sanbay.sanbaytofix.tinh;

            }
        }
        public static string MaSB, TenSB, Tinh;
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void loadDatatoTable()
        {
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
            int stt = 1;
            foreach (DataRow dr in dt.Rows)
            {

                sanbayclass sb = new sanbayclass();
                sb.STT = stt.ToString();
                sb.maSB = dr["MaSanBay"].ToString();
                sb.tenSB = dr["TenSanBay"].ToString();
                sb.tinh = dr["Tinh"].ToString();
                SanbayDataGrid.Items.Add(sb);
                stt++;
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MaSB = masanbayTxb.Text;
            TenSB = tensanbayTxb.Text;
            Tinh = tinhTxb.Text;
            if (thaotac == 0)
            {
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
                sanbayclass sb = new sanbayclass();
                sb.STT = (dt.Rows.Count + 1).ToString();
                sb.maSB = MaSB;
                sb.tenSB = TenSB;
                sb.tinh = Tinh;
                SanbayDataGrid.Items.Add(sb);

                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9LVKB7T\SQLEXPRESS;Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into [SANBAY] values('" + MaSB + "',N'" + TenSB + "', N'" + Tinh + "')", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();

                this.Close();

            }
            else
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9LVKB7T\SQLEXPRESS;Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("Update [SANBAY] set MaSanBay='" + MaSB + "', TenSanBay='" + TenSB + "', Tinh='" + Tinh + "' where MaSanBay='" + Sanbay.sanbaytofix.maSB + "'", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                SanbayDataGrid.Items.Clear();
                loadDatatoTable();
                this.Close();
            }

        }
    }
}
