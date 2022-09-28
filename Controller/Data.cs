using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    public static class Data
    {
        public static Competition _competition { get; set; }
        public static Track CurrentRace;
        public static void Initialize(Competition compitition)
        {
            _competition = compitition;
            AddParticipants();
            AddTracks();
        }

        public static void NextRace()
        {
            Track _track = _competition.NextTrack();
            if(_track != null) 
            {
                CurrentRace = _track;
            }
        }

        public static void AddParticipants()
        {
            _competition.participants.Add(new Driver("henk", SectionData.IParticipant.TeamColors.Red, new Car(100, 1, 0, false)));
            _competition.participants.Add(new Driver("Piet", SectionData.IParticipant.TeamColors.Blue, new Car(100, 1, 0, false)));
            _competition.participants.Add(new Driver("Jan", SectionData.IParticipant.TeamColors.Green, new Car(100, 1, 0, false)));
           
        }
        public static void AddTracks()
        {
            _competition.Tracks.Enqueue(new Track("Track 1", new SectionTypes[] { SectionTypes.RightCorner, SectionTypes.Finish, SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight })); ;
            _competition.Tracks.Enqueue(new Track("Track 2", new SectionTypes[] { SectionTypes.RightCorner, SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.Finish}));
            _competition.Tracks.Enqueue(new Track("Track 3", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Finish }));
        }
    }
}