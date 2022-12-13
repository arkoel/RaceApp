using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerTest
{
        [TestFixture]

    public class Controller_Race_CheckFullSection
    {
        private Race _race;
        private Queue<Track> _tracks;
        private List<SectionData.IParticipant> _participants;
        
        [SetUp]
        public void SetUp()
        {
            _tracks = new Queue<Track>() ;
            Data.AddTracks(_tracks);
            _participants = new List<SectionData.IParticipant>();
            Data.AddParticipants(_participants);
            _race = new Race(_tracks.Dequeue(),_participants);
            CheckMoves();
        }

        [Test]
        public void CheckMoves()
        {
            Section Currentsection = new Section(SectionTypes.Straight);
            Section Copy_currentsection = new Section(SectionTypes.Straight);
            Section Targetsection = new Section(SectionTypes.LeftCorner);
            Section Copy_Targetsection = new Section(SectionTypes.LeftCorner);
            _race.GetSectionData(Currentsection).Left = _participants.First();
            _race.GetSectionData(Copy_currentsection).Left = _participants.First();
            
            PrintSectionData(Currentsection);
            PrintSectionData(Targetsection);
            Console.WriteLine("----");
            _race.UpdateDriver(_participants.First(), Currentsection, Targetsection);
            PrintSectionData(Currentsection);
            PrintSectionData(Targetsection);
            Assert.IsTrue(_race.GetSectionData(Currentsection).Left == _race.GetSectionData(Copy_Targetsection).Left);
            Assert.IsTrue(_race.GetSectionData(Currentsection).Right== _race.GetSectionData(Copy_Targetsection).Right);
            Assert.IsTrue(_race.GetSectionData(Copy_currentsection).Left == _race.GetSectionData(Targetsection).Left);
            Assert.IsTrue(_race.GetSectionData(Copy_currentsection).Right == _race.GetSectionData(Targetsection).Right);
        }
        [Test]
        public void Inhalen()
        {
            Section Currentsection = new Section(SectionTypes.Straight);
            Section Copy_currentsection = new Section(SectionTypes.Straight);
            Section Targetsection = new Section(SectionTypes.LeftCorner);
            Section Copy_Targetsection = new Section(SectionTypes.LeftCorner);
            _race.GetSectionData(Currentsection).Left = _participants.First();
            _race.GetSectionData(Targetsection).Left = _participants.Last();
            _race.GetSectionData(Copy_currentsection).Left = _participants.First();
            _race.GetSectionData(Copy_Targetsection).Left = _participants.Last();

            PrintSectionData(Currentsection);
            PrintSectionData(Targetsection);
            Console.WriteLine("----");
            _race.UpdateDriver(_participants.First(), Currentsection, Targetsection);
            PrintSectionData(Currentsection);
            PrintSectionData(Targetsection);
            Assert.IsTrue(_race.GetSectionData(Currentsection).Left == null);
            Assert.IsTrue(_race.GetSectionData(Currentsection).Right == null);
            Assert.IsTrue(_race.GetSectionData(Targetsection).Left == _race.GetSectionData(Copy_Targetsection).Left);
            Assert.IsTrue(_race.GetSectionData(Targetsection).Right == _race.GetSectionData(Copy_currentsection).Left);
        }
        public void PrintSectionData(Section section)
        {
            Console.WriteLine($"left: {_race.GetSectionData(section).Left?.Name}, right: {_race.GetSectionData(section).Right?.Name}");
        }
    }
}
