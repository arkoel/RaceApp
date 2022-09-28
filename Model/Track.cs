using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Track
    {
        public string Name;
        public LinkedList<Section> Sections;
        public Track(string Name, SectionTypes[] sections)
        {
            this.Name = Name;

            this.Sections = new LinkedList<Section>();
            foreach (var item in sections)
            {
                this.Sections.AddLast(new Section(item));
            }

        }
    }
}
