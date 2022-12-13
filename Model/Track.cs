using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }
        public Track(string name, SectionTypes[] sections)
        {
            this.Name = name;
            this.Sections = new LinkedList<Section>();
            foreach (var item in sections)
            {
                this.Sections.AddLast(new Section(item));
            }
        }
    }
}
