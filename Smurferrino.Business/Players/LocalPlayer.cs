﻿using System;
using System.Numerics;
using Darc_Euphoria.Euphoric;
using Smurferrino.Business.Enums;
using Smurferrino.Business.Helpers;
using Smurferrino.Business.Memory;
using Smurferrino.Business.Objects;
using Smurferrino.Business.Weapons;
using static Darc_Euphoria.Euphoric.Structures;

namespace Smurferrino.Business.Players
{
    public class LocalPlayer : Player
    {
        public LocalPlayer(int index) : base(index)
        {
            Global.LocalPlayer = this;
        }

        public GlobalVarBase GlobalVar =>
            ManageMemory.ReadMemory<GlobalVarBase>(BaseMemory.EngineAddress + MemoryAddr.dwGlobalVars);

        protected internal int StateFlag =>
            ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_fFlags);

        /// <summary>
        /// Get enumerated value of <see cref="StateFlag"/>.
        /// </summary>
        public PlayerState State =>
            Enum.IsDefined(typeof(PlayerState), StateFlag) ? (PlayerState)StateFlag : PlayerState.Null;

        /// <summary>
        /// Returns player id which is actually on crosshair
        /// </summary>
        protected internal int CrosshairId =>
            ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_iCrosshairId);

        /// <summary>
        /// Get <see cref="Player"/> instance which is actually on crosshair
        /// </summary>
        public Player CrosshairPlayer =>
            new Player(CrosshairId - 1);

        /// <summary>
        /// Return and sets client FOV (120-150 is better, sees much more)
        /// </summary>
        public int FOV
        {
            get => ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_iFOV);
            set => ManageMemory.WriteMemory<int>(BaseOffset + MemoryAddr.m_iFOV, value);
        }

        /// <summary>
        /// Returns and sets Flash alpha mask, 255 is max. 
        /// </summary>
        public float FlashMaxAlpha
        {
            get => ManageMemory.ReadMemory<float>(BaseOffset + MemoryAddr.m_flFlashMaxAlpha);
            set
            {
                if (value > 255)
                    value = 255;
                if (value < 1)
                    value = 1;
                ManageMemory.WriteMemory<float>(BaseOffset + MemoryAddr.m_flFlashMaxAlpha, value);
            }
        }

        /// <summary>
        /// Returns and sets flash duration
        /// </summary>
        public float FlashAlphaDuration
        {
            get => ManageMemory.ReadMemory<float>(BaseOffset + MemoryAddr.m_flFlashDuration);
            set => ManageMemory.WriteMemory<float>(BaseOffset + MemoryAddr.m_flFlashDuration, value);
        }

        public int ShotsFired =>
            ManageMemory.ReadMemory<int>(BaseOffset + MemoryAddr.m_iShotsFired);

        private Vector2 _viewAngle;
        public Vector2 ViewAngle
        {
            get
            {
                return _viewAngle = ManageMemory.ReadMemory<Vector2>(BaseMemory.ClientState + MemoryAddr.dwClientState_ViewAngles);
            }
            set
            {
                if (value.Equals(_viewAngle)) return;

                ManageMemory.WriteMemory<Vector2>(BaseMemory.ClientState + MemoryAddr.dwClientState_ViewAngles, value);
                _viewAngle = value;
            }
        }

        public Vector2 PunchAngle => ManageMemory.ReadMemory<Vector2>(BaseOffset + MemoryAddr.m_aimPunchAngle);
        public Vector2 PunchAngleVel => ManageMemory.ReadMemory<Vector2>(BaseOffset + MemoryAddr.m_aimPunchAngleVel);
        public Vector2 ViewPunchAngle => ManageMemory.ReadMemory<Vector2>(BaseOffset + MemoryAddr.m_viewPunchAngle);

        public bool Scoped
        {
            get => ManageMemory.ReadMemory<bool>(BaseOffset + MemoryAddr.m_bIsScoped);
            set
            {
                for (int i = 0; i < 1750; i++)
                    ManageMemory.WriteMemory<bool>(BaseOffset + MemoryAddr.m_bIsScoped, value);
            }
        }

        [Obsolete("Don't use this", true)] public override bool IsAlly => true;
    }
}
