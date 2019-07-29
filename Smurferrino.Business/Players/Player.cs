using System;
using System.Collections.Generic;
using System.Text;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Memory;
using Smurferrino.Business.Structs;

namespace Smurferrino.Business.Players
{
    public class Player
    {
        private readonly int _index;

        public Player(int index)
        {
            _index = index;
        }

        /// <summary>
        /// Returns player's handler
        /// </summary>
        internal int BaseOffset =>
            ManageMemory.ReadMemory<int>(BaseMemory.BaseAddress + MemoryAddr.dwEntityList + _index * 0x10);

        public int Health =>
            ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_iHealth);

        public bool IsAlive =>
            Health > 0;

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

        public bool IsAlly =>
            Team == Global.LocalPlayer.Team;


        /// <summary>
        /// Get player's glow index
        /// </summary>
        protected internal int GlowIndex =>
            ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_iGlowIndex);


        /// <summary>
        /// Drawing border on player. Wallhack
        /// </summary>
        /// <param name="rgba">Color</param>
        public void SetGlow(RGBA rgba)
        {
            var glowStruct = new GlowStruct(true, true, false);

            ManageMemory.WriteMemory<GlowStruct>(BaseMemory.GlowHandle + GlowIndex * 0x38 + 0x24, glowStruct);
            ManageMemory.WriteMemory<RGBA>(BaseMemory.GlowHandle + GlowIndex * 0x38 + 0x4, rgba);
        }
    }
}
