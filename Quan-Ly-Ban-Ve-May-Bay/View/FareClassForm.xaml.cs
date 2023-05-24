using Quan_Ly_Ban_Ve_May_Bay.UserControls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Quan_Ly_Ban_Ve_May_Bay.View
{
    /// <summary>
    /// Interaction logic for FareClassForm.xaml
    /// </summary>
    public partial class FareClassForm : Window
    {
        public bool isSaved = false;
        public FareClassForm()
        {
            InitializeComponent();
        }

        private void BtnSave_click(object sender, RoutedEventArgs e)
        {
            isSaved = true;
            if (Name.Text == "" || Percentage.Text == "" )
            {
                txblError.Text = "Vui lòng nhập đầy đủ thông tin!";
                return;
            }
            try
            {
                int p = int.Parse(Percentage.Text);
                SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-9LVKB7T\SQLEXPRESS;Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True");
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                    {
                        sqlCon.Open();
                        String query = "SELECT COUNT(1) FROM [HANGVE] WHERE TenHangVe=@name";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.AddWithValue("@name", Name.Text);
                        int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            txblError.Text = "Hạng vé này đã tồn tại!";
                            return;
                        }
                    }
                }
                catch { }
                finally { sqlCon.Close(); }
                Random rd = new Random();
                int ID = rd.Next(100000, 999999);
                sqlCon.Open();              
                SqlCommand cmd = new SqlCommand("Insert into [HANGVE] values('" + "HV" + ID + "',N'" + Name.Text + "','" + Percentage.Text + "')", sqlCon);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                sqlCon.Close();
                this.Close();
                MessageBox.Show("Thêm hạng vé thành công!", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vui lòng nhập tỷ lệ phần trăm là một số nguyên.", "Dữ liệu không hợp lệ!");
            }
        }

        private void BtnCancel_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
