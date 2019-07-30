using System;
using System.Collections.Generic;
using System.Text;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Memory;
using Smurferrino.Business.Players;

namespace Smurferrino.Business.Helpers
{
    public static class Global
    {
        private static ProcessState _processState = ProcessState.Null;
        public static ProcessState ProcessState
        {
            get => _processState;
            set
            {
                if (value == _processState) return;
                _processState = value;
                ProcessStateClass.OnProcessStateChanged(EventArgs.Empty);
            }
        }

        public static int GameState =>
            ManageMemory.ReadMemory<int>(MemoryAddr.dwClientState_State + BaseMemory.ClientState);

       public static LocalPlayer LocalPlayer { get; set; }
       public static List<Player> Players { get; set; }
    }
}
