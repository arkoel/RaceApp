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
    public class Controller_Race_PlaceDrivers
    {
        private Race _race;
        private Track _track;
        [SetUp]
        public void SetUp()
        {           
            _track = new Track("track1", new SectionTypes[] { SectionTypes.Straight });
            _race = new Race(_track,new List<SectionData.IParticipant>() {new Driver("henk",SectionData.IParticipant.TeamColors.Blue,new Car(100,1,1,false)) });
            _race.Positions = new Dictionary<Section, SectionData>();
            PlaceOneDriver();
            Console.WriteLine("------------");
            PlaceMoreDriversThanPossible();
        }

        [Test]
        public void PlaceOneDriver()
        {
            Assert.IsNull(_race.GetSectionData(_track.Sections.First()).Left);
            Assert.IsNull(_race.GetSectionData(_track.Sections.First()).Right);
            PrintSectionData(_track.Sections.First());
            _race.PlaceDrivers();
            Assert.IsNotNull(_race.GetSectionData(_track.Sections.First()).Left);
            Assert.IsNull(_race.GetSectionData(_track.Sections.First()).Right);
            PrintSectionData(_track.Sections.First());
            _race.Positions = new Dictionary<Section, SectionData>();
        }

        [Test]
        public void PlaceMoreDriversThanPossible()
        {
            _race.Participants.Add(new Driver("henk2", SectionData.IParticipant.TeamColors.Blue, new Car(100, 1, 1, false)));
            _race.Participants.Add(new Driver("henk3", SectionData.IParticipant.TeamColors.Blue, new Car(100, 1, 1, false)));
            PrintSectionData(_track.Sections.First());
            _race.PlaceDrivers();
            PrintSectionData(_track.Sections.First());
            Assert.AreEqual(_race.GetSectionData(_track.Sections.First()).Left, _race.Participants[0]);
            Assert.AreEqual(_race.GetSectionData(_track.Sections.First()).Right, _race.Participants[1]);
            _race.Positions = new Dictionary<Section, SectionData>();
            _race.Participants.Remove(_race.Participants.Last());
            _race.Participants.Remove(_race.Participants.Last());
        }

        public void PrintSectionData(Section section)
        {
            Console.WriteLine($"left: {_race.GetSectionData(section).Left?.Name}, right: {_race.GetSectionData(section).Right?.Name}");
        }

    }
}
