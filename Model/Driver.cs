using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Model
{
    public class Driver : SectionData.IParticipant
    {
        

        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public SectionData.IParticipant.TeamColors Teamcolor { get; set; }
        public int RacesWon { get; set; }
        

        public Driver(string name, SectionData.IParticipant.TeamColors teamColor, IEquipment equipment)
        {
            this.Name = name;
            this.Teamcolor = teamColor;
            this.Points = 0;
            this.Equipment = equipment;
            this.RacesWon = 0;
        }
    }

}
