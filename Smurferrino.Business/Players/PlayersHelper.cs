using System;
using System.Collections.Generic;
using System.Text;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Memory;

namespace Smurferrino.Business.Players
{
    public class PlayerHelper
    {
        public PlayerHelper()
        {
            Global.Players = PlayersArray();
        }

        public List<Player> PlayersArray()
        {
            var arr = new List<Player>();
            var lPlayerBase =  ManageMemory.ReadMemory<int>(BaseMemory.BaseAddress + MemoryAddr.dwLocalPlayer);
           
            for (int i = 0; i < 64; i++)
            {
                var player = new Player(i);

                if (player.BaseOffset == 0)
                    continue;

                if (player.BaseOffset == lPlayerBase)
                {
                    if (Global.LocalPlayer == null)
                        Global.LocalPlayer = new LocalPlayer(i);
                    else
                        Global.LocalPlayer.Index = i;
                }

                arr.Add(player);
            }

            return arr;
        }
    }
}
