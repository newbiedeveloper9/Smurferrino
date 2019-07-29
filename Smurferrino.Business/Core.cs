using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Memory;
using Smurferrino.Business.Players;
using Smurferrino.Helpers;

namespace Smurferrino.Business
{
    public class Core
    {
        public Attach Attach { get; set; }
        public BaseMemory Memory { get; set; }
        public LocalPlayer LocalPlayer { get; set; }
        public PlayerHelper Players { get; set; }

        public static Core Initialize()
        {
            var mainThread = new Core
            {
                Attach = new Attach(),
                Memory =  new BaseMemory(),
                LocalPlayer = new LocalPlayer(),
                Players =  new PlayerHelper(),
            };

            return mainThread;
        }
    }
}
