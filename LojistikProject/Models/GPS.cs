using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LojistikProject.Models
{
    public class GPS : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //public double UserGps { get; set; }
        public GPS()
        {

        }

        public double UserLatitude { get; set; }

        public double UserAltitude { get; set; }


       
        public void OnPropertyChanged(string _gps)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_gps));
        }
    }
}
