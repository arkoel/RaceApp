using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Section
    {
        public SectionTypes SectionType { get; set; }
        public Section(SectionTypes sectiontype)
        {
            SectionType = sectiontype;
        }

    }
        public enum SectionTypes
        {
            Straight,
            LeftCorner,
            RightCorner,
            Finish,
            StartGrid
        }
    
}