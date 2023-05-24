using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Ve_May_Bay
{
    internal class Flight : INotifyPropertyChanged
    {
        private string flightID;
        public string FlightID
        {
            get { return flightID; }
            set
            {
                flightID = value;
                RaisePropertyChanged();
            }
        }
        private string airlineLogo;
        public string AirlineLogo
        {
            get { return airlineLogo; }
            set
            {
                airlineLogo = value;
                RaisePropertyChanged();
            }
        }
        private string airlineName;
        public string AirlineName
        {
            get { return airlineName; }
            set
            {
                airlineName = value;
                RaisePropertyChanged();
            }
        }
        private string airportDepartureName;
        public string AirportDepartureName
        {
            get { return airportDepartureName; }
            set
            {
                airportDepartureName = value;
                RaisePropertyChanged();
            }
        }

        private string airportDestinationName;
        public string AirportDestinationName
        {
            get { return airportDestinationName; }
            set
            {
                airportDestinationName = value;
                RaisePropertyChanged();
            }
        }
        private string timeDestination;
        public string TimeDestination
        {
            get { return timeDestination; }
            set
            {
                timeDestination = value;
                RaisePropertyChanged();
            }
        }
        private string timeDeparture;
        public string TimeDeparture
        {
            get { return timeDeparture; }
            set
            {
                timeDeparture = value;
                RaisePropertyChanged();
            }
        }
        private string time;
        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                RaisePropertyChanged();
            }
        }
        private string stop;

        public string Stop
        {
            get { return stop; }
            set
            {
                stop = value;
                RaisePropertyChanged();
            }
        }
        private string price;

        public string Price
        {
            get { return price; }
            set
            {
                price = value;
                RaisePropertyChanged();
            }
        }
        public Flight() { }
        public Flight(string flightID, string airlineLogo, string airlineName, string airportDepartureName, string airportDestinationName, string timeDestination, string timeDeparture, string time, string stop, string price)
        {
            this.flightID = flightID;
            this.airlineLogo = airlineLogo;
            this.airlineName = airlineName;
            this.airportDepartureName = airportDepartureName;
            this.airportDestinationName = airportDestinationName;
            this.timeDestination = timeDestination;
            this.timeDeparture = timeDeparture;
            this.time= time;
            this.stop = stop;
            this.price = price;
        }   

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
