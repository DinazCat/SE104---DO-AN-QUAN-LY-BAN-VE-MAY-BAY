using Quan_Ly_Ban_Ve_May_Bay.Model;
using Quan_Ly_Ban_Ve_May_Bay.Pages;
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
    }
}
