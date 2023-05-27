using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Ve_May_Bay.Model
{
    internal class Ticket : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string ticketID;
        public string TiketID
        {
            get { return ticketID; }
            set
            {
                ticketID = value;
                RaisePropertyChanged();
            }
        }
        private string flightClass;
        public string FlightClass
        {
            get { return flightClass; }
            set
            {
                flightClass = value;
                RaisePropertyChanged();
            }
        }
        private int seatNumber;
        public int SeatNumber
        {
            get { return seatNumber; }
            set
            {
                seatNumber = value;
                RaisePropertyChanged();
            }
        }
        private string status;
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                RaisePropertyChanged();
            }
        }
        private string color;
        public string Color
        {
            get { return color; }
            set
            {
                color = value;
                RaisePropertyChanged();
            }
        }

        public Ticket() { }
        public Ticket(string ticketID, string flightClass, int seatNumber, string status, string color)
        {
            this.ticketID = ticketID;
            this.flightClass = flightClass;
            this.seatNumber = seatNumber;
            this.status = status;
            this.color = color;
        }
    }
}
