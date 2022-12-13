using Model;
using System;
using System.Diagnostics;
using static Model.SectionData;
using Section = Model.Section;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<SectionData.IParticipant> Participants { get; set; }
        private DateTime StartTime;
        private Random Random;
        public Dictionary<Section, SectionData> Positions { get; set; }
        private static System.Timers.Timer Timer;
        private int AmountOfCurrentDrivers;
        private bool SomeoneFinished { get; set; }
        public event EventHandler<DriversChangedEventArgs> DriversChanged;

        public Race(Track track, List<SectionData.IParticipant> participants)
        {
            Track = track;
            Positions = new Dictionary<Section, SectionData>();
            Participants = participants;
            RandomizeEquipment();
            PlaceDrivers();
            AmountOfCurrentDrivers = participants.Count;
            Timer = new System.Timers.Timer(500);
            Timer.Elapsed += OnTimedEvent;
            start();
        }

        /// <summary>
        /// Triggers every 0.5 seconds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs eventArgs)
        {
            bool TrackChange = UpdateDrivers();
            if (TrackChange || Data.UsingWPF)
            {
                DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track));
            }

            if (AmountOfCurrentDrivers == 0)
            {
                //garbage collector                
                Timer.Elapsed -= OnTimedEvent;
                DriversChanged = null;
                foreach (var participant in Participants)
                {
                    participant.Points = 0;
                }
                Data.NextRace();
            }
        }

        /// <summary>
        /// Method to give driver chance to break (isBroken => true)
        /// </summary>
        /// <param name="participant"></param>
        public void BreakDriver(SectionData.IParticipant participant)
        {
            int randomValue = Random.Next(100);
            if (!participant.Equipment.IsBroken)
            {
                participant.Equipment.IsBroken = (randomValue > 98);
            }
            else
            {
                if (randomValue > 70)
                {
                    participant.Equipment.IsBroken = false;
                    participant.Equipment.Speed -= 1;
                    if (participant.Equipment.Speed <= 0)
                        participant.Equipment.Speed = 1;
                }
            }
        }

        /// <summary>
        /// Gets sectiondata from positions
        /// </summary>
        /// <param name="Section"></param>
        /// <returns></returns>
        public SectionData GetSectionData(Section Section)
        {
            SectionData Sdata;
            if (Positions.ContainsKey(Section))
            {
                Sdata = Positions[Section];
            }
            else
            {
                Sdata = new SectionData(null, 0, null, 0);
                Positions.Add(Section, Sdata);
            }
            return Sdata;
        }

        /// <summary>
        /// Create random values for Speed & Performance
        /// </summary>
        public void RandomizeEquipment()
        {
            Random = new Random(DateTime.Now.Millisecond);
            foreach (SectionData.IParticipant participant in Participants)
            {
                participant.Equipment.Quality = Random.Next(1, 1);
                participant.Equipment.Performance = Random.Next(7,9);
                participant.Equipment.Speed = Random.Next(9, 11);
            }
        }

        #region Driverdata
        public void PlaceDrivers()
        {
            double HalfPart = (double)Participants.Count / 2.0;
            double AmountStartPlaces = Math.Ceiling(HalfPart);
            for (int j = 0; j > -1 * AmountStartPlaces; j--)
            {
                if (Math.Abs(j) == Track.Sections.Count) break;
                Section sectie = Track.Sections.ElementAt(Convert.ToInt32(j < 0) * (Track.Sections.Count + j));
                SectionData sectiondata = GetSectionData(sectie);
                sectiondata.Left = Participants[j * -2];
                if (Participants.Count == 1 || j * -2 + 1 >= Participants.Count)
                    sectiondata.Right = null;
                else
                    sectiondata.Right = Participants[j == 0 ? 1 : j * -2 + 1];
                sectiondata.DistanceLeft = 0;
                sectiondata.DistanceRight = 0;

            }
        }

        /// <summary>
        /// Changed driversposition according to speed
        /// </summary>
        /// <returns></returns>
        private bool UpdateDrivers()
        {
            bool TrackChange = false;
            foreach (IParticipant participant in Participants)
            {
                Section section;
                Section targetsection;
                for (LinkedListNode<Section> node = Track.Sections.First; node != null; node = node.Next)
                {
                    section = node.Value;

                    if (GetSectionData(section).Left == participant || GetSectionData(section).Right == participant)
                    {
                        BreakDriver(participant);
                        //Calc Speed
                        bool isleft = (GetSectionData(section).Left == participant);
                        IEquipment equipment = participant.Equipment;
                        int AmountOfMeters = (equipment.Speed * equipment.Performance * equipment.Quality * (participant.Equipment.IsBroken?0:1));
                        //Calc Sections
                        int AmountOfSections = 0;
                        if (isleft)
                        {
                            GetSectionData(section).DistanceLeft += AmountOfMeters;
                            AmountOfSections = GetSectionData(section).DistanceLeft / 100;
                        }
                        else
                        {
                            GetSectionData(section).DistanceRight += AmountOfMeters;
                            AmountOfSections = GetSectionData(section).DistanceRight / 100;
                        }

                        targetsection = node.Value;
                        for (int i = 0; i < AmountOfSections; i++)
                        {
                            node = node.Next;
                            if (node == null) { node = Track.Sections.First; }
                            targetsection = node.Value;
                            SectionData targetdata = GetSectionData(targetsection);

                            if (AmountOfSections > 1 && targetsection.SectionType == SectionTypes.Finish) { participant.Points++; }

                            if ((GetSectionData(targetsection).Left != null) && (GetSectionData(targetsection).Right != null))
                            {
                                if (node.Previous == null) { targetsection = Track.Sections.Last.Value; }
                                else { targetsection = node.Previous.Value; }
                                break;
                            }
                        }

                        if (section != targetsection)
                        {
                            UpdateDriver(participant, section, targetsection);
                            TrackChange = true;
                            if (targetsection.SectionType == SectionTypes.Finish)
                            {
                                participant.Points++;
                            }
                            if (!SomeoneFinished && participant.Points > 2)
                            {
                                participant.RacesWon++;
                                SomeoneFinished = true;
                            }
                            if (participant.Points > 2)
                            {
                                SectionData targetdata = GetSectionData(targetsection);
                                if (targetdata.Left == participant)
                                    targetdata.Left = null;
                                else if (targetdata.Right == participant)
                                    targetdata.Right = null;
                                this.AmountOfCurrentDrivers--;
                            }
                        }
                        break;
                    }
                }
            }
            return TrackChange;
        }

        /// <summary>
        /// Change location of driver from currentsection to targetsection
        /// </summary>
        /// <param name="participant"></param>
        /// <param name="currentsection"></param>
        /// <param name="targetsection"></param>
        public void UpdateDriver(IParticipant participant, Section currentsection, Section targetsection) //public voor unit test
        {
            SectionData targetData = GetSectionData(targetsection);
            SectionData currentData = GetSectionData(currentsection);

            if (targetData.Left == null) { targetData.Left = participant; }
            else if (targetData.Right == null) { targetData.Right = participant; }
            if (currentData.Left == participant)
            {
                currentData.Left = null;
                currentData.DistanceLeft = 0;
            }
            else if (currentData.Right == participant)
            {
                currentData.Right = null;
                currentData.DistanceRight = 0;
            }

        }
        #endregion

        /// <summary>
        /// start timer
        /// </summary>
        public void start()
        {
            Timer.Stop();
            Timer.Start();
        }

    }
}
