using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Memory;
using Smurferrino.Business.Objects;
using Smurferrino.Business.Structs;
using Smurferrino.Business.Weapons;
using static Darc_Euphoria.Euphoric.Structures;

namespace Smurferrino.Business.Players
{
    public class Player
    {
        public Inventory Inventory { get; }
        public int Index { get; set; }

        public Player(int index)
        {
            Index = index;

            Inventory = new Inventory(BaseOffset);
        }

        /// <summary>
        /// Returns player handler
        /// </summary>
        internal int BaseOffset =>
            ManageMemory.ReadMemory<int>(BaseMemory.BaseAddress + MemoryAddr.dwEntityList + Index * 0x10);

        public int Health =>
            ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_iHealth);

        public bool IsAlive =>
            Health > 0;

        /// <summary>
        /// Returns player team flag
        /// </summary>
        protected internal int TeamFlag =>
            ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_iTeamNum);
        /// <summary>
        /// Get enumerated value of <see cref="TeamFlag"/>
        /// </summary>
        public Team Team =>
            (Team)TeamFlag;

        public virtual bool IsAlly =>
            Team == Global.LocalPlayer.Team;

        #region Glow
        /// <summary>
        /// Get player glow index
        /// </summary>
        protected internal int GlowIndex =>
            ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_iGlowIndex);

        /// <summary>
        /// Drawing border on player. Wallhack
        /// </summary>
        /// <param name="rgba">Color</param>
        public void SetGlow(RGBA rgba)
        {
            var glowStruct = new GlowStruct(true, true, false);

            //todo: create one struct and use just 1 wpm instead of 2. (add 16bytes junk code)
            ManageMemory.WriteMemory<GlowStruct>(BaseMemory.GlowHandle + GlowIndex * 0x38 + 0x24, glowStruct);
            ManageMemory.WriteMemory<RGBA>(BaseMemory.GlowHandle + GlowIndex * 0x38 + 0x4, rgba);
        }
        #endregion

        public int Observe => ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_hObserverTarget);
        public int Rank => ManageMemory.ReadMemory<int>(BaseMemory.PlayerResource + MemoryAddr.m_iCompetitiveRanking + Index * 0x4);
        public int Wins => ManageMemory.ReadMemory<int>(BaseMemory.PlayerResource + MemoryAddr.m_iCompetitiveWins + Index * 0x4);
        public bool Dormant => ManageMemory.ReadMemory<bool>(BaseOffset + MemoryAddr.m_bDormant);
        public Vector3 Position => ManageMemory.ReadMemory<Vector3>(BaseOffset + MemoryAddr.m_vecOrigin);
        public Vector3 VectorVelocity => ManageMemory.ReadMemory<Vector3>(BaseOffset + MemoryAddr.m_vecVelocity);
        public Vector3 EyeLevel
        {
            get
            {
                Vector3 vector = Position;
                vector.z += ManageMemory.ReadMemory<float>(BaseOffset + 0x10C);
                return vector;
            }
        }

        public bool Spotted
        {
            get => ManageMemory.ReadMemory<bool>(BaseOffset + MemoryAddr.m_bSpotted);
            set => ManageMemory.WriteMemory<bool>(BaseOffset + MemoryAddr.m_bSpotted, value);
        }

        public Vector3 BonePosition(int boneIndex)
        {
            int boneMatrix = ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_dwBoneMatrix);

            return new Vector3()
            {
                x = ManageMemory.ReadMemory<float>(boneMatrix + (0x30 * boneIndex) + 0x0C),
                y = ManageMemory.ReadMemory<float>(boneMatrix + (0x30 * boneIndex) + 0x1C),
                z = ManageMemory.ReadMemory<float>(boneMatrix + (0x30 * boneIndex) + 0x2C),
            };

        }
    }
}
