using System;

namespace Smurferrino.Business.Enums
{
    public enum ProcessState
    {
        Null,
        Searching,
        Detached,
        Attached,
    }

    public class ProcessStateClass
    {
        public static event EventHandler ProcessStateChanged;
        internal static void OnProcessStateChanged(EventArgs e)
        {
            var handler = ProcessStateChanged;
            handler?.Invoke(typeof(ProcessStateClass), e);
        }
    }
}