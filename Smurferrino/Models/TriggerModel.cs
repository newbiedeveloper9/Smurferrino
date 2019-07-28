using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Newtonsoft.Json;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.LocPlayer;
using Smurferrino.Helpers;

namespace Smurferrino.Models
{
    public class TriggerModel : BaseFunctionModel
    {
        public override string FunctionName { get; set; } = "Trigger";

        public override void DoWork()
        {
            while (true)
            {
                if (!Enabled)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                if (Key == 0 || WinApi.GetAsyncKeyState(Key) < 0)
                {
                    Global.LocalPlayer.Attack();
                }
            }
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

        private int _key;
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
    }
}
