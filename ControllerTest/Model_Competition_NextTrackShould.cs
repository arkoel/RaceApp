using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerTest
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
        private Competition _competition;
        [SetUp]
        public void SetUp()
        {
        _competition = new Competition();
        }
        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            Track result = _competition.NextTrack();
            Assert.IsNull(result);
        }
        [Test]
        public void NextTrack_OneInQueue_ReturnTrack() 
        {
            Track newTrack = new Track("raceA1", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Finish });
            _competition.Tracks.Enqueue(newTrack);
            Track result = _competition.NextTrack();
            Assert.AreEqual(newTrack,result);
        }
        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            Track newTrack = new Track("raceA2", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.LeftCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Finish });
            _competition.Tracks.Enqueue(newTrack);
            Track result = _competition.NextTrack();
            result = _competition.NextTrack();
            Assert.IsNull(result);
        }
        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            Track track1 = new Track("raceA3", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Finish });
            Track track2 = new Track("raceA4", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Finish });
            _competition.Tracks.Enqueue(track1);
            _competition.Tracks.Enqueue(track2);
            Assert.AreEqual(_competition.NextTrack(), track1);
            Assert.AreEqual(_competition.NextTrack(), track2);
        }
    }
}
