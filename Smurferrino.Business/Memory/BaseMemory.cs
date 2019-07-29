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
        public static int EngineAddress;

        public static int ClientState => ManageMemory.ReadMemory<int>(EngineAddress + MemoryAddr.dwClientState);
        public static int GlowHandle => ManageMemory.ReadMemory<int>(BaseAddress + MemoryAddr.dwGlowObjectManager);
        public static int PlayerResource => ManageMemory.ReadMemory<int>(BaseAddress + MemoryAddr.dwPlayerResource);

        public BaseMemory()
        {
            BaseAddress = ManageMemory.GetModuleAddress("client_panorama");
            EngineAddress = ManageMemory.GetModuleAddress("engine.dll");
        }
    }
}
