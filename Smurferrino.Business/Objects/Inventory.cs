using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Players;
using Smurferrino.Business.Weapons;

namespace Smurferrino.Business.Objects
{
    public class Inventory
    {
        private int playerBase;

        public Weapon[] Weapons;

        public Inventory(int playerBase)
        {
            this.playerBase = playerBase;

            Weapons = new Weapon[64];
            for (int i = 0; i < Weapons.Length; i++)
            {
                Weapons[i] = new Weapon().MyWeapons(playerBase, i);
            }
        }

        public Weapon Knife => Weapons.FirstOrDefault(x => WeaponCategory.IsKnife(x.Id));
        public Weapon Primary => Weapons.FirstOrDefault(x => IsPrimary(x.TypeOfWeapon()));
        public Weapon Secondary => Weapons.FirstOrDefault(x => WeaponCategory.IsPistol(x.Id));
        public Weapon[] Grenades => Weapons.Where(x => WeaponCategory.IsGrenade(x.Id)).ToArray();
        public Weapon ActiveWeapon => new Weapon().ActiveWeapon(playerBase);

        private bool IsPrimary(WeaponType weapon)
        {
            return weapon == WeaponType.Lmg ||
                   weapon == WeaponType.Rifle ||
                   weapon == WeaponType.Shotgun ||
                   weapon == WeaponType.Smg ||
                   weapon == WeaponType.Sniper;
        }
    }
}
