using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using Controller;
using Model;
using WPF;
using WPF2.DataContext;

namespace WPF2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WindowCompitition windowCompitition;
        private WindowRace windowRace;
        public MainWindow()
        {
            InitializeComponent();
            Data.Initialize(true);
            Data.NextRace();
            WPF.Visualisatie.Initialize();
            Data.NexttrackEvent += OnTrackChanged;
            Data.NexttrackEvent.Invoke();
        }

        public void OnTrackChanged()
        {
            Visualisatie.currentRace = Data.CurrentRace;
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Dispatcher?.BeginInvoke(() => { Data.CurrentRace.DriversChanged += ((Data_Context)DataContext).OndriversChanged;});
            
            Visualisatie.CalculateTrackSize(Data.CurrentRace.Track);
      
        }

        /// <summary>
        /// function to subscribe all events to Competition
        /// </summary>
        public void SubToCompititionStat()
        {
            Competition_Data_Context competition_Data_Context = (Competition_Data_Context)windowCompitition.DataContext;
            Data.NexttrackEvent += competition_Data_Context.OnNextRace;
            Data.CurrentRace.DriversChanged += competition_Data_Context.OnDriversChanged;
        }

        /// <summary>
        /// function to subscribe all events to Race
        /// </summary>
        public void SubToRaceStat()
        {
            Race_Data_Context race_Data_Context = (Race_Data_Context)windowRace.DataContext;
            Data.NexttrackEvent += race_Data_Context.OnNextRace;
            Data.CurrentRace.DriversChanged += race_Data_Context.OnDriversChanged;
        }

        public void OnDriversChanged(object? sender, DriversChangedEventArgs e)
        {
            this.Track.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    this.Track.Source = null;
                    this.Track.Source = WPF.Visualisatie.DrawTrack(e.Track);
                }
                ));
        }           

        
        

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_OpenCompition_Click(object sender, RoutedEventArgs e)
        {
            
            windowCompitition = new WindowCompitition();
            SubToCompititionStat();
            windowCompitition.Show();
        }

        private void MenuItem_OpenStats_Click(object sender, RoutedEventArgs e)
        {
            windowRace = new WindowRace();
            SubToRaceStat();
            windowRace.Show();
        }
    }
}
