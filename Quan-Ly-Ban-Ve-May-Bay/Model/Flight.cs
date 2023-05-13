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
        private string airlineNameLogo;
        public string AirlineNameLogo
        {
            get { return airlineNameLogo; }
            set
            {
                airlineNameLogo = value;
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
        private string timDeparture;
        public string TimDeparture
        {
            get { return timDeparture; }
            set
            {
                timDeparture = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
