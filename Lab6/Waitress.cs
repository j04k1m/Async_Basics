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
                Thread.Sleep(1000);
                if (GlobalVariables.BarOpen == false && MainWindow.DirtyGlassQueue.Count == 0 && MainWindow.DishesGlassQueue.Count == 0 && MainWindow.ShelfGlassQueue.Count == 8)
                {
                    GlobalVariables.WaitressLoad = false;
                    callBack("*Waitress Leaves*");
                }

                if (MainWindow.DirtyGlassQueue.Count == 0 && GlobalVariables.BarOpen == true)
                {
                    callBack("wait for glass");
                    Thread.Sleep(GlobalVariables.WaitSpeed);
                }

                if (MainWindow.DirtyGlassQueue.Count != 0)
                {
                    callBack("fetches " + MainWindow.DirtyGlassQueue.Count.ToString() + " dirty glass");
                    foreach (var glass in MainWindow.DirtyGlassQueue)
                    {
                        MainWindow.DirtyGlassQueue.Take();
                        MainWindow.DishesGlassQueue.Add(new Glass());
                    }
                    Thread.Sleep(10000);

                    callBack("dishes " + MainWindow.DishesGlassQueue.Count + " glass");
                    Thread.Sleep(15000);

                    callBack("puts " + MainWindow.DishesGlassQueue.Count + " glass on shelf");
                    foreach (var glass in MainWindow.DishesGlassQueue)
                    {
                        MainWindow.DishesGlassQueue.Take();
                        MainWindow.ShelfGlassQueue.Add(new Glass());
                    }
                    Thread.Sleep(GlobalVariables.WaitSpeed);

                }


            }
        }

    }
}
