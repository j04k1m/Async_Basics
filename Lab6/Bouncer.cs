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
    public class Bouncer
    {
        public void Work(Action<string> callBack/*"Allt som ska skickas till main"*/)
        {
            Random RandomNumber = new Random();
            //Task.Run(() =>
            //{
                   //MainWindow.BartenderQueue.Enqueue(new Patron("RandomName"));
                    
                    while (GlobalVariables.BouncerLoad)
                    {   
                        int n = RandomNumber.Next(0, 14);
                        int a = RandomNumber.Next(4, 11);
                        string RandomName = names.ElementAt(n);

                        MainWindow.BartenderQueue.Enqueue(new Patron(RandomName));
                        callBack(RandomName + " enters bar");
                        Thread.Sleep(RandomNumber.Next(3000, 10000)); //BussLoad wait time: 6000, 20000

                        if (GlobalVariables.BarOpen == false)
                        {
                            GlobalVariables.BouncerLoad = false;
                            callBack("*Bouncer Leaves*");
                        }

                        /* DOUBLES Night
                        string RandomName2 = names.ElementAt(a);
                        MainWindow.PatronQueue.Enqueue(new Patron(RandomName));
                        callBack(RandomName2 + " Enters bar"); 
                        */

                        /* BUSLOAD
                        if (GlobalVariables.BussLoad)
                        {
                            int y = RandomNumber.Next(4, 11);
                            string BussPatronName = names.ElementAt(y);

                            int Id = 0;
                            while(Id <= 15)
                            {
                                MainWindow.BartenderQueue.Enqueue(new Patron(BussPatronName + Id));
                                callBack(BussPatronName + (Id).ToString() + " Enters bar");
                                Id++;
                            }
                             GlobalVariables.BussLoad = false;
                        }
                        */
                    }

        }

        List<string> names = new List<string>()
            {
                "Bob", "Steve", "Stan", "Dope", "Calvin", "Xan", "Pedro",
                "Vlad", "Pekka", "Boris", "Anna", "Max", "Dennis", "Sara", "Tyrone"
            };
        



    }
}
