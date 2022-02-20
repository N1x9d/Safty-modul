using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SaftyModuleForFreelance sm = new SaftyModuleForFreelance();
            sm.DemoTime = 10;
            Console.WriteLine(sm.isPass);
            System.Reflection.Assembly.GetExecutingAssembly().GetName().Version = new Version(1, 5, 1, 1);
            var a = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);


            //Console.WriteLine($"{d.}");
            //Console.WriteLine(str);             //выводим в консоль расшифрованную строку
            Console.ReadKey();

        }
    }
}