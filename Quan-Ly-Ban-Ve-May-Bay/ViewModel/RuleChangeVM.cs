using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Ve_May_Bay.ViewModel
{
    internal class RuleChangeViewModel : INotifyPropertyChanged
    {
        private int _thoiGianBayToiThieu = 30;
        private int _thoiGianBayToiDa = 2;
        private int _thoiGianDungToiThieu = 10;
        private int _thoiGianDungToiDa = 20;
        private int _soGioTruocKhoiHanhChoPhepDatVe = 24;
        private int _soGioTruocKhoiHanhChoPhepHuyVe = 1;
        public int ThoiGianBayToiThieu
        {
            get { return _thoiGianBayToiThieu; }
            set
            {
                if (_thoiGianBayToiThieu != value)
                {
                    _thoiGianBayToiThieu = value;
                    OnPropertyChanged("ThoiGianBayToiThieu");
                }
            }
        }
        public int ThoiGianBayToiDa
        {
            get { return _thoiGianBayToiDa; }
            set
            {
                if (_thoiGianBayToiDa != value)
                {
                    _thoiGianBayToiDa = value;
                    OnPropertyChanged("ThoiGianBayToiDa");
                }
            }
        }
        public int ThoiGianDungToiThieu
        {
            get { return _thoiGianDungToiThieu; }
            set
            {
                if (_thoiGianDungToiThieu != value)
                {
                    _thoiGianDungToiThieu = value;
                    OnPropertyChanged("ThoiGianDungToiThieu");
                }
            }
        }
        public int ThoiGianDungToiDa
        {
            get { return _thoiGianDungToiDa; }
            set
            {
                if (_thoiGianDungToiDa != value)
                {
                    _thoiGianDungToiDa = value;
                    OnPropertyChanged("ThoiGianDungToiDa");
                }
            }
        }
        public int SoGioTruocKhoiHanhChoPhepHuyVe
        {
            get { return _soGioTruocKhoiHanhChoPhepHuyVe; }
            set
            {
                if (_soGioTruocKhoiHanhChoPhepHuyVe != value)
                {
                    _soGioTruocKhoiHanhChoPhepHuyVe = value;
                    OnPropertyChanged("SoGioTruocKhoiHanhChoPhepHuyVe");
                }
            }
        }
        public int SoGioTruocKhoiHanhChoPhepDatVe
        {
            get { return _soGioTruocKhoiHanhChoPhepDatVe; }
            set
            {
                if (_soGioTruocKhoiHanhChoPhepDatVe != value)
                {
                    _soGioTruocKhoiHanhChoPhepDatVe = value;
                    OnPropertyChanged("SoGioTruocKhoiHanhChoPhepDatVe");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

