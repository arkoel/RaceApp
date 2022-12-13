using Controller;
using Model;
using System;
using System.Runtime.CompilerServices;
using ControllerTest;

namespace Raceapp // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {

            Data.Initialize(false);

            Data.NextRace();

            Visualisatie.Initialize();
            
            for (; ; )
            {
                Thread.Sleep(300_000_000);
            }

            /* FOR THE UNIT TESTS
            Controller_Race_CheckFullSection controller_Race_CheckFullSection = new Controller_Race_CheckFullSection();
            controller_Race_CheckFullSection.SetUp();
            Console.WriteLine("----------------");
            Controller_Race_PlaceDrivers controller_Race_PlaceDrivers = new Controller_Race_PlaceDrivers();
            controller_Race_PlaceDrivers.SetUp();
            */

        }
    }
}