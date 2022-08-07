using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fileDel
{
    public static class Shared
    {
        public static void WriteStrongLineToConsole (string value)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine (value);
            Console.ResetColor ();
        }
    }
}
