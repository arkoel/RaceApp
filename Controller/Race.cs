using Model;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.SectionData;

namespace Controller
{
    public class Race
    {
        public Track Track;
        public List<SectionData.IParticipant> Participants;
        public DateTime StartTime;
        private Random _random;
        private Dictionary<Section, SectionData> _positions;
        public SectionData GetSectionData(Section Section) 
        {
            SectionData Sdata;
            try
            {
                Sdata = _positions[Section];
            }
            catch (Exception ex)
            {
                Sdata = new SectionData(null,1,null,0);
                _positions.Add(Section, Sdata);
            }
            return Sdata;
        }

        public void RandomizeEquipment()
        {
            _random = new Random(DateTime.Now.Millisecond);
            foreach (SectionData.IParticipant participant in Participants)
            {
                participant.Equipment.Quality = _random.Next();
                participant.Equipment.Performance = _random.Next();
            }
        }
        public void PlaceDrivers() {
            double HalfPart = (double)Participants.Count / 2.0;
            double AmountStartPlaces = Math.Ceiling(HalfPart);
            for (int j = 0; j > -1 * AmountStartPlaces; j--)
            {
                if (j < 0)
                {
                    Section sectie = Track.Sections.ElementAt(Track.Sections.Count+j);
                    _positions.Add(sectie, new SectionData(Participants[j*-2],0, (j * -2 + 1 >= Participants.Count) ? null: Participants[j * -2 + 1], 0));
                }
                else 
                {
                    Section sectie = Track.Sections.ElementAt(0);
                    _positions.Add(sectie, new SectionData(Participants[0], 0, Participants[1], 0));
                }
            }
           
        }

        public Race(Track track, List<SectionData.IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _positions = new Dictionary<Section, SectionData>();
            PlaceDrivers();
            
        }


    }
}
