using Quan_Ly_Ban_Ve_May_Bay;
using Quan_Ly_Ban_Ve_May_Bay.Model;
using Quan_Ly_Ban_Ve_May_Bay.Pages;
using Quan_Ly_Ban_Ve_May_Bay.UserControls;
using System.Diagnostics;

namespace Quan_Ly_Ban_Ve_May_Bay_Test
{
    [TestClass]
    public class DataProviderTest
    {
        [TestMethod]
        public void isPositiveIntgerTest()
        {
            Assert.IsFalse(DataProvider.isPositiveInteger("a"));
            Assert.IsFalse(DataProvider.isPositiveInteger("0"));
            Assert.IsTrue(DataProvider.isPositiveInteger("30"));
        }
        [TestMethod]
        public void checkListHangVeTest()
        {
            var data1 = new QLHangVeClass() { Machuyenbay = "VN300", Mahangve = "HV2", Soluong = "3" };
            var data2 = new QLHangVeClass() { Machuyenbay = "VN201", Mahangve = "HV1" };
            var data3 = new QLHangVeClass() { Machuyenbay = "VN202", Soluong = "2" };
            var arrData1 = new List<QLHangVeClass>();
            Assert.AreEqual(0, DataProvider.checkListHangVe(arrData1));
            arrData1.Add(data1);
            Assert.AreEqual(2, DataProvider.checkListHangVe(arrData1));
            arrData1.Add(data2);
            Assert.AreEqual(1, DataProvider.checkListHangVe(arrData1));
            arrData1 = new List<QLHangVeClass>();
            arrData1.Add(data3);
            Assert.AreEqual(1, DataProvider.checkListHangVe(arrData1));
        }
        [TestMethod]
        public void isDuplicateValueTest()
        {
            var arrData1 = new List<QLHangVeClass>();
            Assert.IsFalse(DataProvider.isDuplicateValue(arrData1));
            var data1 = new QLHangVeClass() { Mahangve = "1" };
            arrData1 .Add(data1);
            Assert.IsFalse(DataProvider.isDuplicateValue(arrData1));
            arrData1.Add(data1);
            Assert.IsTrue(DataProvider.isDuplicateValue(arrData1));
            arrData1 = new List<QLHangVeClass>();
            var data2 = new QLHangVeClass() { Mahangve = "2"};
            arrData1.Add (data1);
            arrData1.Add(data2);    
            Assert.IsFalse(DataProvider.isDuplicateValue(arrData1));
        }
        [TestMethod]
        public void checkInforTest()
        {
            var tickets = new List<Ticket>();
            Assert.IsTrue(DataProvider.checkInfor(tickets));
            tickets.Add(new Ticket() { HkName = "1"});
            Assert.IsTrue(DataProvider.checkInfor(tickets));
            tickets.Add(new Ticket() { HkName = "" });
            Assert.IsFalse(DataProvider.checkInfor(tickets));
        }
        [TestMethod]
        [DataRow("241923279", true)]
        [DataRow("243334545", false)]   
        
        public void checkCMNDInArrTest(string compare, bool check)
        {
            var tickets = new List<Ticket>();
            Assert.AreEqual(check, DataProvider.checkCMNDInArr(tickets, check, compare));
            tickets.Add(new Ticket() { CMND = "241925258" });
            Assert.AreEqual(check, DataProvider.checkCMNDInArr(tickets, check, compare));
            tickets.Add(new Ticket() { CMND = compare });
            Assert.AreEqual(false, DataProvider.checkCMNDInArr(tickets, check, compare));
        }
        [TestMethod]    
        public void addDataToSeatTest()
        {
            var data1 = new Ticket() { SeatNumber = 1 };
            var data2 = new Ticket() { SeatNumber = 2 };
            var data3 = new Ticket() { SeatNumber = 3 };
            var data4 = new Ticket() { SeatNumber = 4 };
            var data5 = new Ticket() { SeatNumber = 5 };
            var data6 = new Ticket() { SeatNumber = 6 };
            var data7 = new Ticket() { SeatNumber = 7 };
            var tickets = new List<Ticket>() { data1, data2, data3, data4, data5, data6, data7};
            var expectedResult = new List<Ticket>() { data6 };
            Assert.IsTrue(expectedResult.SequenceEqual(DataProvider.addDataToSeat(tickets, 0)));
            expectedResult = new List<Ticket>() { data1, data7 };
            Assert.IsTrue(expectedResult.SequenceEqual(DataProvider.addDataToSeat(tickets, 1)));
            expectedResult = new List<Ticket>() { data3 };
            Assert.IsTrue(expectedResult.SequenceEqual(DataProvider.addDataToSeat(tickets, 3)));
            expectedResult = new List<Ticket>() { data4 };
            Assert.IsTrue(expectedResult.SequenceEqual(DataProvider.addDataToSeat(tickets, 4)));
            expectedResult = new List<Ticket>() { data5 };
            Assert.IsTrue(expectedResult.SequenceEqual(DataProvider.addDataToSeat(tickets, 5)));
        }
        [TestMethod]
        public void airlineNameFilterTest()
        {
            var flight = new Flight() { AirlineName = "A" };
            Assert.IsFalse(DataProvider.airlineNameFilter(flight));
            flight = new Flight() { AirlineName = "Vietjet Air" };
            Assert.IsTrue(DataProvider.airlineNameFilter(flight));
        }
        [TestMethod]
        public void stopFilterTest()
        {
            var flight0 = new Flight() { Stop = 0 };
            var flight1 = new Flight() { Stop = 1 };
            var flight2 = new Flight() { Stop = 2 };
            var flight3 = new Flight() { Stop = 3 };
            Assert.IsTrue(DataProvider.noStopFilter(flight0));
            Assert.IsFalse(DataProvider.noStopFilter(flight1));
            Assert.IsTrue(DataProvider.oneStopFilter(flight1));
            Assert.IsFalse(DataProvider.oneStopFilter(flight0));
            Assert.IsTrue(DataProvider.moreTwoStopFilter(flight2));
            Assert.IsTrue(DataProvider.moreTwoStopFilter(flight3));
            Assert.IsFalse(DataProvider.moreTwoStopFilter(flight1));
        }
        [TestMethod]
        public void nightToMorning_FilterTest()
        {
            var flight1 = new Flight() { TimeDeparture = "00:00", TimeDestination = "05:59" };
            var flight2 = new Flight() { TimeDeparture = "05:59", TimeDestination = "00:00" };
            var flight3 = new Flight() { TimeDeparture = "02:00", TimeDestination = "04:00" };
            var flight4 = new Flight() { TimeDeparture = "", TimeDestination = "" };
            Assert.IsTrue(DataProvider.nightToMorning_Departure_Filter(flight1));
            Assert.IsTrue(DataProvider.nightToMorning_Departure_Filter(flight2));
            Assert.IsTrue(DataProvider.nightToMorning_Departure_Filter(flight3));
            Assert.IsFalse(DataProvider.nightToMorning_Departure_Filter(flight4));
            Assert.IsTrue(DataProvider.nightToMorning_Destination_Filter(flight1));
            Assert.IsTrue(DataProvider.nightToMorning_Destination_Filter(flight2));
            Assert.IsTrue(DataProvider.nightToMorning_Destination_Filter(flight3));
            Assert.IsFalse(DataProvider.nightToMorning_Destination_Filter(flight4));
        }
        [TestMethod]
        public void morningToNoon_FilterTest()
        {
            var flight1 = new Flight() { TimeDeparture = "06:00", TimeDestination = "11:59" };
            var flight2 = new Flight() { TimeDeparture = "11:59", TimeDestination = "06:00" };
            var flight3 = new Flight() { TimeDeparture = "07:00", TimeDestination = "08:00" };
            var flight4 = new Flight() { TimeDeparture = "", TimeDestination = "" };
            Assert.IsTrue(DataProvider.morningToNoon_Departure_Filter(flight1));
            Assert.IsTrue(DataProvider.morningToNoon_Departure_Filter(flight2));
            Assert.IsTrue(DataProvider.morningToNoon_Departure_Filter(flight3));
            Assert.IsFalse(DataProvider.morningToNoon_Departure_Filter(flight4));
            Assert.IsTrue(DataProvider.morningToNoon_Destination_Filter(flight1));
            Assert.IsTrue(DataProvider.morningToNoon_Destination_Filter(flight2));
            Assert.IsTrue(DataProvider.morningToNoon_Destination_Filter(flight3));
            Assert.IsFalse(DataProvider.morningToNoon_Destination_Filter(flight4));
        }
        [TestMethod]
        public void noonToEvening_FilterTest()
        {
            var flight1 = new Flight() { TimeDeparture = "12:00", TimeDestination = "17:59" };
            var flight2 = new Flight() { TimeDeparture = "17:59", TimeDestination = "12:00" };
            var flight3 = new Flight() { TimeDeparture = "13:00", TimeDestination = "14:00" };
            var flight4 = new Flight() { TimeDeparture = "", TimeDestination = "" };
            Assert.IsTrue(DataProvider.noonToEvening_Departure_Filter(flight1));
            Assert.IsTrue(DataProvider.noonToEvening_Departure_Filter(flight2));
            Assert.IsTrue(DataProvider.noonToEvening_Departure_Filter(flight3));
            Assert.IsFalse(DataProvider.noonToEvening_Departure_Filter(flight4));
            Assert.IsTrue(DataProvider.noonToEvening_Destination_Filter(flight1));
            Assert.IsTrue(DataProvider.noonToEvening_Destination_Filter(flight2));
            Assert.IsTrue(DataProvider.noonToEvening_Destination_Filter(flight3));
            Assert.IsFalse(DataProvider.noonToEvening_Destination_Filter(flight4));
        }
        [TestMethod]
        public void eveningToNight_FilterTest()
        {
            var flight1 = new Flight() { TimeDeparture = "18:00", TimeDestination = "23:59" };
            var flight2 = new Flight() { TimeDeparture = "23:59", TimeDestination = "18:00" };
            var flight3 = new Flight() { TimeDeparture = "19:00", TimeDestination = "20:00" };
            var flight4 = new Flight() { TimeDeparture = "", TimeDestination = "" };
            Assert.IsTrue(DataProvider.eveningToNight_Departure_Filter(flight1));
            Assert.IsTrue(DataProvider.eveningToNight_Departure_Filter(flight2));
            Assert.IsTrue(DataProvider.eveningToNight_Departure_Filter(flight3));
            Assert.IsFalse(DataProvider.eveningToNight_Departure_Filter(flight4));
            Assert.IsTrue(DataProvider.eveningToNight_Destination_Filter(flight1));
            Assert.IsTrue(DataProvider.eveningToNight_Destination_Filter(flight2));
            Assert.IsTrue(DataProvider.eveningToNight_Destination_Filter(flight3));
            Assert.IsFalse(DataProvider.eveningToNight_Destination_Filter(flight4));
        }
    }
}
