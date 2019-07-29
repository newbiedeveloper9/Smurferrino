using System.Threading;
using Newtonsoft.Json;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Structs;

namespace Smurferrino.FunctionModels
{
    public class GlowModel : BaseFunctionModel
    {
        public override string FunctionName { get; set; } = "Glow";
        public override void DoWork()
        {
            while (true)
            {
                if (!Enabled || Global.LocalPlayer.Team == TeamEnum.Spectator)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                foreach (var player in Global.Players)
                {
                    var rgba = new RGBA(1f, 0f, 0f, 1f);
                    if (player.IsAlly)
                    {
                        if (!ShowTeammates)
                            continue;

                        rgba = new RGBA(0f, 0f, 1f, 1f);
                    }

                    player.SetGlow(rgba);
                }
                Thread.Sleep(3);
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

        private bool _showTeammates;
        [JsonProperty]
        public bool ShowTeammates
        {
            get => _showTeammates;
            set
            {
                if (_showTeammates == value) return;
                _showTeammates = value;
                NotifyOfPropertyChange(() => ShowTeammates);
            }
        }
    }
}
