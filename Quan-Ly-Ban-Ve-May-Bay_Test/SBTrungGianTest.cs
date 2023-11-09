using Quan_Ly_Ban_Ve_May_Bay;
using Quan_Ly_Ban_Ve_May_Bay.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Ve_May_Bay_Test
{
    [TestClass]
    public class SBTrungGianTest
    {
        [TestMethod]
        public void FlightClassConstructorTest()
        {
            TimeSpan times = new TimeSpan(2, 30, 0);
            SBTrungGian sb = new SBTrungGian("Tokyo Airline", times);
            Assert.AreEqual("Tokyo Airline", sb.AirportName);
        }
        [TestMethod]
        public void FlightIdTest()
        {
            SBTrungGian sBTrungGian = new SBTrungGian();
            bool propertyWasUpdated = false;
            sBTrungGian.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "FlightId")
                {
                    propertyWasUpdated = true;
                }
            };
            sBTrungGian.FlightId = "001";
            Assert.IsTrue(propertyWasUpdated);
            Assert.AreEqual("001", sBTrungGian.FlightId);
        }
        [TestMethod]
        public void AirportNameTest()
        {
            SBTrungGian sBTrungGian = new SBTrungGian();
            bool propertyWasUpdated = false;
            sBTrungGian.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "AirportName")
                {
                    propertyWasUpdated = true;
                }
            };
            sBTrungGian.AirportName = "Bamboo";
            Assert.IsTrue(propertyWasUpdated);
            Assert.AreEqual("Bamboo", sBTrungGian.AirportName);
        }
        [TestMethod]
        public void TimeStopTest()
        {
            SBTrungGian sBTrungGian = new SBTrungGian();
            bool propertyWasUpdated = false;
            sBTrungGian.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "TimeStop")
                {
                    propertyWasUpdated = true;
                }
            };
            TimeSpan times = new TimeSpan(2, 30, 0);
            sBTrungGian.TimeStop = times;
            Assert.IsTrue(propertyWasUpdated);
            Assert.AreEqual("150", sBTrungGian.TimeStop.TotalMinutes.ToString());
        }
        [TestMethod]
        public void NoteTest()
        {
            SBTrungGian sBTrungGian = new SBTrungGian();
            bool propertyWasUpdated = false;
            sBTrungGian.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Note")
                {
                    propertyWasUpdated = true;
                }
            };
            sBTrungGian.Note = "001";
            Assert.IsTrue(propertyWasUpdated);
            Assert.AreEqual("001", sBTrungGian.Note);
        }
        [TestMethod]
        public void AirportIdTest()
        {
            SBTrungGian sBTrungGian = new SBTrungGian();
            bool propertyWasUpdated = false;
            sBTrungGian.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "AirportID")
                {
                    propertyWasUpdated = true;
                }
            };
            sBTrungGian.AirportID = "BB001";
            Assert.IsTrue(propertyWasUpdated);
            Assert.AreEqual("BB001", sBTrungGian.AirportID);
        }


    }
}
