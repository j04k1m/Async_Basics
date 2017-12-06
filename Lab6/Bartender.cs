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
        public void Work(Action<string> callBack, Action<string> PatronListCallBack/*"Allt som ska skickas till main"*/)
        {
                while (GlobalVariables.BartenderLoad)
                {
                    Thread.Sleep(GlobalVariables.WaitSpeed);
                    if (MainWindow.BartenderQueue.Count == 0 && GlobalVariables.BartenderLoad == true)
                    {
                        callBack("waits for patron");
                        Thread.Sleep(GlobalVariables.WaitSpeed);
                    }

                    if(MainWindow.BartenderQueue.Count != 0 && MainWindow.ShelfGlassQueue.Count != 0)
                    {
                    Thread.Sleep(1000);
                    callBack("fetches glass to " + MainWindow.BartenderQueue.First().name);
                    MainWindow.ShelfGlassQueue.Take();
                    Thread.Sleep(GlobalVariables.WaitSpeed);

                    Thread.Sleep(1000);
                    callBack("pours beer to " + MainWindow.BartenderQueue.First().name);
                    Thread.Sleep(GlobalVariables.WaitSpeed);
                    MainWindow.PatronQueue.Enqueue(new Patron(MainWindow.BartenderQueue.First().name));
                    MainWindow.PatronQueue.FirstOrDefault().Work(PatronListCallBack);
                    MainWindow.BartenderQueue.TryDequeue(out Patron p);
                    }

                    if (MainWindow.BartenderQueue.Count != 0 && MainWindow.ShelfGlassQueue.Count == 0)
                    {
                        callBack("waits for glass");
                        Thread.Sleep(GlobalVariables.WaitSpeed);

                        if (MainWindow.BartenderQueue.Count != 0 && MainWindow.ShelfGlassQueue.Count != 0)
                            {
                            callBack("get glass and pours beer to " + MainWindow.BartenderQueue.First().name);
                            MainWindow.ShelfGlassQueue.Take();
                            MainWindow.PatronQueue.Enqueue(new Patron(MainWindow.BartenderQueue.First().name));
                            MainWindow.BartenderQueue.TryDequeue(out Patron asdp);
                            Thread.Sleep(GlobalVariables.WaitSpeed);
                            }
                }
                    
                    if (GlobalVariables.BarOpen == false && MainWindow.PatronQueue.Count == 0 && MainWindow.BartenderQueue.Count == 0 /***test***/ && MainWindow.FinishesBeerQueue.Count == 0)
                    {
                        GlobalVariables.BartenderLoad = false;
                        callBack("*Bartender Leaves*");
                    }
                }
        }

    }
}
