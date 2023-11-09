using System.Globalization;
using System;
using System.ComponentModel;
using Quan_Ly_Ban_Ve_May_Bay;
using Quan_Ly_Ban_Ve_May_Bay.Converter;
using System.Diagnostics;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quan_Ly_Ban_Ve_May_Bay_Test
{
    [TestClass]
    public class FlightTest
    {
        [TestMethod]
        [DataRow("QH001", "/Images/logo_Bamboo.png", "Bamboo Airways", "SGN", "HAN",
            "02/08/2023", "15:30", "30", 2, 2500000, 30, 40)]
        public void FlightConstructorTest(string flightID, string airlineLogo, string airlineName,
            string airportDepartureName, string airportDestinationName, string dateDeparture, string
            timeDeparture, string time, int stop, long price, int availableSeats, int bookedSeats)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            string format = "dd/MM/yyyy HH:mm";
            TimeSpan _time = TimeSpan.FromMinutes(double.Parse(time));

            DateTime dateTimeDeparture = DateTime.ParseExact(dateDeparture + " " + timeDeparture, format, provider);
            DateTime dateTimeDestination = dateTimeDeparture.Add(_time);
            string timeDestination = dateTimeDestination.ToString("HH:mm");
            Flight flight = new Flight(flightID, airlineLogo, airlineName, airportDepartureName,
                airportDestinationName, timeDestination, timeDeparture, _time, dateTimeDeparture, dateTimeDestination,
                stop, price, availableSeats, bookedSeats);
            Assert.AreEqual(flightID, flight.FlightID);
            flight = new Flight(flightID, airlineLogo, airlineName, airportDepartureName,
                airportDestinationName, timeDestination, timeDeparture, _time, dateTimeDeparture, dateTimeDestination,
                stop, price);
            Assert.AreEqual(flightID, flight.FlightID);
        }
        [TestMethod]
        public void AirlineNameTest()
        {
            Flight flight = new Flight();
            bool propertyWasUpdated = false;
            flight.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "AirlineName")
                {
                    propertyWasUpdated = true;
                }
            };
            flight.AirlineName = "Jetstar Pacific Airlines";//set
            Assert.IsTrue(propertyWasUpdated);//PropertyChanged
            Assert.AreEqual("Jetstar Pacific Airlines", flight.AirlineName);//get
        }
        
        [TestMethod]
        public void AirlineLogoTest()
        {
            Flight flight = new Flight();
            bool propertyWasUpdated = false;
            flight.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "AirlineLogo")
                {
                    propertyWasUpdated = true;
                }
            };
            flight.AirlineLogo = "/Images/logo_Bamboo.png";
            Assert.IsTrue(propertyWasUpdated);
            Assert.AreEqual("/Images/logo_Bamboo.png", flight.AirlineLogo);
        }
        [TestMethod]
        public void AirportDepartureNameTest()
        {
            Flight flight = new Flight();
            bool propertyWasUpdated = false;
            flight.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "AirportDepartureName")
                {
                    propertyWasUpdated = true;
                }
            };
            flight.AirportDepartureName = "SGN";
            Assert.IsTrue(propertyWasUpdated);
            Assert.AreEqual("SGN", flight.AirportDepartureName);
        }
        [TestMethod]
        public void AirportDestinationNameTest()
        {
            Flight flight = new Flight();
            bool propertyWasUpdated = false;
            flight.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "AirportDestinationName")
                {
                    propertyWasUpdated = true;
                }
            };
            flight.AirportDestinationName = "SGN";
            Assert.IsTrue(propertyWasUpdated);
            Assert.AreEqual("SGN", flight.AirportDestinationName);
        }
        [TestMethod]
        public void TimeDepartureTest()
        {
            Flight flight = new Flight();
            bool propertyWasUpdated = false;
            flight.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "TimeDeparture")
                {
                    propertyWasUpdated = true;
                }
            };
            flight.TimeDeparture = "15:30";
            Assert.IsTrue(propertyWasUpdated);
            Assert.AreEqual("15:30", flight.TimeDeparture);
        }
        [TestMethod]
        public void TimeDestinationTest()
        {
            Flight flight = new Flight();
            bool propertyWasUpdated = false;
            flight.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "TimeDestination")
                {
                    propertyWasUpdated = true;
                }
            };
            flight.TimeDestination = "16:50";
            Assert.IsTrue(propertyWasUpdated);
            Assert.AreEqual("16:50", flight.TimeDestination);
        }
        [TestMethod]
        public void DateTimeDestinationTest()
        {
            Flight flight = new Flight();
            bool propertyWasUpdated = false;
            flight.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "DateTimeDestination")
                {
                    propertyWasUpdated = true;
                }
            };
            DateTime dt = new DateTime(2022, 5, 15);
            flight.DateTimeDestination = dt;
            Assert.IsTrue(propertyWasUpdated);
            Assert.AreEqual("15/05/2022", flight.DateTimeDestination.ToString("dd/MM/yyyy"));
        }

        [TestMethod]
        public void DateTimeDepartureTest()
        {
            Flight flight = new Flight();
            bool propertyWasUpdated = false;
            flight.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "DateTimeDeparture")
                {
                    propertyWasUpdated = true;
                }
            };
            DateTime dt = new DateTime(2022, 5, 15);
            flight.DateTimeDeparture = dt;
            Assert.AreEqual("15/05/2022", flight.DateTimeDeparture.ToString("dd/MM/yyyy"));
            Assert.IsTrue(propertyWasUpdated);
        }


        [TestMethod]
        public void StopTest()
        {
            Flight flight = new Flight();
            bool propertyWasUpdated = false;
            flight.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Stop")
                {
                    propertyWasUpdated = true;
                }
            };
            flight.Stop = 2;
            Assert.IsTrue(propertyWasUpdated);
            Assert.AreEqual(2, flight.Stop);
        }

        [TestMethod]
        public void TimeTest()
        {
            Flight flight = new Flight();
            bool propertyWasUpdated = false;
            flight.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Time")
                {
                    propertyWasUpdated = true;
                }
            };

            TimeSpan times = new TimeSpan(2, 30, 0);
            flight.Time = times;
            Assert.IsTrue(propertyWasUpdated);
            Assert.AreEqual("150", flight.Time.TotalMinutes.ToString());
        }

        [TestMethod]
        public void PriceTest()
        {
            Flight flight = new Flight();
            bool propertyWasUpdated = false;
            flight.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Price")
                {
                    propertyWasUpdated = true;
                }
            };
            flight.Price = 1500000;
            Assert.AreEqual(1500000, flight.Price);
            Assert.IsTrue(propertyWasUpdated);
        }
        [TestMethod]
        public void AvailableSeatsTest()
        {
            Flight flight = new Flight();
            bool propertyWasUpdated = false;
            flight.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "AvailableSeats")
                {
                    propertyWasUpdated = true;
                }
            };
            flight.AvailableSeats = 30;
            Assert.AreEqual(30, flight.AvailableSeats);
            Assert.IsTrue(propertyWasUpdated);
        }

        [TestMethod]
        public void BookedSeatsTest()
        {
            Flight flight = new Flight();
            bool propertyWasUpdated = false;
            flight.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "BookedSeats")
                {
                    propertyWasUpdated = true;
                }
            };
            flight.BookedSeats = 3;
            Assert.AreEqual(3, flight.BookedSeats);
            Assert.IsTrue(propertyWasUpdated);
        }
        [TestMethod]    
        public void FlightIDTest()
        {
            Flight flight = new Flight();
            bool propertyWasUpdated = false;
            flight.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "FlightID")
                {
                    propertyWasUpdated = true;
                }
            };
            flight.FlightID = "VNA201";
            Assert.AreEqual("VNA201", flight.FlightID);
            Assert.IsTrue(propertyWasUpdated);
        }
    }
}