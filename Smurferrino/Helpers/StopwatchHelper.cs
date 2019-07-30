using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smurferrino.Helpers
{
    public class StopwatchHelper
    {
        public static int iterate = 0;
        public static float value = 0;
        public static float average => (value / iterate);
    }
}
