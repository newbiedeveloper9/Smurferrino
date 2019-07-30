using System;
using System.Threading;
using Newtonsoft.Json;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Players;
using Smurferrino.Helpers;

namespace Smurferrino.FunctionModels
{
    public class TriggerModel : BaseFunctionModel
    {
        private readonly Random random;

        public override string FunctionName { get; set; } = "Trigger";

        public TriggerModel()
        {
            random = new Random();
        }

        public override void DoWork()
        {
            while (true)
            {
                if (!Enabled || Global.ProcessState != ProcessState.Attached)
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

        public void TriggerPattern()
        {
            Thread.Sleep(PreSprayDelay + random.Next(MaxRandomSleep));

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

            Thread.Sleep(AfterSprayDelay + random.Next(MaxRandomSleep));
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
            Console.WriteLine("Shots fired:" + Global.LocalPlayer.ShotsFired);
            if (HasResources)
                Global.LocalPlayer.AttackStart();
            while (HasResources)
                Thread.Sleep(20);
            Global.LocalPlayer.AttackEnd();
        }

        private bool HasResources => Global.LocalPlayer.ShotsFired < SprayDuration &&
                                     Global.LocalPlayer.Inventory.ActiveWeapon.Ammo > 0;

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

        private int _preSprayDelay;
        [JsonProperty]
        public int PreSprayDelay
        {
            get => _preSprayDelay;
            set
            {
                if (_preSprayDelay == value) return;
                _preSprayDelay = value;
                NotifyOfPropertyChange(() => PreSprayDelay);
            }
        }

        private int _sprayDuration;
        [JsonProperty]
        public int SprayDuration
        {
            get => _sprayDuration;
            set
            {
                if (_sprayDuration == value) return;
                _sprayDuration = value;
                NotifyOfPropertyChange(() => SprayDuration);
            }
        }

        private int _afterSprayDelay;
        [JsonProperty]
        public int AfterSprayDelay
        {
            get => _afterSprayDelay;
            set
            {
                if (_afterSprayDelay == value) return;
                _afterSprayDelay = value;
                NotifyOfPropertyChange(() => AfterSprayDelay);

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
        #endregion Properties
    }
}
