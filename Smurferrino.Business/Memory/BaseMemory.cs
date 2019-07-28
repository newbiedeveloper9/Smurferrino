using System;
using System.Collections.Generic;
using System.Text;
using Smurferrino.Business.Helpers;
using Smurferrino.Helpers;

namespace Smurferrino.Business.Memory
{
    public class BaseMemory
    {
        public static int BaseAddress;

        public BaseMemory()
        {
            BaseAddress = ManageMemory.GetModuleAddress("client_panorama");
        }
    }
}
