using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Smurferrino.Enums;

namespace Smurferrino.Helpers
{
    public static class ThreadSleep
    {
        public static ComputerSpec Specs { get; set; } = ComputerSpec.Normal;

        private static Dictionary<string, int> LowSpec = new Dictionary<string, int>()
        {
            {"Glow",  10},
            {"Trigger", 10 },
            {"Bunny", 5 },
            {"Radar", 300 },
            {"RCS", 10},
        };

        private static readonly Dictionary<string, int> NormalSpec = new Dictionary<string, int>()
        {
            {"Glow",  3},
            {"Trigger", 5 },
            {"Bunny", 2 },
            {"Radar", 75 },
            {"RCS", 5},
        };

        private static readonly Dictionary<string, int> UltraSpec = new Dictionary<string, int>()
        {
            {"Glow",  1 },
            {"Trigger", 2 },
            {"Bunny", 1 },
            {"Radar", 20 },
            {"RCS", 2},
        };

        public static void Set(string key)
        {
            int sleepVal = Get(key);
            Thread.Sleep(sleepVal);
        }

        public static int Get(string key)
        {
            switch (Specs)
            {
                case ComputerSpec.Ultra:
                    return UltraSpec[key];
                case ComputerSpec.Low:
                    return LowSpec[key];
                default:
                    return NormalSpec[key];
            }
        }
    }
}
