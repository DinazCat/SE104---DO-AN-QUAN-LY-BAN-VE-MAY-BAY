using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Ban_Ve_May_Bay;
using Quan_Ly_Ban_Ve_May_Bay.Model;
using Quan_Ly_Ban_Ve_May_Bay.UserControls;

namespace Quan_Ly_Ban_Ve_May_Bay_Test
{
    [TestClass]
    public class TicketTest
    {
        [TestMethod]    
        public void TicketPropertiesTest()
        {
            var ticket = new Ticket();
            bool propertyWasUpdated = false;
            string[] props = new string[]
            {
                "TicketID",
                "FlightClass",
                "SeatNumber",
                "Status",
                "Color"
            };
            ticket.PropertyChanged += (s, e) =>
            {
                if (props.Contains(e.PropertyName))
                {
                    propertyWasUpdated = true;
                }
            };
            ticket.TicketID = "1432532";
            Assert.AreEqual("1432532", ticket.TicketID);
            Assert.IsTrue(propertyWasUpdated);
            propertyWasUpdated = false;
            ticket.FlightClass = "Hang 1";
            Assert.AreEqual("Hang 1", ticket.FlightClass);
            Assert.IsTrue(propertyWasUpdated);
            propertyWasUpdated = false;
            ticket.SeatNumber = 1;
            Assert.AreEqual(1, ticket.SeatNumber);
            Assert.IsTrue(propertyWasUpdated);
            propertyWasUpdated = false;
            ticket.Status = "BOOKED";
            Assert.AreEqual("BOOKED", ticket.Status);
            Assert.IsTrue(propertyWasUpdated);
            propertyWasUpdated = false;
            ticket.Color = "Red";
            Assert.AreEqual("Red", ticket.Color);
            Assert.IsTrue(propertyWasUpdated);
            ticket.HkID = "143";
            Assert.AreEqual("143", ticket.HkID);
            ticket.CMND = "241923279";
            Assert.AreEqual("241923279", ticket.CMND);
            ticket.PhoneNumber = "0843593598";
            Assert.AreEqual("0843593598", ticket.PhoneNumber);
        }
        [TestMethod]
        [DataRow("1", "Hang 1", 15, "BOOKED", "1121",
            "Le Thi Lan Nhi", "241923279", "0843593598", "Red")]
        public void FlightConstructorTest(string ticketID, string flightClass, int seatNumber, 
            string status, string hkID, string hkName, string cmnd, string phoneNumber, string color)
        {
            var ticket = new Ticket(ticketID, flightClass, seatNumber, status, hkID, hkName, cmnd, phoneNumber);
            Assert.AreEqual(ticketID, ticket.TicketID);
            ticket = new Ticket(ticketID, flightClass, seatNumber, status, color);
            Assert.AreEqual(color, ticket.Color);
        }
    }
}
