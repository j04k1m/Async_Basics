using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6
{
    public class Klose
    {
        public void Work(Action f, Action<string> a, Action<string> b, Action<string> c)
        {
            while (GlobalVariables.ClosedLoad)
                if (GlobalVariables.BarOpen == false && GlobalVariables.BouncerLoad == false && GlobalVariables.BartenderLoad == false && GlobalVariables.WaitressLoad == false)
                {
                    f();
                    Thread.Sleep(500);
                    a("*Bar closed*");
                    b("*Bar closed*");
                    c("*Bar closed*");
                    
                    GlobalVariables.ClosedLoad = false;
                }
        }
    }
}
