using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Competition
    {
        public List<SectionData.IParticipant> Participants { get; private set; }
        public Queue<Track> Tracks { get; private set; }
        public Track NextTrack() {
            if(Tracks.Count == 0) { return null; }
            return Tracks.Dequeue();            
        }

        public Competition(Queue<Track> tracks, List<SectionData.IParticipant> participants)
        {
            if(tracks.Count < 2) { throw new Exception("Gegroet, Helaas is het tot mijn aandacht gekomen dat er minder dan 2 tracks zijn gegeven voor de competitie. Dit is helaas niet de bedoeling."); }
            if(participants.Count < 3) { throw new Exception("Hallo, Er blijken minden dan 3 participanten meegegeven te zijn voor deze compititie. Ik vraag u vriendelijk om dit niet meer te doen."); }
            this.Participants = participants;
            this.Tracks = tracks;
        }
    
    }
}
