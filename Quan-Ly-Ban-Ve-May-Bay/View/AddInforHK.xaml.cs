using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Quan_Ly_Ban_Ve_May_Bay.View
{
    /// <summary>
    /// Interaction logic for AddInforHK.xaml
    /// </summary>
    public partial class AddInforHK : Window
    {
        ObservableCollection<Ve> ve = new ObservableCollection<Ve>();
        public AddInforHK()
        {
            InitializeComponent();
            veLV.ItemsSource = ve;

            maVeBox.Items.Add("9");
            maVeBox.Items.Add("22");
            maVeBox.Items.Add("30");


            maVeBox.SelectedIndex = 0;
            ngaylapHDTxt.Text = DateTime.Today.ToString();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public class Ve
        {
            public string mave { get; set; }
            public string soghe { get; set; }
            public string mahangve { get; set; }
            public string mahk { get; set; }
            public string tenhk { get; set; }
            public string cmnd { get; set; }
            public string sdt { get; set; }

        }
        private void btnXacNhanVe_Click(object sender, RoutedEventArgs e)
        {
            if (tenHanhKhachTxt.Text == "" || cmndTxt.Text == "" || sdtTxt.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hành khách!");
            }
            else
            {
                if (ve.Count == 0)
                {
                    ve.Add(new Ve() { mave = maVeBox.SelectedItem.ToString(), soghe = soGheTxt.Text, mahangve = maHangVeTxt.Text, mahk = maHanhKhachText.Text, tenhk = tenHanhKhachTxt.Text, cmnd = cmndTxt.Text, sdt = sdtTxt.Text });
                }
                else
                {
                    for (int i = 0; i < ve.Count; i++)
                    {
                        if (ve[i].mave == maVeBox.SelectedItem)
                        {
                            ve.Remove(ve[i]);
                            ve.Insert(i, new Ve() { mave = maVeBox.SelectedItem.ToString(), soghe = soGheTxt.Text, mahangve = maHangVeTxt.Text, mahk = maHanhKhachText.Text, tenhk = tenHanhKhachTxt.Text, cmnd = cmndTxt.Text, sdt = sdtTxt.Text });
                        }
                        else
                        {
                            ve.Add(new Ve() { mave = maVeBox.SelectedItem.ToString(), soghe = soGheTxt.Text, mahangve = maHangVeTxt.Text, mahk = maHanhKhachText.Text, tenhk = tenHanhKhachTxt.Text, cmnd = cmndTxt.Text, sdt = sdtTxt.Text });
                        }
                    }
                }
            }

        }

        private void maVeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tenHanhKhachTxt.Text = "";
            cmndTxt.Text = "";
            sdtTxt.Text = "";
            for (int i = 0; i < ve.Count; i++)
            {
                if (ve[i].mave == maVeBox.SelectedItem)
                {
                    tenHanhKhachTxt.Text = ve[i].tenhk;
                    cmndTxt.Text = ve[i].cmnd;
                    sdtTxt.Text = ve[i].sdt;
                }
            }
        }

        private void veLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ve item_ve = veLV.SelectedItem as Ve;
            if (item_ve != null)
            {
                tenHanhKhachTxt.Text = item_ve.tenhk;
                cmndTxt.Text = item_ve.cmnd;
                sdtTxt.Text = item_ve.sdt;
            }
        }
    }
}