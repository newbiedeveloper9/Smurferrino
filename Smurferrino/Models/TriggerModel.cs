using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Newtonsoft.Json;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Players;
using Smurferrino.Helpers;

namespace Smurferrino.Models
{
    public class TriggerModel : BaseFunctionModel
    {
        private Random random;

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
                Thread.Sleep(5);
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
                    Global.LocalPlayer.Attack(SprayDuration + random.Next(MaxRandomSleep));
                else
                {
                    while (!CanShoot())
                    {
                        if (!(Key == 0 || Keyboard.IsPressed(Key)))
                            return;

                        Thread.Sleep(3);
                    }
                    Global.LocalPlayer.Attack(SprayDuration + random.Next(MaxRandomSleep));
                }
            }
            else
            {
                Global.LocalPlayer.Attack(SprayDuration + random.Next(MaxRandomSleep));
            }

            if (!(Key == 0 || Keyboard.IsPressed(Key)))
                return;

            Thread.Sleep(AfterSprayDelay + random.Next(MaxRandomSleep));
        }

        private bool CanShoot()
        {
            var localPlayer = Global.LocalPlayer;
            var target = Global.LocalPlayer.CrosshairPlayer;

            return target.Team != localPlayer.Team && localPlayer.IsAlive && target.IsAlive;
        }

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
    }
}
