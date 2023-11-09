using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Ban_Ve_May_Bay.Pages;
using System.Windows.Media.Media3D;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Quan_Ly_Ban_Ve_May_Bay.Model
{
    public class DataProvider
    {
        //private static string connectionStr = @"Data Source=(local);Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True";
        private static string connectionStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True";

        public static SqlDataReader ExecuteReader(String commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connectionStr);

            SqlCommand cmd = new SqlCommand(commandText, conn);

            cmd.CommandType = commandType;
            cmd.Parameters.AddRange(parameters);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }


        //public static SqlConnection sqlConnection = new SqlConnection(@"Data Source=(local);Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True");
        public static SqlConnection sqlConnection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanVeMayBay;Integrated Security=True");

        public static List<string> colorList = new List<string>() { "#C8D70C", "#CB1D1D", "#459DF2", "#459DF2", "#F44EEE", "#ECA65D", "#ECA65D", "#C0F081" };
        public static bool isPositiveInteger(string input)
        {
            try
            {
                int p1 = int.Parse(input);
                if (p1 <= 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static int checkListHangVe(List<QLHangVeClass> qLHangVeClass)
        {
            if(qLHangVeClass.Count <= 0)
                return 0;
            for (int i = 0; i < qLHangVeClass.Count; i++)
            {
                if (qLHangVeClass[i].Mahangve == null || qLHangVeClass[i].Soluong == null)
                    return 1;
            }
            return 2;   
        }
        public static bool isDuplicateValue(List<QLHangVeClass> qLHangVeClass)
        {
            var tempArr = new List<string>();
            for (int i = 0; i < qLHangVeClass.Count; i++)
            {
                if (tempArr.Contains(qLHangVeClass[i].Mahangve))
                    return true;
                else
                {
                    tempArr.Add(qLHangVeClass[i].Mahangve); 
                }
            }
            return false;
        }
        public static bool checkInfor(List<Ticket>ve)
        {
            for(int i = 0; i < ve.Count; i++)
            {
                if (ve[i].HkName == "")
                    return false;
            }
            return true;
        }
        public static bool checkCMNDInArr(List<Ticket>ve, bool check, string compare)
        {
            for(int i = 0; i < ve.Count; i++)
            {
                if (ve[i].CMND == compare)
                {
                    return false;
                }
            }
            return check;
        }
        public static List<Ticket> addDataToSeat(List<Ticket>tickets, int reminder) 
        { 
            List<Ticket> tempArr = new List<Ticket>();
            for (int i = 0; i < tickets.Count; i++)
            {
                if (tickets[i].SeatNumber % 6 == reminder) {
                    tempArr.Add((Ticket)tickets[i]);    
                }
            }
            return tempArr;
        }
        public static string selectedAirlineName = "Vietjet Air";
        public static bool airlineNameFilter(object item)
        {
            return ((item as Flight).AirlineName.CompareTo(selectedAirlineName) == 0);
        }
        public static bool noStopFilter(object item)
        {
            return ((item as Flight).Stop == 0);
        }
        public static bool oneStopFilter(object item)
        {
            return ((item as Flight).Stop == 1);
        }
        public static bool moreTwoStopFilter(object item)
        {
            return ((item as Flight).Stop >= 2);
        }
        public static bool nightToMorning_Destination_Filter(object item)
        {
            if (((item as Flight).TimeDestination.CompareTo("00:00")) >= 0 && ((item as Flight).TimeDestination.CompareTo("05:59")) <= 0)
            {
                return true;
            }
            return false;
        }
        public static bool morningToNoon_Destination_Filter(object item)
        {
            if (((item as Flight).TimeDestination.CompareTo("06:00")) >= 0 && ((item as Flight).TimeDestination.CompareTo("11:59")) <= 0)
            {
                return true;
            }
            return false;
        }
        public static bool noonToEvening_Destination_Filter(object item)
        {
            if (((item as Flight).TimeDestination.CompareTo("12:00")) >= 0 && ((item as Flight).TimeDestination.CompareTo("17:59")) <= 0)
            {
                return true;
            }
            return false;
        }
        public static bool eveningToNight_Destination_Filter(object item)//test
        {
            if (((item as Flight).TimeDestination.CompareTo("18:00")) >= 0 && ((item as Flight).TimeDestination.CompareTo("23:59")) <= 0)
            {
                return true;
            }
            return false;
        }

        public static bool nightToMorning_Departure_Filter(object item)
        {
            if (((item as Flight).TimeDeparture.CompareTo("00:00")) >= 0 && ((item as Flight).TimeDeparture.CompareTo("05:59")) <= 0)
            {
                return true;
            }
            return false;
        }
        public static bool morningToNoon_Departure_Filter(object item)//test
        {
            if (((item as Flight).TimeDeparture.CompareTo("06:00")) >= 0 && ((item as Flight).TimeDeparture.CompareTo("11:59")) <= 0)
            {
                return true;
            }
            return false;
        }
        public static bool noonToEvening_Departure_Filter(object item)//test
        {
            if (((item as Flight).TimeDeparture.CompareTo("12:00")) >= 0 && ((item as Flight).TimeDeparture.CompareTo("17:59")) <= 0)
            {
                return true;
            }
            return false;
        }
        public static bool eveningToNight_Departure_Filter(object item)//test
        {
            if (((item as Flight).TimeDeparture.CompareTo("18:00")) >= 0 && ((item as Flight).TimeDeparture.CompareTo("23:59")) <= 0)
            {
                return true;
            }
            return false;
        }
    }
}
