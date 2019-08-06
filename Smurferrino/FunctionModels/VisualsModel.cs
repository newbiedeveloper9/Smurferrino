using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Memory;
using Smurferrino.Business.Players;
using Smurferrino.Helpers;

namespace Smurferrino.FunctionModels
{
    public class VisualsModel : BaseFunctionModel
    {
        public override string FunctionName { get; set; } = "Visuals";
        private DateTime radarTimer;

        public override void DoWork()
        {
            while (true)
            {
                if (Global.ProcessState != ProcessState.Attached && Global.LocalPlayer == null)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                var lPlayer = Global.LocalPlayer;

                if (RadarSpottedEnabled && radarTimer <= DateTime.Now)
                {
                    foreach (var player in Global.Players.Where(x => !x.IsAlly))
                        if (player.IsAlive)
                            player.Spotted = true;


                    radarTimer = DateTime.Now.AddMilliseconds(ThreadSleep.Get("Radar"));
                    lPlayer.FlashMaxAlpha = 255f * (FlashbangAlphaPercentage / 100);
                }

                if (!FOVEnabled)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                if (lPlayer.Inventory.ActiveWeapon.TypeOfWeapon() == WeaponType.Sniper) continue;

                if ((Keyboard.IsPressed(FOVKey) || FOVKey == 0))
                {
                    if (lPlayer.FOV != FOV)
                    {
                        for (int i = 0; i < 1750; i++) //flood to bruteforce FOV (the only way probably)
                        {
                            lPlayer.FOV = FOV;
                        }
                        continue;
                    }
                }
                else
                {
                    if (lPlayer.FOV != 0)
                    {
                        for (int i = 0; i < 1750; i++) //flood to bruteforce FOV (the only way probably)
                        {
                            lPlayer.FOV = 0;
                        }
                        continue;
                    }
                }

                Thread.Sleep(10);
            }
        }

        private float _flashbangAlphaPercentage;
        [JsonProperty]
        public float FlashbangAlphaPercentage
        {
            get => _flashbangAlphaPercentage;
            set
            {
                _flashbangAlphaPercentage = value;
                NotifyOfPropertyChange(() => FlashbangAlphaPercentage);
            }
        }

        private int _fov;
        [JsonProperty]
        public int FOV
        {
            get => _fov;
            set
            {
                if (_fov == value) return;
                _fov = value;
                NotifyOfPropertyChange(() => FOV);
            }
        }

        private int _fovKey;
        [JsonProperty]
        public int FOVKey
        {
            get => _fovKey;
            set
            {
                if (_fovKey == value) return;
                _fovKey = value;
                NotifyOfPropertyChange(() => FOVKey);
            }
        }

        private bool _fovEnabled;
        [JsonProperty]
        public bool FOVEnabled
        {
            get => _fovEnabled;
            set
            {
                if (_fovEnabled == value) return;
                _fovEnabled = value;
                NotifyOfPropertyChange(() => FOVEnabled);
            }
        }

        private bool _radarSpottedEnabled;
        [JsonProperty]
        public bool RadarSpottedEnabled
        {
            get => _radarSpottedEnabled;
            set
            {
                _radarSpottedEnabled = value;
                NotifyOfPropertyChange(() => RadarSpottedEnabled);


            }
        }
    }
}
