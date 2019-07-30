using System;
using System.Threading;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Memory;
using Smurferrino.Helpers;

namespace Smurferrino.Business.Players
{
    public static class LocalPlayerHelper
    {
        public static void AttackStart(this LocalPlayer localPlayer)
        {
            ManageMemory.WriteMemory<int>(BaseMemory.BaseAddress + MemoryAddr.dwForceAttack, 5);
        }

        public static void AttackEnd(this LocalPlayer localPlayer)
        {
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
