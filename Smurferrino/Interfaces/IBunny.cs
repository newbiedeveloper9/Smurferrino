using Smurferrino.Enums;

namespace Smurferrino.Interfaces
{
    public interface IBunny
    {
        BhopOption BhopOption { get; set; }
        void Handle();
        void ManualReset();
        int FailChancePerc { get; }
    }
}
