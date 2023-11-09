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
        //viết tương tự cho set, get, PropertyChanged của thuộc tính khác
    }
}