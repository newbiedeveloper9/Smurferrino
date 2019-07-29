using System;
using System.Collections.Generic;
using System.Text;
using Smurferrino.Business.Helpers;

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

            for (int i = 0; i < 64; i++)
            {
                var player = new Player(i);

                if(player.BaseOffset == 0)
                    continue;

                if (player.BaseOffset == Global.LocalPlayer.BaseOffset)
                    Global.LocalPlayer.Index = i;

                arr.Add(player);
            }

            return arr;
        }
    }
}
