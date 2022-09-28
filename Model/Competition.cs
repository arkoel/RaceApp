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
        public List<SectionData.IParticipant> participants { get; set; }
        public Queue<Track> Tracks { get; set; }
        public Track NextTrack() {
            //return Tracks.Count > 0 ? Tracks.Dequeue() : null;
            if(Tracks.Count == 0) { return null; }
            return Tracks.Dequeue();            
        }
        
        public Competition()
        { 
            this.participants = new List<SectionData.IParticipant>();
            this.Tracks = new Queue<Track>(); 
        }
      
    }
}
