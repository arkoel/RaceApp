using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WPF2
{
    public class Data_Context : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public string TrackName { get; set; }

        public void OndriversChanged(object Sender, DriversChangedEventArgs DriversChangedEventArgs)
        {
            TrackName = DriversChangedEventArgs.Track.Name;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

    }
}
