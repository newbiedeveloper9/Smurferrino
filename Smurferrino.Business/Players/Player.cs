using System;
using System.Collections.Generic;
using System.Text;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Memory;

namespace Smurferrino.Business.Players
{
    public class Player
    {
        private readonly int _index;

        public Player(int index)
        {
            _index = index;
        }
        
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
    }
}
