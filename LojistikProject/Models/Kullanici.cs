using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LojistikProject.Models
{
    public class Kullanici : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public string UserPass { get; set; }
        public string UserSirketKod { get; set; }
        public string AdminKod { get; set; }

        public double UserLatitude{ get; set; }
        public double UserAltitude{ get; set; }

        public void OnPropertyChanged(string _gps)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_gps));
        }

    }
}
