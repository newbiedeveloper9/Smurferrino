using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Darc_Euphoria.Euphoric;
using Smurferrino.Business.Helpers;
using Smurferrino.Interfaces;

namespace Smurferrino.Services
{
    public class NoRecoil : IRcs
    {
        private Structures.Vector2 _oldAimPunch;

        public Structures.Vector2 NewViewAngle()
        {
            var lPlayer = Global.LocalPlayer;
            var currentViewAngles = lPlayer.ViewAngle;
            var aimPunch = lPlayer.PunchAngle * 2f;

            var newViewAngles = new Structures.Vector2
            {
                x = ((currentViewAngles + _oldAimPunch).x - aimPunch.x),
                y = ((currentViewAngles + _oldAimPunch).y - aimPunch.y)
            };

            _oldAimPunch = aimPunch;
            return newViewAngles;
        }

        public void Reset()
        {
            var lPlayer = Global.LocalPlayer;
            var punchAngle = lPlayer.PunchAngle;

            var oldAbsSum = Math.Abs(_oldAimPunch.x + _oldAimPunch.y);
            var newAbsSum = Math.Abs(punchAngle.x + punchAngle.y);
            if (oldAbsSum > 1.9f)
            {
                while (newAbsSum > 0.25)
                {
                    Thread.Sleep(5);

                    newAbsSum = Math.Abs(lPlayer.PunchAngle.x + lPlayer.PunchAngle.y);
                }

                var newViewAngle = NewViewAngle();
                var playerAngle = lPlayer.ViewAngle;
                var diffX = Math.Max(playerAngle.x, newViewAngle.x) - Math.Min(playerAngle.x, newViewAngle.x);
                var diffY = Math.Max(playerAngle.y, newViewAngle.y) - Math.Min(playerAngle.y, newViewAngle.y);
                var diff = new Structures.Vector2(diffX, diffY);

                for (int i = 0; i < 4; i++)
                {
                    lPlayer.SendPackets = false;
                    lPlayer.ViewAngle -= diff / 4;
                    Thread.Sleep(6);
                    lPlayer.SendPackets = true;
                }

            }
            else
            {
                lPlayer.SendPackets = false;
                _oldAimPunch = new Structures.Vector2();
                Thread.Sleep(10);
                lPlayer.SendPackets = true;
            }
        }

        public void HardReset()
        {
            _oldAimPunch = new Structures.Vector2();
        }
    }
}
