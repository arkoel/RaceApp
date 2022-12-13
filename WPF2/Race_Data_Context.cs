using Controller;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WPF2.DataContext
{
    public class Race_Data_Context : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public IEnumerable ParticipantInfo { get; set; }
        public List<SectionData.IParticipant> Participants { get; set; }
        public string TrackName { get; set; }

        public void OnDriversChanged(object sender, DriversChangedEventArgs DriversChangedEventArgs)
        {
            if (Data.CurrentRace is not null)
            {
                UpdateProperties(DriversChangedEventArgs);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
            }
        }

        public void UpdateProperties(DriversChangedEventArgs DriversChangedEvenArgs)
        {
            var Querry1 =
                   from participant in Data.CurrentRace.Participants
                   join section in DriversChangedEvenArgs.Track.Sections
                   on participant equals
                   Data.CurrentRace.GetSectionData(section).Left
                   select new
                   {
                       participant.Name,
                       Speed = participant.Equipment.Speed * participant.Equipment.Quality,
                       participant.Equipment.IsBroken,
                       section.SectionType,
                       participant.Teamcolor,
                       participant.Equipment.Quality
                   };

            var Querry2 =
                from participant in Data.CurrentRace.Participants
                join section in DriversChangedEvenArgs.Track.Sections
                on participant equals
                Data.CurrentRace.GetSectionData(section).Right
                select new
                {
                    participant.Name,
                    Speed = participant.Equipment.Speed * participant.Equipment.Quality,
                    participant.Equipment.IsBroken,
                    section.SectionType,
                    participant.Teamcolor,
                    participant.Equipment.Quality
                };

            ParticipantInfo = Querry1.Concat(Querry2);
            TrackName = DriversChangedEvenArgs.Track.Name;
            Participants = new List<SectionData.IParticipant>(Data.CurrentRace.Participants);
        }

        public void OnNextRace()
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
        }
    }
}
