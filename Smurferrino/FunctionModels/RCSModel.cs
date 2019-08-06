using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Darc_Euphoria.Euphoric;
using Newtonsoft.Json;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Players;
using Smurferrino.Helpers;
using Smurferrino.Interfaces;
using Smurferrino.Services;

namespace Smurferrino.FunctionModels
{
    public class RCSModel : BaseFunctionModel
    {
        public override string FunctionName { get; set; } = "RCS";
        private IRcs RCS;
        private bool _reset = false;

        public override void DoWork()
        {
            while (true)
            {

                if (Global.LocalPlayer == null || Global.ProcessState != ProcessState.Attached || !Enabled)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                if (Keyboard.IsPressed(1))
                {
                    var lPlayer = Global.LocalPlayer;
                    var wpn = lPlayer.Inventory.ActiveWeapon.TypeOfWeapon();
                    if (wpn != WeaponType.Pistol && lPlayer.Inventory.ActiveWeapon.Name != "AWP" &&
                        wpn != WeaponType.Knife && wpn != WeaponType.Shotgun)
                    {
                        if (lPlayer.ShotsFired >= 1)
                            lPlayer.ViewAngle = RCS.NewViewAngle().NormalizeAngle();
                        _reset = true;
                    }
                }
                else if (_reset)
                {
                    _reset = false;
                    RCS.HardReset();
                }
                 
                ThreadSleep.Set(FunctionName);
            }
        }

        public RCSModel(IRcs rcs)
        {
            RCS = rcs;
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
