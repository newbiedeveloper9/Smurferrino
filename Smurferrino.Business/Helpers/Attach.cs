using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Smurferrino.Business.Enums;
using Smurferrino.Helpers;

namespace Smurferrino.Business.Helpers
{
    public class Attach
    {
        public Attach()
        {
            bool attached = false;
            while (!attached)
            {
                Thread.Sleep(333);
                Global.ProcessState = ProcessState.Searching;
                attached = ManageMemory.Attach("csgo");
            }

            Global.ProcessState = ProcessState.Attached;
        }
    }
}
