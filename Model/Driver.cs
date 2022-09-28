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
        private string _Name;
        private string _Points;
        private IEquipment _Equipment;
        private Enum _TeamColors;

        public string Name { get { return _Name; } set { _Name = value; } }
        public string Points { get { return _Points; } set { _Points = value; } }
        public IEquipment Equipment { get { return _Equipment; } set { _Equipment = value; } }
        Enum TeamColors { get { return _TeamColors; } set { _TeamColors = value; } }


        //private int _Quality;
        //private int _Performance;
        //private int _Speed;
        //private bool _IsBroken;

        //int IEquipment.Quality { get { return _Quality; } set { _Quality = value; } }
        //int IEquipment.Performance { get { return _Performance; } set { _Performance = value; } }
        //int IEquipment.Speed { get { return _Speed; } set { _Speed = value; } }
        //bool IEquipment.IsBroken { get { return _IsBroken; } set { _IsBroken = value; } }

        public Driver(string Name, SectionData.IParticipant.TeamColors TeamColor, IEquipment Equipment)
        {
            this._Name = Name;
            this._TeamColors = TeamColor;
            //this._Performance = Performance;
            this._Points = "0";
            //this._Quality = 100;
            //this._IsBroken = false;
            //this._Speed = 0;
            this._Equipment = Equipment;
                       
        }
    }

}
