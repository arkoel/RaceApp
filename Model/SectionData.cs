using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SectionData
    {
        public IParticipant Left;
        public int DistanceLeft;
        public IParticipant Right;
        public int DistanceRight;
        public SectionData(IParticipant Left, int DistanceLeft, IParticipant Right,int DictanceRight)
        {
            this.Left = Left;
            this.DistanceLeft = DistanceLeft;
            this.Right = Right;
            this.DistanceRight = DictanceRight;
        }


        public interface IParticipant //: IEquipment    
        {
            public string Name { get; set; }
            public string Points { get; set; }
            public IEquipment Equipment { get; set; }

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
