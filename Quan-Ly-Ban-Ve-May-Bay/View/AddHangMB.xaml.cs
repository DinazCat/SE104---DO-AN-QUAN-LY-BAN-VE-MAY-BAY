﻿using Quan_Ly_Ban_Ve_May_Bay.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
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
using static Quan_Ly_Ban_Ve_May_Bay.UserControls.Hangmaybay;
using Quan_Ly_Ban_Ve_May_Bay.UserControls;

namespace Quan_Ly_Ban_Ve_May_Bay.View
{
    /// <summary>
    /// Interaction logic for AddHangMB.xaml
    /// </summary>
    public partial class AddHangMB : Window
    {
        int thaotac;
        DataGrid hangmbtable;
        public AddHangMB(DataGrid dataGrid, int thaotac)
        {
            InitializeComponent();
            this.thaotac = thaotac;
            this.hangmbtable = dataGrid;
            if (thaotac == 1)
            {
                mahangTxb.Text = Hangmaybay.hangbaytofix.mahang;
                mahangTxb.IsEnabled = false;
                tenhangTxb.Text = Hangmaybay.hangbaytofix.tenhang;
            }
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void loadDatatoHMB()
        {
            string query = "SELECT * FROM HANGMAYBAY";
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

                HangMBclass hb = new HangMBclass();
                hb.STT = stt.ToString();
                hb.mahang = dr["MaHang"].ToString();
                hb.tenhang = dr["TenHang"].ToString();
                hangmbtable.Items.Add(hb);
                stt++;
            }
        }
        private string mahang, tenhang;
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            mahang = mahangTxb.Text;
            tenhang = tenhangTxb.Text;
            if (thaotac == 0)
            {
                string query = "SELECT * FROM HANGMAYBAY";
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
                HangMBclass hb = new HangMBclass();
                hb.STT = (dt.Rows.Count + 1).ToString();
                hb.tenhang = tenhang;
                hb.mahang = mahang;
                hangmbtable.Items.Add(hb);
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9LVKB7T\SQLEXPRESS;Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into HANGMAYBAY values('" + mahang + "',N'" + tenhang + "')", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();

                this.Close();
            }
            else
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9LVKB7T\SQLEXPRESS;Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("Update [HANGMAYBAY] set MaHang='" + mahang + "',TenHang='" + tenhang + "' where MaHang='" + Hangmaybay.hangbaytofix.mahang + "'", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                hangmbtable.Items.Clear();
                loadDatatoHMB();
                this.Close();
            }
        }
    }
}