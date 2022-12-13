
using System.Runtime.CompilerServices;
using System.Timers;
using Model;

namespace Controller
{
    public delegate void NexttrackEvent();

    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }
        public static NexttrackEvent NexttrackEvent { get; set; }
        public static bool UsingWPF { get; set; }


        public static void Initialize(bool UsingWPF)
        {
            Data.UsingWPF = UsingWPF;
            List<SectionData.IParticipant> participants = new List<SectionData.IParticipant>();
            AddParticipants(participants);
            Queue<Track> tracks = new Queue<Track>();
            AddTracks(tracks);
            Competition = new Competition(tracks, participants);
        }

        /// <summary>
        /// Runs Next Race in compitition
        /// </summary>
        public static void NextRace()
        {
            Track Track = Competition.NextTrack();
            if(Track != null) 
            {
                CurrentRace = new Race(Track,Competition.Participants);
                NexttrackEvent?.Invoke();
            }
        }

        /// <summary>
        /// Adds participants to List
        /// </summary>
        /// <param name="participants"></param>
        public static void AddParticipants(List<SectionData.IParticipant> participants)
        {
            participants.Add(new Driver("henk", SectionData.IParticipant.TeamColors.Red, new Car(0, 0, 0, false)));
            participants.Add(new Driver("Piet", SectionData.IParticipant.TeamColors.Blue, new Car(0, 0, 0, false)));
            participants.Add(new Driver("Jan de Snelle", SectionData.IParticipant.TeamColors.Green, new Car(0, 0, 0, false)));
            //participants.Add(new Driver("Jan", SectionData.IParticipant.TeamColors.Green, new Car(100, 2, 10, false)));
        }

        /// <summary>
        /// Adds Tracks to List 
        /// </summary>
        /// <param name="tracks"></param>
        public static void AddTracks(Queue<Track> tracks)
        {
            tracks.Enqueue(new Track("Track 1", new SectionTypes[] { SectionTypes.StartGrid,SectionTypes.Finish, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner})); ;
            tracks.Enqueue(new Track("Track 2", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.RightCorner,SectionTypes.RightCorner, SectionTypes.Straight,SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.RightCorner }));
            //tracks.Enqueue(new Track("Track 3", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight  }));
        }
    }
}