using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_05
{
    public class UtilityClass
    {
        public static void PrintHeader(string name)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - name.Length, 0);
            Console.WriteLine(name);
        }
       
    }
}
