using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Ve_May_Bay.Model
{
    internal class FlightClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string classColor;
        public string ClassColor
        {
            get { return classColor; }
            set
            {
                classColor = value;
                RaisePropertyChanged();
            }
        }
        private string className;
        public string ClassName
        {
            get { return className; }
            set
            {
                className = value;
                RaisePropertyChanged();
            }
        }
    }
}
