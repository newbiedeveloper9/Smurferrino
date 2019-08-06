using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Caliburn.Micro;
using Darc_Euphoria.Euphoric;
using Newtonsoft.Json;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Memory;
using Smurferrino.Business.Players;
using Smurferrino.Business.Weapons;
using Smurferrino.Helpers;
using Smurferrino.Interfaces;
using Smurferrino.Models;
using Smurferrino.Services;

namespace Smurferrino.FunctionModels
{
    public class TriggerModel : BaseFunctionModel
    {
        private readonly Random _rnd;
        private readonly IRcs RCS;

        public override string FunctionName { get; set; } = "Trigger";

        public TriggerModel(IRcs rcs)
        {
            RCS = rcs;
            _rnd = new Random();
            WeaponCollection = new BindableCollection<WeaponModel>();

            foreach (var weapon in WeaponHelper.WeaponNames)
            {
                var model = new WeaponModel(weapon.Key, weapon.Value);
                WeaponCollection.Add(model);
            }
        }

        public override void DoWork()
        {
            while (Global.LocalPlayer == null)
            {
                Thread.Sleep(500);
            }

            Global.LocalPlayer.AfterShotFire += (sender, args) =>
            {
                Global.LocalPlayer.SendPackets = false;
                Global.LocalPlayer.ViewAngle = RCS.NewViewAngle().NormalizeAngle();
                Thread.Sleep(10);
                Global.LocalPlayer.SendPackets = true;
            };

            while (true)
            {
                if (!Enabled || Global.ProcessState != ProcessState.Attached || Global.LocalPlayer == null)
                {
                    Thread.Sleep(1000);
                    continue;
                }


                if (Key == 0 || Keyboard.IsPressed(Key))
                {
                    if (CanShoot())
                        TriggerPattern();
                }

                ThreadSleep.Set(FunctionName);
            }
        }

        #region Functions
        public void TriggerPattern()
        {
       //     Thread.Sleep(PreSprayDelay + _rnd.Next(MaxRandomSleep));

            if (!(Key == 0 || Keyboard.IsPressed(Key)))
                return;

            if (DoubleCheck)
            {
                if (CanShoot())
                    Shot();
                else
                {
                    while (!CanShoot())
                    {
                        if (!(Key == 0 || Keyboard.IsPressed(Key)))
                            return;
                    }
                    Shot();
                }
            }
            else
            {
                Shot();
            }

            if (!(Key == 0 || Keyboard.IsPressed(Key)))
                return;

//            Thread.Sleep(AfterSprayDelay + _rnd.Next(MaxRandomSleep));
        }

        private bool CanShoot()
        {
            var localPlayer = Global.LocalPlayer;
            var weapon = localPlayer.Inventory.ActiveWeapon.TypeOfWeapon();

            var target = Global.LocalPlayer.CrosshairPlayer;
            return !target.IsAlly && localPlayer.IsAlive && target.IsAlive && !target.Dormant &&
                   weapon != WeaponType.Bomb && weapon != WeaponType.Grenade && weapon != WeaponType.Knife;
        }

        private void Shot()
        {
            //Don't compensate recoil between the shots, only compensate it on each shot through your aimbot.

            Console.WriteLine("Shots fired:" + Global.LocalPlayer.ShotsFired);
            if (HasResources)
                Global.LocalPlayer.AttackStart();
            while (HasResources)
                Thread.Sleep(20);
            Global.LocalPlayer.AttackEnd();

            RCS.Reset();
        }
        #endregion Functions

        private bool HasResources => Global.LocalPlayer.ShotsFired < 3 &&
                                     Global.LocalPlayer.Inventory.ActiveWeapon.Ammo > 0 &&
                                     !Global.LocalPlayer.IsReloading;

        #region Properties
        private bool _enabled;
        [JsonProperty]
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (_enabled == value) return;
                _enabled = value;
                NotifyOfPropertyChange(() => Enabled);
            }
        }

        private int _key = 0;
        [JsonProperty]
        public int Key
        {
            get => _key;
            set
            {
                if (_key == value) return;
                _key = value;
                NotifyOfPropertyChange(() => Key);
            }
        }

        private bool _doubleCheck;
        [JsonProperty]
        public bool DoubleCheck
        {
            get => _doubleCheck;
            set
            {
                if (_doubleCheck == value) return;
                _doubleCheck = value;
                NotifyOfPropertyChange(() => DoubleCheck);
            }
        }

        private int _maxRandomSleep;
        [JsonProperty]
        public int MaxRandomSleep
        {
            get => _maxRandomSleep;
            set
            {
                if (_maxRandomSleep == value) return;
                _maxRandomSleep = value;
                NotifyOfPropertyChange(() => MaxRandomSleep);
            }
        }

        private BindableCollection<WeaponModel> _weaponCollection;
        public BindableCollection<WeaponModel> WeaponCollection
        {
            get => _weaponCollection;
            set
            {
                if (_weaponCollection == value) return;
                _weaponCollection = value;
                NotifyOfPropertyChange(() => WeaponCollection);
            }
        }
        #endregion Properties
    }
}
