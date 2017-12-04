using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6
{
    public class Waitress
    {

        public void Work(Action<string> callBack /*"Allt som ska skickas till main"*/)
        {
                while (GlobalVariables.WaitressLoad)
                {

                    if (MainWindow.EmptyGlassQueue.Count != 0)
                    {
                        callBack("fetches empty/dirty glass");
                        Thread.Sleep(10000);

                        callBack("dishes glass");
                        Thread.Sleep(15000);
                        MainWindow.EmptyGlassQueue.TryTake(out Glass glassd);
                        MainWindow.CleanGlassQueue.TryAdd(new Glass());

                        callBack("puts glass on shelf");
                        MainWindow.ShelfGlassQueue.TryAdd(new Glass());
                        MainWindow.CleanGlassQueue.TryTake(out Glass glassr);
                        Thread.Sleep(GlobalVariables.WaitSpeed);
                    }
                    

                    if(GlobalVariables.BarOpen == false && MainWindow.ShelfGlassQueue.Count == 8 && MainWindow.EmptyGlassQueue.Count == 0)
                    {
                        GlobalVariables.WaitressLoad = false;
                        callBack("*Waitress Leaves*");
                    }

                    if(MainWindow.PatronQueue.Count != 0 && MainWindow.EmptyGlassQueue.Count == 0)
                    {
                        callBack("wait for glass");
                        Thread.Sleep(GlobalVariables.WaitSpeed);
                    }
                }
        }

    }
}
