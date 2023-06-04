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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Quan_Ly_Ban_Ve_May_Bay.Pages;
using Quan_Ly_Ban_Ve_May_Bay.View;
using Quan_Ly_Ban_Ve_May_Bay.Model;
using System.Data.SqlClient;
using System.Data;

namespace Quan_Ly_Ban_Ve_May_Bay.Pages
{
    /// <summary>
    /// Interaction logic for MyBookings.xaml
    /// </summary>
    public partial class MyBookings : Page
    {
        BookingsDetail bookingsDetail;
        BookingsUpdate bookingsUpdate;
        public MyBookings()
        {
            InitializeComponent();
            bookingsUpdate = new BookingsUpdate();
        }


        public void MyTicket(string userID)
        {
            DataProvider.sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(
                "select [v].TenHK, [v].SoGhe, [hv].TenHangVe, [cb].SanBayDi, [cb].SanBayDen,[cb].NgayKhoiHanh, [cb].ThoiGianXuatPhat " +
                "from [HOADON] [hd], [VE] [v], [CTHD] [ct], [CHUYENBAY] [cb], [HANGVE] [hv]" +
                "where  [hd].MaHD = [ct].MaHD and " +
                        "[v].MaVe = [ct].MaVe and " +
                        "[v].MaChuyenBay = [cb].MaChuyenBay and " +
                        "[hv].MaHangVe = [v].MaHangVe and" +
                        "[hd].MaTK = @userID " +
                "order by [v].MaVe DESC", DataProvider.sqlConnection);
            sqlCommand.Parameters.Add("@userID", SqlDbType.NVarChar).Value = userID;
            SqlDataReader reader = sqlCommand.ExecuteReader();
            List<SymbolTicket> list = new List<SymbolTicket>();

            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    string tuyen = reader["SanBayDi"].ToString() + " - " + reader["SanBayDen"].ToString();
                    string ngaygio = reader["ThoiGianXuatPhat"].ToString() + " " + reader["NgayKhoiHanh"].ToString();
                    string soghe = reader["SoGhe"].ToString();
                    string hangve = reader["TenHangVe"].ToString();
                    string tenhk = reader["TenHK"].ToString();
                    list.Add(new SymbolTicket(tuyen, ngaygio, soghe, hangve, tenhk));
                }
            }
            DataProvider.sqlConnection.Close();

            lvTicket.ItemsSource = list;
            if (list.Count == 0)
            {
                StatusBookings.Text = "Không có lịch sử đặt vé";
            }
            else StatusBookings.Text = "Vui lòng nhấn vào vé để xem thông tin vé!";


        }

        public void NoLogin()
        {
            StatusBookings.Text = "Vui lòng đăng nhập để xem thông tin vé!";
            tuTxt.IsEnabled = false;
            denTxt.IsEnabled = false;
            ngayBox.IsEnabled = false;
            hangveBox.IsEnabled = false;
        }

        private void lvTicket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bookingsDetail = new BookingsDetail();
            fTicket.Content = bookingsDetail;
        }
    }
}