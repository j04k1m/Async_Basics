using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Lab6
{
    public class Patron
    {
        //Constructor
        public string name { get; set; }

        public Patron(string s)
        {
            name = s;
        }
        public Patron() {}

        public void Work(Action<string> callBack/*"Allt som ska skickas till main"*/)
        {
                while (GlobalVariables.PatronLoad)
                {
                    if(MainWindow.PatronQueue.Count != 0 && MainWindow.ChairQueue.Count == 0)
                    {
                        callBack(MainWindow.PatronQueue.First().name + " waits for chair");
                        Thread.Sleep(4000); //test 4: waitspeed = 8000
                    }
                    
                    if (MainWindow.PatronQueue.Count != 0 && MainWindow.ChairQueue.Count != 0 /*&& MainWindow.ChairQueue.Count != 0*/)
                    {
                        callBack(MainWindow.PatronQueue.First().name + " looks for chair");
                        Thread.Sleep(4000); //test 4: waitspeed = 8000

                        MainWindow.ChairQueue.TryTake(out Chair l);
                        callBack(MainWindow.PatronQueue.First().name + " sits down and drinks beer");
                        Thread.Sleep(GlobalVariables.rand.Next(3000, 20000)); //test 4: waitspeed = 6000, 40000

                        MainWindow.ChairQueue.TryAdd(new Chair());
                        MainWindow.EmptyGlassQueue.Add(new Glass());
                        callBack(MainWindow.PatronQueue.First().name + " leaves bar");
                        MainWindow.PatronQueue.TryDequeue(out Patron adl);
                        Thread.Sleep(GlobalVariables.WaitSpeed);
                    }
                }
        }

    }
}
