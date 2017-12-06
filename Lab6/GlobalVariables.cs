using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Lab6
{
    class GlobalVariables
    {
        public static bool BarOpen = true;
        
        public static bool BouncerLoad = true;
        public static bool PatronLoad = true;
        public static bool BartenderLoad = true;
        public static bool WaitressLoad = true;
        public static bool ClosedLoad = true;

        public static int WaitSpeed = 3000;
        public static int FetchSpeedX2 = 5000;
        public static int DishSpeedX2 = 7500;

        public static int EmptyGlass = 0;
        public static int CleanGlass = 0;

        public static int ShelfGlass = 8; //standard == 8
        public static int Chair = 9; //standard == 9

        // test 1: standard
        // test 2: 20 glas 3 stolar
        // test 3: 20 stolar 5 glas
        // test 4: dubbel tid Patron
        // test 5: diskar/plockar glas dubbel speed
        // test 6: bar öppen 5*60 sec
        // test 7: Couples Night
        // test 8: BussLoad

        public static int TimerSeconds = 120;

        public static bool BussLoad = false;

        public static Random rand = new Random();

    }
}
