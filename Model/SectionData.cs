using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SectionData
    {
        public IParticipant Left    { get; set; }
        public int DistanceLeft     { get; set; }
        public IParticipant Right   { get; set; }
        public int DistanceRight    { get; set; }
        public SectionData(IParticipant left, int distanceLeft, IParticipant right,int dictanceRight)
        {
            this.Left = left;
            this.DistanceLeft = distanceLeft;
            this.Right = right;
            this.DistanceRight = dictanceRight;
        }

        public interface IParticipant    
        {
            public string Name  { get; set; }
            public int Points   { get; set; }
            public int RacesWon { get; set; }
            public IEquipment Equipment { get; set; }
            public TeamColors Teamcolor { get; set; }

            
            enum TeamColors
            {
                Red,
                Green,
                Yellow,
                Grey,
                Blue,
            }
        }
    }
}
