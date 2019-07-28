using System;
using System.Collections.Generic;
using System.Text;
using Smurferrino.Helpers;

namespace Smurferrino.Business.Helpers
{
    public static class Keyboard
    {
        public static bool IsPressed(int key)
        {
            return WinApi.GetAsyncKeyState(key) < 0;
        }
    }
}
