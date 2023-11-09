using Quan_Ly_Ban_Ve_May_Bay;
using Quan_Ly_Ban_Ve_May_Bay.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Quan_Ly_Ban_Ve_May_Bay_Test
{
    [TestClass]
    public class FlightClassTest
    {
        [TestMethod]
        [DataRow("001", "Hang ve 1", "20", "#fff")]
        public void FlightClassConstructorTest(string classId, string className, string percent, string classColor)
        {
            FlightClass flightClass = new FlightClass(classId, className, percent, classColor);
            Assert.AreEqual(classId, flightClass.ClassId);
            flightClass = new FlightClass(classId, className, percent, classColor);
            Assert.AreEqual(classId, flightClass.ClassId);
        }
        [TestMethod]
        [DataRow("Hang ve 1", "#fff")]
        public void FlightClassConstructorTest2(string className, string classColor)
        {
            FlightClass flightClass = new FlightClass(className, classColor);
            Assert.AreEqual(className, flightClass.ClassName);
            flightClass = new FlightClass(className, classColor);
            Assert.AreEqual(className, flightClass.ClassName);
        }
        [TestMethod]
        public void FlightClassIdTest()
        {
            FlightClass flightClass = new FlightClass();
            bool propertyWasUpdated = false;
            flightClass.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "ClassId")
                {
                    propertyWasUpdated = true;
                }
            };
            flightClass.ClassId = "001";
            Assert.IsTrue(propertyWasUpdated);
            Assert.AreEqual("001", flightClass.ClassId);
        }
        [TestMethod]
        public void FlightClassColorTest()
        {
            FlightClass flightClass = new FlightClass();
            bool propertyWasUpdated = false;
            flightClass.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "ClassColor")
                {
                    propertyWasUpdated = true;
                }
            };
            flightClass.ClassColor = "#FFF";
            Assert.IsTrue(propertyWasUpdated);
            Assert.AreEqual("#FFF", flightClass.ClassColor);
        }
        [TestMethod]
        public void FlightClassPercentTest()
        {
            FlightClass flightClass = new FlightClass();
            bool propertyWasUpdated = false;
            flightClass.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Percent")
                {
                    propertyWasUpdated = true;
                }
            };
            flightClass.Percent = "20";
            Assert.IsTrue(propertyWasUpdated);
            Assert.AreEqual("20", flightClass.Percent);
        }
        [TestMethod]
        public void FlightClassNameTest()
        {
            FlightClass flightClass = new FlightClass();
            bool propertyWasUpdated = false;
            flightClass.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "ClassName")
                {
                    propertyWasUpdated = true;
                }
            };
            flightClass.ClassName = "001";
            Assert.IsTrue(propertyWasUpdated);
            Assert.AreEqual("001", flightClass.ClassName);
        }
    }
}
