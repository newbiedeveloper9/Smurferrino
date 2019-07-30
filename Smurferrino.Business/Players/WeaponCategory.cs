using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Weapons;

namespace Smurferrino.Business.Players
{
    public static class WeaponCategory
    {
        private static readonly int[] PistolsId = { 1, 2, 3, 4, 30, 32, 36, 61, 63, 64 };
        private static readonly int[] KnivesId = { 41, 42, 59, 500, 505, 506, 507, 508, 509, 512, 514, 515, 516, 519, 520, 522, 523 };
        private static readonly int[] SnipersId = { 9, 11, 38, 40 };
        private static readonly int[] RiflesId = { 7, 8, 10, 13, 16, 39, 60 };
        private static readonly int[] SMGsId = { 17, 19, 24, 26, 33, 34 };
        private static readonly int[] ShotgunsId = { 25, 27, 29, 35 };
        private static readonly int[] LMGsId = { 14, 28 };
        private static readonly int[] GrenadesId = { 43, 44, 45, 46, 47, 48 };

        public static bool IsBomb(int id) =>
            id == 49;

        public static bool IsPistol(int id) =>
             PistolsId.Any(x => x == id);

        public static bool IsKnife(int id) =>
            KnivesId.Any(x => x == id);

        public static bool IsSniper(int id) =>
            SnipersId.Any(x => x == id);

        public static bool IsRifle(int id) =>
            RiflesId.Any(x => x == id);

        public static bool IsSMG(int id) =>
            SMGsId.Any(x => x == id);

        public static bool IsShotgun(int id) =>
            ShotgunsId.Any(x => x == id);

        public static bool IsLMG(int id) =>
            LMGsId.Any(x => x == id);

        public static bool IsGrenade(int id) =>
            GrenadesId.Any(x => x == id);

        public static WeaponType TypeOfWeapon(this Weapon weapon)
        {
            var type = WeaponType.Knife;

            if (IsBomb(weapon.Id))
                type = WeaponType.Bomb;
            else if (IsPistol(weapon.Id))
                type = WeaponType.Pistol;
            else if (IsKnife(weapon.Id))
                type = WeaponType.Knife;
            else if (IsSniper(weapon.Id))
                type = WeaponType.Sniper;
            else if (IsRifle(weapon.Id))
                type = WeaponType.Rifle;
            else if (IsSMG(weapon.Id))
                type = WeaponType.Smg;
            else if (IsShotgun(weapon.Id))
                type = WeaponType.Shotgun;
            else if (IsLMG(weapon.Id))
                type = WeaponType.Lmg;
            else if (IsGrenade(weapon.Id))
                type = WeaponType.Grenade;

            return type;
        }
    }
}
