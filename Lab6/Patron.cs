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
        public string name { get; set; }

        //Constructor
        public Patron(string s)
        {
            name = s;
        }
        public Patron() {}

        public void Work(Action<string> callBack/*"Allt som ska skickas till main"*/)
        {

            Task.Run(() =>
            {

                    Thread.Sleep(1000);
                    while (MainWindow.ChairQueue.Count == 0)
                    {
                        callBack(MainWindow.PatronQueue.First().name + " waits for chair");
                        Thread.Sleep(4000); //test 4: waitspeed = 8000
                    }

                    Thread.Sleep(4000); //test 4: waitspeed = 8000
                    MainWindow.ChairQueue.Take();
                    callBack(MainWindow.PatronQueue.First().name + " sits down");

                    //Finishes beer Queue
                    //FinishBeerQueue Enqueue
                    MainWindow.FinishesBeerQueue.Enqueue(new Patron(MainWindow.PatronQueue.First().name));
                    //PatronQueue Dequeue
                    MainWindow.PatronQueue.TryDequeue(out Patron d);

                    Thread.Sleep(GlobalVariables.rand.Next(10000, 20000)); //test 4: waitspeed = 6000, 40000
                    MainWindow.ChairQueue.Add(new Chair());
                    MainWindow.DirtyGlassQueue.Add(new Glass());
                    callBack(MainWindow.FinishesBeerQueue.First().name + " finishes beer and leaves");
                    MainWindow.FinishesBeerQueue.TryDequeue(out Patron smk);

            });
                
                    
               
        }

    }
}
