using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model
{
    public class Car : IEquipment
    {

        private int _Quality;
        private int _Performance;
        private int _Speed;
        private bool _IsBroken;
        int IEquipment.Quality { get { return _Quality; } set { _Quality = value; } }
        int IEquipment.Performance { get { return _Performance; } set { _Performance = value; } }
        int IEquipment.Speed { get { return _Speed; } set { _Speed = value; } } 
        bool IEquipment.IsBroken { get { return _IsBroken; } set { _IsBroken = value; } }
        public Car(int Quality, int Performance, int Speed, bool IsBroken)
        {
            this._Quality = Quality;
            this._Performance = Performance;
            this._Speed = Speed;
            this._IsBroken = IsBroken;
        }
    }
}