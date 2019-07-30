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

namespace Smurferrino.FunctionModels
{
    public class RCSModel : BaseFunctionModel
    {
        public override string FunctionName { get; set; } = "RCS";

        private Structures.Vector2 _oldAimPunch;
        private Structures.Vector2 RCS()
        {
            var lPlayer = Global.LocalPlayer;
            var currentViewAngles = lPlayer.ViewAngle;
            var aimPunch = lPlayer.PunchAngle * 2f;

            var newViewAngles = new Structures.Vector2();
            newViewAngles.x = ((currentViewAngles + _oldAimPunch).x - aimPunch.x);
            newViewAngles.y = ((currentViewAngles + _oldAimPunch).y - aimPunch.y);

            _oldAimPunch = aimPunch;
            return newViewAngles;
        }

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

                    if (lPlayer.ShotsFired >= 1)
                    {
                        lPlayer.ViewAngle = RCS().NormalizeAngle();
                    }
                    else if (Math.Abs(_oldAimPunch.x) > 0 || Math.Abs(_oldAimPunch.y) > 0)
                    {
                        _oldAimPunch = new Structures.Vector2();
                    }

                }

                ThreadSleep.Set(FunctionName);
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
