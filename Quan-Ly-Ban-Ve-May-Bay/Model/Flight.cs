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
        private string hour;
        public string Hour
        {
            get { return hour; }
            set
            {
                hour = value;
                RaisePropertyChanged();
            }
        }
        private string ho;

        public string Ho
        {
            get { return ho; }
            set
            {
                ho = value;
                RaisePropertyChanged();
            }
        }
        public Flight() { }
        public Flight(string AirlineName, string hour, string ho)
        {
            airlineName = AirlineName;
            Hour = hour;
            Ho = ho;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
        }
    }
}
