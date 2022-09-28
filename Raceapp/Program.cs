using Controller;
using Model;
using System;

namespace Raceapp // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize(new Competition());
            Data.NextRace();
            Race race = new Race(Data.CurrentRace, Data._competition.participants);
            
            Visualisatie.Initialize();
            Visualisatie.DrawTrack(Data.CurrentRace);
            for (; ; )
            {
                Thread.Sleep(3);
            }


            //Data.NextRace();
            //Visualisatie.Initialize();
            //Visualisatie.DrawTrack(Data.CurrentRace);
        }
    }
}