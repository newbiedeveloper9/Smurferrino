using System;
using Darc_Euphoria.Euphoric;

namespace Smurferrino.Interfaces
{
    public interface IRcs
    {
        Structures.Vector2 NewViewAngle();
        void Reset();
        void HardReset();
    }
}