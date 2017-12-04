using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Lab6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random SleepRandom = new Random();
        DispatcherTimer ClockTimer = new DispatcherTimer();
        
        Bouncer bounce = new Bouncer();
        Patron customer = new Patron();
        Bartender bt = new Bartender();
        Waitress waits = new Waitress();

        public static BlockingCollection<Glass> EmptyGlassQueue = new BlockingCollection<Glass>();
        public static BlockingCollection<Glass> CleanGlassQueue = new BlockingCollection<Glass>();
        public static BlockingCollection<Glass> ShelfGlassQueue = new BlockingCollection<Glass>();

        public static BlockingCollection<Chair> ChairQueue = new BlockingCollection<Chair>();

        public static ConcurrentQueue<string> BouncerQueue = new ConcurrentQueue<string>();
        public static ConcurrentQueue<Patron> PatronQueue = new ConcurrentQueue<Patron>();
        public static ConcurrentQueue<Patron> BartenderQueue = new ConcurrentQueue<Patron>();

        //Timer/Clock
        public void Clock()
        {
            ClockTimer.Tick += Clock_Tick;
            ClockTimer.Interval = new TimeSpan(0, 0, 1);
            ClockTimer.Start();
        }
        public void Clock_Tick(object sender, EventArgs e)
        {
            GlobalVariables.TimerSeconds--;
            LabelShowClock.Content = string.Format("Closing Time: " + GlobalVariables.TimerSeconds);

            if (GlobalVariables.TimerSeconds == 0)
            {
                ClockTimer.Stop();
                GlobalVariables.BarOpen = false;
            }

            if (GlobalVariables.TimerSeconds == 40)
            {
                GlobalVariables.BussLoad = true;
            }
        }
        
        public MainWindow()
        {
            InitializeComponent();
        }


        private void ButtonStartAll_Click(object sender, RoutedEventArgs e)
        {
            ButtonStartAll.IsEnabled = false;
            FillChairQueue();
            FillEmptyGlassQueue();
            FillCleanGlassQueue();
            FillShelfGlassQueue();

            Clock();

            Task.Run(() =>
            {
                bounce.Work(FillPatronListBox);
            });

            Task.Run(() =>
            {
                customer.Work(FillPatronListBox);
            });
                
            Task.Run(() =>
            {
                bt.Work(FillBartenderListBox);
            });

            Task.Run(() =>
            {
                waits.Work(FillWaitressListBox);
            });

        }

        //Funktioner
        private void ClearAllListBoxes()
        {
            ListboxBartender.Items.Clear();
            ListboxPatron.Items.Clear();
            ListboxWaitress.Items.Clear();
        }

        public void FillBartenderListBox(string s)
        {
                Dispatcher.Invoke(() =>
                {
                    TextBoxAllPatrons.Text = "Patrons " + (PatronQueue.Count + BartenderQueue.Count).ToString();
                    ListboxBartender.Items.Insert(0, s);
                });
        }

        public void FillPatronListBox(string s)
        {

                Dispatcher.Invoke(() =>
                {
                    ListboxPatron.Items.Insert(0, s);
                    TextBoxTables.Text = "Chairs: " + ChairQueue.Count.ToString();
                });
        }

        public void FillWaitressListBox(string s)
        {
              Dispatcher.Invoke(() =>
                {
                    TextBoxEmptyGlass.Text = "EmptyGlass: " + EmptyGlassQueue.Count.ToString();
                    TextBoxCleanGlass.Text = "CleanGlass: " + CleanGlassQueue.Count.ToString();
                    TextBoxShelfGlass.Text = "ShelfGlass: " + ShelfGlassQueue.Count.ToString();
                    ListboxWaitress.Items.Insert(0,  s);
                });
        }

        public void FillEmptyGlassQueue()
        {
            for(int x = 0; x <GlobalVariables.EmptyGlass; x++)
            {
                MainWindow.EmptyGlassQueue.Add(new Glass());
            }
        }

        public void FillCleanGlassQueue()
        {
            for (int x = 0; x < GlobalVariables.CleanGlass; x++)
            {
                MainWindow.CleanGlassQueue.Add(new Glass());
            }
        }

        public void FillShelfGlassQueue()
        {
            for (int x = 0; x < GlobalVariables.ShelfGlass; x++)
            {
                MainWindow.ShelfGlassQueue.Add(new Glass());
            }
        }

        public void FillChairQueue()
        {
            for(int x = 0; x < GlobalVariables.Chair; x++)
            {
                MainWindow.ChairQueue.Add(new Chair());
            }
        }

    }
}
