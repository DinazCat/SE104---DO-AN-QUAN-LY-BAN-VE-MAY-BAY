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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Quan_Ly_Ban_Ve_May_Bay.UserControls
{
    /// <summary>
    /// Interaction logic for HangVeItem.xaml
    /// </summary>
    public partial class HangVeItem : UserControl
    {
        public HangVeItem()
        {
            InitializeComponent();
            string query = "SELECT * FROM HANGVE";
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
                maHVcBox.Items.Add(dr["MaHangVe"]);
            }

            SLcBox.Items.Add("6");
            SLcBox.Items.Add("12");
            SLcBox.Items.Add("18");

        }


        private void maHVcBox_DropDownClosed(object sender, EventArgs e)
        {
            QLHangVeClass hv = (sender as ComboBox).DataContext as QLHangVeClass;
            if (hv != null)
            {
                hv.Mahangve = (sender as ComboBox).SelectedItem as string;
            }
        }

        private void SLcBox_DropDownClosed(object sender, EventArgs e)
        {
            QLHangVeClass hv = (sender as ComboBox).DataContext as QLHangVeClass;
            if (hv != null)
            {
                hv.Soluong = (sender as ComboBox).SelectedItem as string;
            }
        }

        private void maHVcBox_DropDownOpened(object sender, EventArgs e)
        {
            QLHangVeClass hv = (sender as ComboBox).DataContext as QLHangVeClass;
            if (hv.Machuyenbay != null && hv.Mahangve != null)
            {
                string query = "SELECT * From VE where MaChuyenBay = @ma and MaHangVe = @mah";
                SqlParameter param1 = new SqlParameter("@ma", hv.Machuyenbay);
                SqlParameter param2 = new SqlParameter("@mah", hv.Mahangve);
                DataTable table;
                using (SqlDataReader reader = DataProvider.ExecuteReader(query, CommandType.Text, param1, param2))
                {
                    table = new DataTable();
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Không thể sửa hạng vé này.", "Thông báo");

                        return;

                    }
                }
            }
        }

        private void SLcBox_DropDownOpened(object sender, EventArgs e)
        {
            QLHangVeClass hv = (sender as ComboBox).DataContext as QLHangVeClass;
            if (hv.Machuyenbay != null && hv.Mahangve != null)
            {
                string query = "SELECT * From VE where MaChuyenBay = @ma and MaHangVe = @mah";
                SqlParameter param1 = new SqlParameter("@ma", hv.Machuyenbay);
                SqlParameter param2 = new SqlParameter("@mah", hv.Mahangve);
                DataTable table;
                using (SqlDataReader reader = DataProvider.ExecuteReader(query, CommandType.Text, param1, param2))
                {
                    table = new DataTable();
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Không thể sửa hạng vé này.", "Thông báo");

                        return;

                    }
                }
            }
        }
    }
}
