using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6
{
    public class Bartender
    {
        public void Work(Action<string> callBack/*"Allt som ska skickas till main"*/)
        {
                while (GlobalVariables.BartenderLoad)
                {
                    if (MainWindow.BartenderQueue.Count == 0 && GlobalVariables.BartenderLoad == true)
                    {
                        callBack("waits for patron");
                        Thread.Sleep(GlobalVariables.WaitSpeed);
                    }

                    if (MainWindow.BartenderQueue.Count != 0)
                    {
                        callBack(MainWindow.BartenderQueue.First().name + " arrives");
                        Thread.Sleep(1000);
                    }

                    if(MainWindow.BartenderQueue.Count != 0 && MainWindow.ShelfGlassQueue.Count != 0)
                    {
                    callBack("fetches glass to " + MainWindow.BartenderQueue.First().name);
                    Thread.Sleep(GlobalVariables.WaitSpeed);
                    MainWindow.ShelfGlassQueue.TryTake(out Glass a);

                    callBack("pours beer to " + MainWindow.BartenderQueue.First().name);
                    MainWindow.PatronQueue.Enqueue(new Patron(MainWindow.BartenderQueue.First().name));
                    MainWindow.BartenderQueue.TryDequeue(out Patron p);
                    Thread.Sleep(GlobalVariables.WaitSpeed);
                    }

                    if (MainWindow.BartenderQueue.Count != 0 && MainWindow.ShelfGlassQueue.Count == 0)
                    {
                        callBack("waits for glass");
                        Thread.Sleep(GlobalVariables.WaitSpeed);

                        if (MainWindow.BartenderQueue.Count != 0 && MainWindow.ShelfGlassQueue.Count != 0)
                            {
                            callBack("gets glass");
                            Thread.Sleep(GlobalVariables.WaitSpeed);
                            MainWindow.ShelfGlassQueue.TryTake(out Glass kds);

                            callBack("pours beer to " + MainWindow.BartenderQueue.First().name);
                            MainWindow.PatronQueue.Enqueue(new Patron(MainWindow.BartenderQueue.First().name));
                            MainWindow.BartenderQueue.TryDequeue(out Patron asdp);
                            Thread.Sleep(GlobalVariables.WaitSpeed);
                            }
                    }

                    if (GlobalVariables.BarOpen == false && MainWindow.PatronQueue.Count == 0 && MainWindow.BartenderQueue.Count == 0)
                    {
                        GlobalVariables.BartenderLoad = false;
                        callBack("*Bartender Leaves*");
                    }
                }
        }

    }
}
