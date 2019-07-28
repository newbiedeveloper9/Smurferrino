using System;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Memory;

namespace Smurferrino.Business.Players
{
    public class LocalPlayer
    {
        public LocalPlayer()
        {
            Global.LocalPlayer = this;
        }

        /// <summary>
        /// Base offset of localplayer
        /// </summary>
        internal int BaseOffset =>
            ManageMemory.ReadMemory<int>(BaseMemory.BaseAddress + MemoryAddr.dwLocalPlayer);

        /// <summary>
        /// Returns player's state e.g. Idle, crouching, jumping
        /// </summary>
        protected internal int StateFlag =>
            ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_fFlags);

        /// <summary>
        /// Get enumerated value of <see cref="StateFlag"/>.
        /// </summary>
        public PlayerState State =>
            Enum.IsDefined(typeof(PlayerState), StateFlag) ? (PlayerState)StateFlag : PlayerState.Null;

        /// <summary>
        /// Returns player's team flag
        /// </summary>
        protected internal int TeamFlag =>
            ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_iTeamNum);
        /// <summary>
        /// Get enumerated value of <see cref="TeamFlag"/>
        /// </summary>
        public TeamEnum Team =>
            (TeamEnum)TeamFlag;

        /// <summary>
        /// Returns player's id which is actually on crosshair
        /// </summary>
        protected internal int CrosshairId =>
            ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_iCrosshairId);
        /// <summary>
        /// Get <see cref="Player"/>'s instance which is actually on crosshair
        /// </summary>
        public Player CrosshairPlayer =>
            new Player(CrosshairId - 1);

        public int Health =>
            ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_iHealth);

        public bool IsAlive =>
            Health > 0;

    }
}
