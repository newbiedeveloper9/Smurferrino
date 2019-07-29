using System;
using System.Collections.Generic;
using System.Text;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Memory;

namespace Smurferrino.Business.Weapons
{
    public class Weapon
    {
        public int Index { get; set; }
        public Weapon(int index)
        {
            Index = index;
        }

        /// <summary>
        /// Returns weapon's handler
        /// </summary>
        protected internal int BaseOffset
        {
            get
            {
                int myWeapons = ManageMemory.ReadMemory<int>
                                    (Global.LocalPlayer.BaseOffset + MemoryAddr.m_hMyWeapons + Index * 0x4) & 0xfff;

                return ManageMemory.ReadMemory<int>
                    (BaseMemory.BaseAddress + MemoryAddr.dwEntityList + (myWeapons - 1) * 0x10);
            }
        }

        /// <summary>
        /// Returns weapon's Id
        /// </summary>
        public int Id => 
            ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_iItemDefinitionIndex);


        /// <summary>
        /// Returns weapon's name
        /// </summary>
        public string Name =>
            WeaponHelper.WeaponNameDictionary[Id];
    }
}
