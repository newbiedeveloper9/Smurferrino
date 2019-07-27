using System.IO;
using System.Reflection;

namespace Smurferrino.Serialize
{
    public static class FilePaths
    {
        public static string LocalDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static string JsonDirectoryPath = $@"{LocalDirectory}\cfg\";
    }
}
