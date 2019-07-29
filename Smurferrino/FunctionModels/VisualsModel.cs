using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Smurferrino.Business.Helpers;

namespace Smurferrino.FunctionModels
{
    public class VisualsModel : BaseFunctionModel
    {
        public override string FunctionName { get; set; } = "Visuals";
        public override void DoWork()
        {
            while (true)
            {
                if (!FOVEnabled)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                if (Keyboard.IsPressed(FOVKey) || FOVKey == 0)
                {
                    Global.LocalPlayer.FOV = FOV;
                    continue;
                }

                Global.LocalPlayer.FOV = 74;
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
                if (_flashbangAlphaPercentage == value) return;
                _flashbangAlphaPercentage = value;
                NotifyOfPropertyChange(() => FlashbangAlphaPercentage);

                Global.LocalPlayer.FlashMaxAlpha = 255f * (value / 100);
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
    }
}
