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
using System.Xml.Linq;
using Quan_Ly_Ban_Ve_May_Bay.ViewModel;

namespace Quan_Ly_Ban_Ve_May_Bay.UserControls
{
    /// <summary>
    /// Interaction logic for RuleChange.xaml
    /// </summary>
    public partial class RuleChange : UserControl
    {
        RuleChangeViewModel ruleChangeVM;
        public RuleChange()
        {
            InitializeComponent();
            ruleChangeVM = new RuleChangeViewModel();
            this.DataContext = ruleChangeVM;
        }

        private void editTGBTT_Click(object sender, RoutedEventArgs e)
        {
            panelTGBTT.Visibility = Visibility.Visible;
            TGBTT.IsReadOnly = false;
            TGBTT.Focus();
        }
        private void editTGBTD_Click(object sender, RoutedEventArgs e)
        {
            panelTGBTD.Visibility = Visibility.Visible;
            TGBTD.IsReadOnly = false;
            TGBTD.Focus();
        }
        private void editTGDTT_Click(object sender, RoutedEventArgs e)
        {
            panelTGDTT.Visibility = Visibility.Visible;
            TGDTT.IsReadOnly = false;
            TGDTT.Focus();
        }
        private void editTGDTD_Click(object sender, RoutedEventArgs e)
        {
            panelTGDTD.Visibility = Visibility.Visible;
            TGDTD.IsReadOnly = false;
            TGDTD.Focus();
        }
        private void editSGTKHCPDV_Click(object sender, RoutedEventArgs e)
        {
            panelSGTKHCPDV.Visibility = Visibility.Visible;
            SGTKHCPDV.IsReadOnly = false;
            SGTKHCPDV.Focus();
        }
        private void editSGTKHCPHV_Click(object sender, RoutedEventArgs e)
        {
            panelSGTKHCPHV.Visibility = Visibility.Visible;
            SGTKHCPHV.IsReadOnly = false;
            SGTKHCPHV.Focus();
        }
        private void OkTGBTTBtn_Click(object sender, RoutedEventArgs e)
        {
            panelTGBTT.Visibility = Visibility.Collapsed;
            TGBTT.IsReadOnly = true;
            try
            {
                ruleChangeVM.ThoiGianBayToiThieu = int.Parse(TGBTT.Text);
            } catch(Exception ex) {
                MessageBox.Show("Dữ liệu không hợp lệ!", "Vui lòng nhập một số nguyên.");
            }
        }
        private void CancelTGBTTBtn_Click(object sender, RoutedEventArgs e)
        {
            panelTGBTT.Visibility = Visibility.Collapsed;
            TGBTT.IsReadOnly = true;
            TGBTT.Text = ruleChangeVM.ThoiGianBayToiThieu.ToString();
        }
        private void OkTGBTDBtn_Click(object sender, RoutedEventArgs e)
        {
            panelTGBTT.Visibility = Visibility.Collapsed;
            TGBTT.IsReadOnly = true;
            try
            {
                ruleChangeVM.ThoiGianBayToiDa = int.Parse(TGBTT.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dữ liệu không hợp lệ!", "Vui lòng nhập một số nguyên.");
            }
        }
        private void CancelTGBTDBtn_Click(object sender, RoutedEventArgs e)
        {
            panelTGBTT.Visibility = Visibility.Collapsed;
            TGBTT.IsReadOnly = true;
            TGBTT.Text = ruleChangeVM.ThoiGianBayToiDa.ToString();
        }
        private void OkTGDTTBtn_Click(object sender, RoutedEventArgs e)
        {
            panelTGBTT.Visibility = Visibility.Collapsed;
            TGBTT.IsReadOnly = true;
            try
            {
                ruleChangeVM.ThoiGianDungToiThieu = int.Parse(TGBTT.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dữ liệu không hợp lệ!", "Vui lòng nhập một số nguyên.");
            }
        }
        private void CancelTGDTTBtn_Click(object sender, RoutedEventArgs e)
        {
            panelTGBTT.Visibility = Visibility.Collapsed;
            TGBTT.IsReadOnly = true;
            TGBTT.Text = ruleChangeVM.ThoiGianDungToiThieu.ToString();
        }
        private void OkTGDTDBtn_Click(object sender, RoutedEventArgs e)
        {
            panelTGBTT.Visibility = Visibility.Collapsed;
            TGBTT.IsReadOnly = true;
            try
            {
                ruleChangeVM.ThoiGianDungToiDa = int.Parse(TGBTT.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dữ liệu không hợp lệ!", "Vui lòng nhập một số nguyên.");
            }
        }
        private void CancelTGDTDBtn_Click(object sender, RoutedEventArgs e)
        {
            panelTGBTT.Visibility = Visibility.Collapsed;
            TGBTT.IsReadOnly = true;
            TGBTT.Text = ruleChangeVM.ThoiGianDungToiDa.ToString();
        }
        private void OkSGTKHCPHVBtn_Click(object sender, RoutedEventArgs e)
        {
            panelTGBTT.Visibility = Visibility.Collapsed;
            TGBTT.IsReadOnly = true;
            try
            {
                ruleChangeVM.SoGioTruocKhoiHanhChoPhepDatVe = int.Parse(TGBTT.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dữ liệu không hợp lệ!", "Vui lòng nhập một số nguyên.");
            }
        }
        private void CancelSGTKHCPHVBtn_Click(object sender, RoutedEventArgs e)
        {
            panelTGBTT.Visibility = Visibility.Collapsed;
            TGBTT.IsReadOnly = true;
            TGBTT.Text = ruleChangeVM.SoGioTruocKhoiHanhChoPhepDatVe.ToString();
        }
        private void OkSGTKHCPDVBtn_Click(object sender, RoutedEventArgs e)
        {
            panelTGBTT.Visibility = Visibility.Collapsed;
            TGBTT.IsReadOnly = true;
            try
            {
                ruleChangeVM.SoGioTruocKhoiHanhChoPhepHuyVe = int.Parse(TGBTT.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dữ liệu không hợp lệ!", "Vui lòng nhập một số nguyên.");
            }
        }
        private void CancelSGTKHCPDVBtn_Click(object sender, RoutedEventArgs e)
        {
            panelTGBTT.Visibility = Visibility.Collapsed;
            TGBTT.IsReadOnly = true;
            TGBTT.Text = ruleChangeVM.SoGioTruocKhoiHanhChoPhepHuyVe.ToString();
        }
    }
}
