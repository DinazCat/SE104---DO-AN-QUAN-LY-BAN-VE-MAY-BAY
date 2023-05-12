using Quan_Ly_Ban_Ve_May_Bay.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
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
using Quan_Ly_Ban_Ve_May_Bay.View;

namespace Quan_Ly_Ban_Ve_May_Bay.UserControls
{
    /// <summary>
    /// Interaction logic for FareClassManagement.xaml
    /// </summary>
    public class FareClass
    {
        string _id, _name, _percentage;
        public string id { get { return _id; } set { _id = value; } }
        public string name { get { return _name; } set { _name = value; } }
        public string percentage { get { return _percentage; } set { _percentage = value; } }
    }
    public partial class FareClassManagement : UserControl
    {
        public FareClassManagement()
        {
            InitializeComponent();
            loadData();
            
        }
        private void loadData() {
            FareClassTable.Items.Clear();
            string query = "SELECT * FROM HANGVE";
            DataTable dt;
            using (SqlDataReader reader = DataProvider.ExecuteReader(query, CommandType.Text))
            {
                dt = new DataTable();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                    foreach (DataRow dr in dt.Rows)
                    {
                        FareClass fareClass = new FareClass();
                        fareClass.id = dr[0].ToString();
                        fareClass.name = dr[1].ToString();
                        fareClass.percentage = dr[2].ToString();
                        FareClassTable.Items.Add(fareClass);
                    }
                }
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            FareClassForm fareClassForm = new FareClassForm();
            fareClassForm.ShowDialog();
            if (fareClassForm.isSaved)
                loadData();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            FareClass selectedFareClass = FareClassTable.SelectedItem as FareClass;
            if (selectedFareClass != null)
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa hạng vé này không?", "Xóa hạng vé", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True");
                    sqlCon.Open();
                    SqlCommand cmd = new SqlCommand("Delete from [HANGVE]  where MaHangVe='" + selectedFareClass.id + "'", sqlCon);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    sqlCon.Close();
                    FareClassTable.Items.Remove(selectedFareClass);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng bạn muốn xóa!", "Error");
            }
        }
    }
}
