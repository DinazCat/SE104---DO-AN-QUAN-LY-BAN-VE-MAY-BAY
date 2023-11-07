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
    public class PropertyChangedTest
    {
        [TestMethod]

        public void PropertyChangedEventHandlerIsRaised()
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
            flight.AirlineName = "Jetstar Pacific Airlines";
            Assert.IsTrue(propertyWasUpdated);
        }
    }
}