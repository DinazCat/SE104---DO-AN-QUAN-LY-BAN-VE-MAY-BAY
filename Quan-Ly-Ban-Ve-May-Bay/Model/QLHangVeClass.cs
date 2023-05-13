using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Ve_May_Bay.Model
{
    internal class QLHangVeClass : INotifyPropertyChanged
    {
        private string machuyenbay;
        private string mahangve;
        private int gia;
        private string soluong;

        public string Machuyenbay { get { return machuyenbay; } set { machuyenbay = value; } }
        public string Mahangve { get { return mahangve; } set { mahangve = value; OnPropertyChanged("Mahangve"); } }
        public int Gia { get { return gia; } set { gia = value; } }
        public string Soluong { get { return soluong; } set { soluong = value; OnPropertyChanged("Soluong"); } }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newname));
            }
        }
    }
}
