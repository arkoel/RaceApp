using Controller;
using Model;
using System.Diagnostics;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

namespace Raceapp
{
    public static class Visualisatie
    {
        #region graphics
        private static string[] _startHorizontal = { "         ", "---------", "    _:   ", "         ", "    ~:   ", "---------", "         " };
        private static string[] _finishHorizontal = { "         ", "---------", "    #_   ", "    #    ", "    #~   ", "---------", "         " };
        private static string[] _finishVertical = { " |     | ", " |     | ", " | _ ~ | ", " |#####| ", " |     | ", " |     | ", " |     | " };
        private static string[] _straightHorizontal = { "         ", "---------", "    _    ", "         ", "    ~    ", "---------", "             " };
        private static string[] _straightVertical = { " |     | ", " |     | ", " |     | ", " | _ ~ | ", " |     | ", " |     | ", " |     | " };
        private static List<string[]> _turn = new List<string[]>();
        private static Race currentRace;

        #endregion
        public static void Initialize()
        {
            _turn.Add(new string[] { "         ", " /---------", " |       ", " | _     ", " |   ~   ", " |     /-", " |     | " });
            _turn.Add(new string[] { "         ", "-------\\ ", "       | ", "    _  | ", "   ~   | ", "-\\     | ", " |     | " });
            _turn.Add(new string[] { " |     | ", "-/     | ", "   _   | ", "     ~ | ", "       | ", "-------/ ", "         " });
            _turn.Add(new string[] { " |     | ", " |     \\-", " |   _   ", " |  ~    ", " |       ", " \\-------", "         " });
            Data.NexttrackEvent+=OnNextTrackEvent;
            Data.NexttrackEvent.Invoke();
        }

        /// <summary>
        /// Draws track on console
        /// </summary>
        /// <param name="track"></param>
        public static void DrawTrack(Track track)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Red;
            int[] coordinate = { 50, 1 };
            int dir = 1;
            string[] sectiontxt = new string[7];
            foreach (Section section in track.Sections)
            {
                switch (section.SectionType)
                {
                    case SectionTypes.StartGrid:
                        sectiontxt = _startHorizontal;
                        break;
                    case SectionTypes.Finish:
                        if (dir % 2 == 1)
                            sectiontxt = _finishHorizontal;
                        else
                            sectiontxt = _finishVertical;
                        break;
                    case SectionTypes.Straight:
                        if (dir % 2 == 1)
                            sectiontxt = _straightHorizontal;
                        else
                            sectiontxt = _straightVertical;
                        break;
                    case SectionTypes.LeftCorner:
                        sectiontxt = _turn[dir + 1 > 3 ? 0 : dir + 1];
                        dir--;
                        if (dir < 0) { dir = 3; }
                        break;
                    case SectionTypes.RightCorner:
                        sectiontxt = _turn[dir];
                        dir++;
                        if (dir > 3) { dir = 0; }
                        break;
                    default:
                        sectiontxt = _finishHorizontal;
                        break;
                }
                int offset = 0;
                foreach (string woord in sectiontxt)
                {
                    if (coordinate[0]>0 && coordinate[1]+offset>0)
                        Console.SetCursorPosition(coordinate[0], coordinate[1] + offset);
                    Console.Write(GiveCharDriver(woord, Data.CurrentRace.GetSectionData(section).Left, Data.CurrentRace.GetSectionData(section).Right));
                    offset++;
                }
                switch (dir)
                {
                    case 0:
                        coordinate[1] -= 7;
                        break;
                    case 1:
                        coordinate[0] += 9;
                        break;
                    case 2:
                        coordinate[1] += 7;
                        break;
                    case 3:
                        coordinate[0] -= 9;
                        break;
                }
                Console.SetCursorPosition(coordinate[0], coordinate[1]);
            }
        }

        /// <summary>
        /// Adds Driver-icons to String of section
        /// </summary>
        /// <param name="baanstuk"></param>
        /// <param name="participant1"></param>
        /// <param name="participant2"></param>
        /// <returns></returns>
        public static string GiveCharDriver(string baanstuk, SectionData.IParticipant participant1, SectionData.IParticipant participant2) 
        {
            char Char1 = ' ';
            char Char2 = ' ';
            if (participant1 is not null)
            {
                int index1 = Data.Competition.Participants.IndexOf(participant1);
                Char1 = (participant1.Equipment.IsBroken) ? '*' : (index1.ToString()[0]);
            }
            if (participant2 is not null)
            {
                int index2 = Data.Competition.Participants.IndexOf(participant2);
                Char2 = (participant2.Equipment.IsBroken) ? '*' : (index2.ToString()[0]);
            }
            
            baanstuk = baanstuk.Replace('_', Char1);
            baanstuk = baanstuk.Replace('~', Char2);
            
            return baanstuk;  
        }

        public static void OnDriversChanged(object source, DriversChangedEventArgs driversChangedEventArgs)
        {
            DrawTrack(driversChangedEventArgs.Track);
        }

        public static void OnNextTrackEvent()
        {
            Console.Clear();
            currentRace = Data.CurrentRace;
            currentRace.DriversChanged += OnDriversChanged;
            DrawTrack(Data.CurrentRace.Track); //to show starting position
        }
    }
}
