using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smurferrino.Serialize;

namespace Smurferrino
{
    class ExecuteOnStart
    {
        public ExecuteOnStart()
        {
            Directory.CreateDirectory(FilePaths.JsonDirectoryPath);
        }
    }
}
