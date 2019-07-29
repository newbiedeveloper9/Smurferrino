using System;
using System.Collections.Generic;
using System.Text;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Memory;

namespace Smurferrino.Business.Objects
{
    public static class Rank
    {
        public static string[] CompetitiveRanks =
        {
            "Unranked",
            "Silver I",
            "Silver II",
            "Silver III",
            "Silver IV",
            "Silver Elite",
            "Silver Elite Master",
            "Gold Nova I",
            "Gold Nova II",
            "Gold Nova III",
            "Gold Nova Master",
            "Master Guardian I",
            "Master Guardian II",
            "Master Guardian Elite",
            "Distinguished Master Guardian",
            "Legendary Eagle",
            "Legendary Eagle Master",
            "Supreme Master First Class",
            "The Global Elite"
        };

/*        public static string GetRank(int playerIndex)
        {
            int playerResource = ManageMemory.ReadMemory<int>(BaseMemory.BaseAddress + MemoryAddr.dwPlayerResource);
            var rankId = ManageMemory.ReadMemory<int>(playerResource + MemoryAddr.m_iCompetitiveRanking + playerIndex * 4);

            return CompetitiveRanks[rankId];
        }

        public static int WinsCount(int playerIndex)
        {
            int playerResource = ManageMemory.ReadMemory<int>(BaseMemory.BaseAddress + MemoryAddr.dwPlayerResource);
            return ManageMemory.ReadMemory<int>(playerResource + MemoryAddr.m_iCompetitiveWins + playerIndex * 4);
        }*/
    }
}
