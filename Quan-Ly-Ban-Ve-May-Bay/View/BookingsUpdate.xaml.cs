using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace Quan_Ly_Ban_Ve_May_Bay.View
{
    /// <summary>
    /// Interaction logic for BookingsUpdate.xaml
    /// </summary>
    public partial class BookingsUpdate : Window
    {
        private string MaVe;
        //public event RoutedEventHandler ReturnBookings;
        public BookingsUpdate()
        {
            InitializeComponent();
        }

        public void ShowInforHK(string mave)
        {
            MaVe = mave;
            DataProvider.sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(
                "select [v].TenHK, [v].CMND, [v].SDT from [VE] [v] " +
                "where [v].MaVe = @mave", DataProvider.sqlConnection);
            sqlCommand.Parameters.Add("@mave", SqlDbType.NVarChar).Value = mave;
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    tenHanhKhachTxt.Text = reader["TenHK"].ToString();
                    cmndTxt.Text = reader["CMND"].ToString();
                    sdtTxt.Text = reader["SDT"].ToString();
                }
            }
            DataProvider.sqlConnection.Close();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy chỉnh sửa?");
            if (result == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        private void btnXacNhan_Click(object sender, RoutedEventArgs e)
        {
            if (tenHanhKhachTxt.Text == "" || cmndTxt.Text == "" || sdtTxt.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
            }
            else
            {
                DataProvider.sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(
                    "update [VE] set TenHK = @tenhk, CMND = @cmnd, SDT = @sdt " +
                    "where MaVe = @mave", DataProvider.sqlConnection);
                sqlCommand.Parameters.Add("@tenhk", SqlDbType.NVarChar).Value = tenHanhKhachTxt.Text;
                sqlCommand.Parameters.Add("@cmnd", SqlDbType.NVarChar).Value = cmndTxt.Text;
                sqlCommand.Parameters.Add("@sdt", SqlDbType.NVarChar).Value = sdtTxt.Text;
                sqlCommand.Parameters.Add("@mave", SqlDbType.NVarChar).Value = MaVe;
                sqlCommand.ExecuteNonQuery();
                DataProvider.sqlConnection.Close();

                MessageBox.Show("Cập nhật thông tin hành khách thành công!");
                //ReturnBookings?.Invoke(this, new RoutedEventArgs());
                this.Close();
            }

        }

        private void tenHanhKhachTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}