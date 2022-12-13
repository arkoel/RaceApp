using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using Controller;
using Model;
using static Model.SectionData;

namespace WPF
{
    public static class Visualisatie
    {
        public static Race currentRace { get; set; }
        private static int TrackWidth = 0;
        private static int TrackHeight = 0;
        private static int start_x = 0;
        private static int start_y = 0;
        #region url's
        const string Car_Blue = ".\\Graphics\\Car_Blue.png";
        const string Car_Blue_Broken = ".\\Graphics\\Car_Blue_Broken.png";
        const string Car_Gray = ".\\Graphics\\Car_Gray.png";
        const string Car_Gray_Broken = ".\\Graphics\\Car_Gray_Broken.png";
        const string Car_Green = ".\\Graphics\\Car_Green.png";
        const string Car_Green_Broken = ".\\Graphics\\Car_Green_Broken.png";
        const string Car_Red = ".\\Graphics\\Car_Red.png";
        const string Car_Red_Broken = ".\\Graphics\\Car_Red_Broken.png";
        const string Car_Yellow = ".\\Graphics\\Car_Yellow.png";
        const string Car_Yellow_Broken = ".\\Graphics\\Car_Yellow_Broken.png";
        const string _finishHorizontal = ".\\Graphics\\FinishHorisontal.png";
        const string _startHorizontal = ".\\Graphics\\StartHorizontal.png";
        const string _straightHorizontal = ".\\Graphics\\StraightHorisontal.png";
        const string _straightVertical = ".\\Graphics\\StraightVertical.png";
        static string[] _turn = { ".\\Graphics\\Turn_0.png", ".\\Graphics\\Turn_1.png", ".\\Graphics\\Turn_2.png", ".\\Graphics\\Turn_3.png" };
        #endregion

        public static void Initialize()
        {

        }

        /// <summary>
        /// Draws track with drivers
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        public static BitmapSource DrawTrack(Model.Track track)
        {
            string filelocation = Directory.GetParent(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString()+"\\";
            Bitmap emptybitmap = ImageLoader.MakeEmptyImage(1440, 900);
            Graphics graphics = Graphics.FromImage(emptybitmap);
            //add tracks
            for (int i = 0; i < track.Sections.Count; i++)
            {
                (int[] Coördinates, string Source, int Direction, int Afstand, bool IsLeft) = GetCoordinateFromSection(track, i);
                graphics.DrawImage(Image.FromFile(filelocation + Source), Coördinates[0] * 180 + 180, Coördinates[1] * 180 + 180, 180, 180);
            }
            //done adding tracks
            //add drivers
            foreach (SectionData.IParticipant participant in currentRace.Participants)
            {
                (int[] Coördinates, string Source, int Direction, int Afstand, bool IsLeft) = GetCoordinateFromSection(track, int.MaxValue, participant);
                if (Afstand >= 0)
                {
                    Image DriverImage = Image.FromFile(filelocation + "\\Graphics\\Car_" + participant.Teamcolor + (participant.Equipment.IsBroken ? "_Broken" : "") + ".png");
                    RotateFlipType Rotation = RotateFlipType.RotateNoneFlipNone;
                    int x = 0;
                    int y = 0;
                    switch (Direction)
                    {
                        case 0:
                            Rotation = RotateFlipType.Rotate270FlipNone;
                            x = IsLeft ? 20 : 110;
                            y = 90 - Afstand;
                            break;
                        case 1:
                            Rotation = RotateFlipType.RotateNoneFlipNone;
                            x = Afstand + 90;
                            y = IsLeft ? 20 : 110;
                            break;
                        case 2:
                            Rotation = RotateFlipType.Rotate90FlipNone;
                            x = IsLeft ? 110 : 20;
                            y = Afstand + 90;
                            break;
                        case 3:
                            Rotation = RotateFlipType.Rotate180FlipNone;
                            x = 90 - Afstand;
                            y = IsLeft ? 110 : 20;
                            break;
                    }
                    DriverImage.RotateFlip(Rotation);
                    x += (Coördinates[0] + 1) * 180;
                    y += (Coördinates[1] + 1) * 180;
                    graphics.DrawImage(DriverImage, x, y, Direction % 2 == 1 ? 50 : 25, Direction % 2 == 1 ? 25 : 50);
                }
            }
            //done adding drivers
            return ImageLoader.CreateBitmapSourceFromGdiBitmap(emptybitmap);
        }

        /// <summary>
        /// Calculates with and height of track
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        public static void CalculateTrackSize(Model.Track track)
        {
            int max_x = 0;
            int min_x = 0;
            int max_y = 0;
            int min_y = 0;
            int x;
            int y;

            for (int i = 0; i < track.Sections.Count; i++)
            {
                (int[] Coördinates, string Source, int Direction, int Afstand, bool IsLeft) = GetCoordinateFromSection(track, int.MaxValue);
                x = Coördinates[0];
                y = Coördinates[1];
                if (x > max_x) max_x = x;
                if (y > max_y) max_y = y;
                if (x < min_x) min_x = x;
                if (y < min_y) min_y = y;
            }
            start_x = Math.Abs(min_x);
            start_y = Math.Abs(min_y);
            TrackHeight = max_y - min_y + 1;
            TrackWidth = max_x - min_x + 1;
        }

        /// <summary>
        /// gets coordinate form section with an index.
        /// Het returnt Coordinates, Source, Direction, Afstand en of de participant links is;
        /// </summary>
        /// <param name="track"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static (int[], string, int, int, bool) GetCoordinateFromSection(Model.Track track, int index, SectionData.IParticipant participant)
        {
            int[] coordinate = { start_x, start_y };
            int dir = 1;
            string source = "";
            int currentIndex = 0;
            foreach (Section section in track.Sections)
            {
                switch (section.SectionType)
                {
                    case SectionTypes.StartGrid:
                        source = _startHorizontal;
                        break;
                    case SectionTypes.Finish:
                        if (dir % 2 == 1)
                        {
                            source = _finishHorizontal;
                        }

                        break;
                    case SectionTypes.Straight:
                        if (dir % 2 == 1)
                        {
                            source = _straightHorizontal;
                        }
                        else
                        {
                            source = _straightVertical;
                        }
                        break;
                    case SectionTypes.LeftCorner:
                        source = _turn[dir + 1 > 3 ? 0 : dir + 1];
                        dir--;
                        if (dir < 0) { dir = 3; }
                        break;
                    case SectionTypes.RightCorner:
                        source = _turn[dir];
                        dir++;
                        if (dir > 3) { dir = 0; }
                        break;
                    default:
                        source = _finishHorizontal;
                        break;
                }

                if (currentIndex == index)
                    return (coordinate, source, dir, 0, false);
                SectionData sectionData = currentRace.GetSectionData(section);
                if (sectionData.Left == participant && participant is not null)
                    return (coordinate, source, dir, sectionData.DistanceLeft, true);
                else if (sectionData.Right == participant && participant is not null)
                    return (coordinate, source, dir, sectionData.DistanceRight, false);

                switch (dir)
                {
                    case 0:
                        coordinate[1]--;
                        break;
                    case 1:
                        coordinate[0]++;
                        break;
                    case 2:
                        coordinate[1]++;
                        break;
                    case 3:
                        coordinate[0]--;
                        break;
                }
                currentIndex++;
            }

            return (coordinate, source, dir, -1, false);
        }
        private static (int[], string, int, int, bool) GetCoordinateFromSection(Model.Track track, int index)
        {
            return GetCoordinateFromSection(track, index, null);
        }
    }
}
