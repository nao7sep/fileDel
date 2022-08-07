using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace fileDel
{
    public static class iShared
    {
        public static readonly string TargetPathsListFilePath = Path.Join (Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location), "TargetPaths.txt");
    }
}
