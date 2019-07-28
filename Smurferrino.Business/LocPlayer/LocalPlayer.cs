using Smurferrino.Business.Helpers;

namespace Smurferrino.Business.LocPlayer
{
    public class LocalPlayer
    {
        public LocalPlayer()
        {
            Global.LocalPlayer = this;
        }
    }
}
