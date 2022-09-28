using Controller;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine;
using Model;
using System.Security.Cryptography.X509Certificates;

namespace Raceapp
{
    public static class Visualisatie
    {
        #region graphics
        private static string[] _startHorizontal = { "         ", "---------", "    _0   ", "         ", "    _0   ", "---------", "       " };
        private static string[] _finishHorizontal = { "         ", "---------", "    #_   ", "    #    ", "    #_   ", "---------", "         " };
        private static string[] _finishVertical = { " |     | ", " |     | ", " | _ _ | ", " |#####| ", " |     | ", " |     | ", " |     | " };
        private static string[] _straightHorizontal = { "         ", "---------", "    _    ", "         ", "    _    ", "---------", "       " };
        private static string[] _straightVertical = { " |     | ", " |     | ", " |     | ", " | _ _ | ", " |     | ", " |     | ", " |     | " };
        private static List<string[]> _turn = new List<string[]>();

        #endregion

        public static void Initialize()
        {
            _turn.Add(new string[] { "         ", " /---------", " |       ", " | _     ", " |   _   ", " |     /-", " |     | " });
            _turn.Add(new string[] { "       ", "-------\\ ", "       | ", "    _  | ", "   _   | ", "-\\     | ", " |     | " });
            _turn.Add(new string[] { " |     | ", "-/     | ", "   _   | ", "     _ | ", "       | ", "-------/ ", "         " });
            _turn.Add(new string[] { " |     | ", " |     \\-", " |   _   ", " |  _    ", " |       ", " \\-------", "         " });
        }
        public static void DrawTrack(Track track)
        {
            Console.WriteLine(track.Name);
            int[] coordinate = { 27, 1 };

            string[] sectiontxt = new string[7];
            //bool[] dir = { true, true }; //[up, right]
            int dir = 0;
            Console.BackgroundColor = ConsoleColor.Blue;
            foreach (Section section in track.Sections)
            {
                switch (section.SectionType)
                {
                    case SectionTypes.StartGrid:
                        sectiontxt = _startHorizontal;
                        break;
                    case SectionTypes.Finish:
                        if (dir % 2 == 1)
                        {
                            sectiontxt = _finishHorizontal;
                        }
                        else
                        {
                            sectiontxt = _finishVertical;
                        }
                        break;
                    case SectionTypes.Straight:
                        if (dir % 2 == 1)
                        {
                            sectiontxt = _straightHorizontal;
                        }
                        else
                        {
                            sectiontxt = _straightVertical;
                        }
                        break;
                    case SectionTypes.LeftCorner:
                        sectiontxt = _turn[dir + 1 > 3 ? 0 : dir + 1];
                        dir -= 1;
                        if (dir < 0) { dir = 3; }
                        break;
                    case SectionTypes.RightCorner:
                        sectiontxt = _turn[dir];
                        dir += 1;
                        if (dir > 3) { dir = 0; }
                        break;
                    default:
                        sectiontxt = _finishHorizontal;
                        break;
                }
                // Console.WriteLine(section.SectionType);
                int n = 0;
                foreach (string woord in sectiontxt)
                {
                    Console.SetCursorPosition(coordinate[0], coordinate[1] + n);
                    Console.Write(woord);
                    n++;
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


        public static string GiveCharDriver(string baanstuk, SectionData.IParticipant participant1, SectionData.IParticipant participant2) 
        {
            int index1 = Data._competition.participants.IndexOf(participant1);
            int index2 = Data._competition.participants.IndexOf(participant2);
            if (index1 != -1) {baanstuk = baanstuk.Replace('_', ((char)index1)); }
            if (index2 != -1) {baanstuk = baanstuk.Replace('_', ((char)index2)); }
            return baanstuk;  
        }


    }
}
