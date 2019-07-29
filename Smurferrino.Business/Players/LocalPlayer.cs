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

        public int Index { get; set; }
        /// <summary>
        /// Returns LocalPlayer's handler
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

        /// <summary>
        /// Return and sets client FOV (120-150 is better, sees much more)
        /// </summary>
        public int FOV
        {
            get => ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_iFOV);
            set => ManageMemory.WriteMemory<int>(BaseOffset + MemoryAddr.m_iFOV, value);
        }

        /// <summary>
        /// Returns and sets Flash alpha mask, 255 is max. 
        /// </summary>
        public float FlashMaxAlpha
        {
            get => ManageMemory.ReadMemory<float>(BaseOffset + MemoryAddr.m_flFlashMaxAlpha);
            set
            {
                if (value > 255)
                    value = 255;
                if (value < 1)
                    value = 1;
                ManageMemory.WriteMemory<float>(BaseOffset + MemoryAddr.m_flFlashMaxAlpha, value);
            }
        }

        /// <summary>
        /// Returns and sets 
        /// </summary>
        public float FlashAlphaDuration
        {
            get => ManageMemory.ReadMemory<float>(BaseOffset + MemoryAddr.m_flFlashDuration);
            set => ManageMemory.WriteMemory<float>(BaseOffset + MemoryAddr.m_flFlashDuration, value);
        }

        public int CompetetiveRank => 
            ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_iCompetitiveRanking);

    }
}
