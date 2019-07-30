using System;
using System.Collections.Generic;
using System.Text;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Memory;
using Smurferrino.Business.Players;

namespace Smurferrino.Business.Weapons
{
    public class Weapon
    {
        private int Pointer;

        public Weapon()
        {
            
        }

        private Weapon(int pointer)
        {
            Pointer = pointer;
        }

        /// <summary>
        /// Returns <see cref="Player"/> weapon on <see cref="index"/> slot
        /// </summary>
        /// <param name="playerBase">Pointer(handler) of player(</param>
        /// <param name="index">Inventory index (1 knife, 2 pistol etc.)</param>
        /// <returns></returns>
        public Weapon MyWeapons(int playerBase, int index)
        {
            int pointer = ManageMemory.ReadMemory<int>(playerBase + MemoryAddr.m_hMyWeapons + (index - 1) * 0x4) & 0xFFF;
            return new Weapon(pointer);
        }

        /// <summary>
        /// Returns instance of active weapon
        /// </summary>
        /// <param name="playerBase">Pointer(handler) of player</param>
        /// <returns></returns>
        public Weapon ActiveWeapon(int playerBase)
        {
            int pointer = ManageMemory.ReadMemory<int>(playerBase + MemoryAddr.m_hActiveWeapon) & 0xFFF;
            return new Weapon(pointer);
        }

        protected internal int BaseOffset =>
            ManageMemory.ReadMemory<int>
                (BaseMemory.BaseAddress + MemoryAddr.dwEntityList + (Pointer - 1) * 0x10);

        /// <summary>
        /// Returns weapon Id
        /// </summary>
        public int Id =>
            ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_iItemDefinitionIndex);

        /// <summary>
        /// Returns weapon ammo
        /// </summary>
        public int Ammo => ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_iClip1);

        /// <summary>
        /// Returns weapon name
        /// </summary>
        public string Name =>
            WeaponHelper.WeaponNameDictionary[Id];

        
    }
}
