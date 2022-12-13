using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace WPF2
{
    public class Competition_Data_Context : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public List<SectionData.IParticipant> Participants { get; set; }
        public List<Track> Tracks { get; set; }

        public void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            Participants = new List<SectionData.IParticipant>(Data.Competition.Participants);
            if (Data.CurrentRace is not null)
            {
                List<Track> templist = new List<Track>();
                templist.Add(Data.CurrentRace.Track);

                templist.AddRange(Data.Competition.Tracks.ToList());
                Tracks = new List<Track>(templist);
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
        public void OnNextRace()
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
        }
    }
}
