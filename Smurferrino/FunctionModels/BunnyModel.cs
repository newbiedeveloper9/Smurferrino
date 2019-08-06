using System;
using System.Diagnostics.Eventing.Reader;
using System.Threading;
using Darc_Euphoria.Euphoric;
using Newtonsoft.Json;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Players;
using Smurferrino.Enums;
using Smurferrino.Helpers;
using Smurferrino.Interfaces;
using Smurferrino.Services;

namespace Smurferrino.FunctionModels
{
    public class BunnyModel : BaseFunctionModel
    {
        private readonly IBunny _bunny;

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

                var state = Global.LocalPlayer.State;

                if (Keyboard.IsPressed(0x20))
                {
                    _bunny.Handle();
                    if (Global.LocalPlayer.IsReloading)
                    {
                        Console.WriteLine("reload");
                    }

                    ThreadSleep.Set(FunctionName);
                    continue;
                }
                if (state != PlayerState.Jump && state != PlayerState.DuckJump)
                {
                    _bunny.ManualReset();
                }

                Thread.Sleep(5);
            }
        }

        public BunnyModel()
        {
            _bunny = new Jumper(BhopOption);
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

                CanBhopOption = value;
            }
        }

        private bool _canBhopOption;
        public bool CanBhopOption
        {
            get => _canBhopOption;
            set
            {
                if (_canBhopOption == value) return;
                _canBhopOption = value;
                NotifyOfPropertyChange(() => CanBhopOption);
            }
        }

        private BhopOption _bhopOption;
        [JsonProperty]
        public BhopOption BhopOption
        {
            get => _bhopOption;
            set
            {
                _bhopOption = value;
                NotifyOfPropertyChange(() => BhopOption);

                _bunny.BhopOption = value;
            }
        }
    }
}
