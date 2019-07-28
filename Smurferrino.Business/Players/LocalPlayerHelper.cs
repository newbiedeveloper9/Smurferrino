using System.Threading;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Memory;

namespace Smurferrino.Business.Players
{
    public static class LocalPlayerHelper
    {
        public static void Attack(this LocalPlayer localPlayer, int sprayDuration)
        {
            if (sprayDuration < 20)
                sprayDuration = 20;

            ManageMemory.WriteMemory<int>(BaseMemory.BaseAddress + MemoryAddr.dwForceAttack, 5);
            Thread.Sleep(sprayDuration);
            ManageMemory.WriteMemory<int>(BaseMemory.BaseAddress + MemoryAddr.dwForceAttack, 4);
        }

        public static void Attack2(this LocalPlayer localPlayer)
        {
            ManageMemory.WriteMemory<int>(BaseMemory.BaseAddress + MemoryAddr.dwForceAttack2, 5);
            Thread.Sleep(20);
            ManageMemory.WriteMemory<int>(BaseMemory.BaseAddress + MemoryAddr.dwForceAttack2, 4);
        }

        public static void Jump(this LocalPlayer localPlayer)
        {
            ManageMemory.WriteMemory<int>(BaseMemory.BaseAddress + MemoryAddr.dwForceJump, 5);
            Thread.Sleep(20);
            ManageMemory.WriteMemory<int>(BaseMemory.BaseAddress + MemoryAddr.dwForceJump, 4);
        }
    }
}
