using System.Threading;
using Newtonsoft.Json;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Players;

namespace Smurferrino.FunctionModels
{
    public class BunnyModel : BaseFunctionModel
    {
        public override string FunctionName { get; set; } = "Bunny";
        public override void DoWork()
        {
            while (true)
            {
                if (!Enabled || Global.ProcessState != ProcessState.Attached)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                if (Keyboard.IsPressed(0x20))
                {
                    if (Global.LocalPlayer.State == PlayerState.Idle)
                        Global.LocalPlayer.Jump();
                    Thread.Sleep(1);
                    continue;
                }

                Thread.Sleep(5);
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
    }
}
