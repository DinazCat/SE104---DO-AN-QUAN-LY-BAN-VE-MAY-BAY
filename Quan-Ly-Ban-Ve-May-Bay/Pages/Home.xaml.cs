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
using static Quan_Ly_Ban_Ve_May_Bay.MainWindow;

namespace Quan_Ly_Ban_Ve_May_Bay.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        /* SqlConnection sqlConnection = new SqlConnection(@"Server=(local);Database=Data;Trusted_Connection=Yes;");
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataAdapter adapter;
        DataSet ds; */
        public event RoutedEventHandler Search;
        public Home()
        {
            InitializeComponent();
            //bindcombobox();
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search?.Invoke(this, new RoutedEventArgs());
        }
        /*private void bindcombobox()
        {
            sqlConnection.Open();
            sqlCommand = new SqlCommand("select * from ccb_destination", sqlConnection);
            adapter = new SqlDataAdapter(sqlCommand);
            ds = new DataSet();
            adapter.Fill(ds, "ccb_destination");
            IList<destination> destinations = new List<destination>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                destinations.Add(new destination
                {
                    code = dr[0].ToString(),
                    city = dr[1].ToString()
                });
            }
            ds = null;
            adapter.Dispose();
            sqlConnection.Close();
            sqlConnection.Dispose();
            cbb_from.ItemsSource = destinations;
            cbb_from.DisplayMemberPath = "city";
            cbb_to.ItemsSource = destinations;
            cbb_to.DisplayMemberPath = "city";

        }
        public class destination
        {
            public string code { get; set; }
            public string city { get; set; }
        }*/

    }
}
