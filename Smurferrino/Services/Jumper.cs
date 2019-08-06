using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Players;
using Smurferrino.Enums;
using Smurferrino.Interfaces;

namespace Smurferrino.Services
{
    class Jumper : IBunny
    {
        private Random _rnd;
        private bool _delay;
        private int _jumpCounter = 0;

        public BhopOption BhopOption { get; set; }

        public int FailChancePerc
        {
            get
            {
                if (BhopOption == BhopOption.Always) return 0;
                if (BhopOption == BhopOption.Legit && _jumpCounter == 0) return 0;

                return 15 + (int)Math.Pow(2 + ((float)BhopOption / 6), _jumpCounter);
            }
        }

        public Jumper(BhopOption bhopOption)
        {
            _rnd = new Random();
            BhopOption = bhopOption;
        }

       //public BhopOption BhopOption { get; set; }

        public void Handle()
        {
            var state = Global.LocalPlayer.State;
            if (_delay)
            {
                Thread.Sleep(32);
                _delay = false;
            }

            if (state != PlayerState.Idle &&
                state != PlayerState.DuckFinal &&
                state != PlayerState.DuckStart) return;

            if (CanJump())
            {
                Global.LocalPlayer.Jump();
                _jumpCounter++;
                _delay = true;
                return;
            }
            else if (!_delay)
            {
                Thread.Sleep(15);
                _jumpCounter = 0;
            }
            else
            {
                _jumpCounter = 0;
            }
        }

        public void ManualReset()
        {
            if (_jumpCounter == 0) return;

            _jumpCounter = 0;
            _delay = false;
        }

        private bool CanJump()
        {
            if (FailChancePerc == 0) return true;

            var val = _rnd.Next(0, 100);
            return val >= FailChancePerc;
        }
    }
}
