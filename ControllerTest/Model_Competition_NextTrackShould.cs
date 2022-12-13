using Controller;
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
        private Competition Competition;
        [SetUp]
        public void SetUp()
        {
            Data.Initialize(false);
            Competition = Data.Competition;
            Competition.NextTrack();
            Competition.NextTrack();
            Competition.NextTrack();
        }
        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            Track result = null;
            Assert.IsNull(result);
            result = Competition.NextTrack();
           Assert.IsNull(result);
        }
        [Test]
        public void NextTrack_OneInQueue_ReturnTrack() 
        {
            Track newTrack = new Track("raceA1", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Finish });
            Competition.Tracks.Enqueue(newTrack);
            Track result = Competition.NextTrack();
            Assert.AreEqual(newTrack,result);
        }
        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            Track newTrack = new Track("raceA2", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.LeftCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Finish });
            Competition.Tracks.Enqueue(newTrack);
            Track result = Competition.NextTrack();
            result = Competition.NextTrack();
            Assert.IsNull(result);
        }
        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            Track track1 = new Track("raceA3", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Finish });
            Track track2 = new Track("raceA4", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Finish });
            Competition.Tracks.Enqueue(track1);
            Competition.Tracks.Enqueue(track2);
            Assert.AreEqual(Competition.NextTrack(), track1);
            Assert.AreEqual(Competition.NextTrack(), track2);
        }
    }
}
