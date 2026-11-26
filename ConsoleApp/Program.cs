using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    delegate void Function(string name);
    delegate void Arithmetic_Function(int x, int y);

    class Program
    {
        

        static void Greeting(String name)
        {
            Console.WriteLine("Hello, {0}!", name);
        }

        static void Sum(int x, int y)
        {
            Console.WriteLine("{0} + {1} = {2}", x, y, (x + y));
        }

        static void Sub(int x, int y)
        {
            Console.WriteLine("{0} - {1} = {2}", x, y, (x - y));
        }

        static void Mlt(int x, int y)
        {
            Console.WriteLine("{0} x {1} = {2}", x, y, (x * y));
        }

        static void Div(int x, int y)
        {
            Console.WriteLine("{0} / {1} = {2}", x, y, (x / y));
        }

        static void Mod(int x, int y)
        {
            Console.WriteLine("{0} % {1} = {2}", x, y, (x % y));
        }

        public static void Main(string[] args)
        {
            Arithmetic_Function sum = Sum;
            Arithmetic_Function sub = Sub;
            Arithmetic_Function mlt = Mlt;
            Arithmetic_Function div = Div;
            Arithmetic_Function mod = Mod;
            sum(10, 3);
            sub(10, 3);
            mlt(10, 3);
            div(10, 3);
            mod(10, 3);

            Function f = Greeting;
            f("Anderson");


            Console.ReadKey();
        }
    }
}
