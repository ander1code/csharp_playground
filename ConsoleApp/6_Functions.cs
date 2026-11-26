#define CONDITIONAL1
// #define CONDITIONAL2

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{

    public static class Extension
    {
        // Extension Methods:
        public static bool GreaterThan(this int x, int y)
        {
            return (x > y);
        }
    }

    public class Functions
    {
        [MyAttribute]
        public class MyClass
        {
            public MyClass()
            {
                Console.WriteLine("Object created!");
            }

            ~MyClass()  // não pode ter encapsulamento;
            {
                Console.WriteLine("Object destroyed!");
            }

            // Obsolete:
            //     Marks the program elements that are no longer in use. This class cannot be
            //     inherited.
            [Obsolete("Use New Method!")]
            public void OldMethod()
            {
                Console.WriteLine("Old method!");
            }

            public void NewMethod()
            {
                Console.WriteLine("Old method!");
            }

            [Conditional("CONDITIONAL1")]
            public void MethodTrue()
            {
                Console.WriteLine("MethodTrue!");
            }

            [Conditional("CONDITIONAL2")]
            public void MethodFalse()
            {
                Console.WriteLine("MethodFalse!");
            }

            [MyAttribute]
            public void MethodAttribute()
            {
                Console.WriteLine("MethodAttribute!");
            }


            // Sobrecarga de metodos:
            public void Greeting()
            {
                Console.WriteLine("Hello, World!");
            }

            public void Greeting(string name)
            {
                Console.WriteLine("Hello, {0}!", name);
            }


            /*  ERRADO!!!
            string Greeting(string name)
            {
                return String.Format("Hello, {0}!", name);
            }
            */

            public void Method()
            {
                Console.WriteLine("Method!");
            }
        }


        class MyClassChild : MyClass
        {

            // overriding
            public void Method()
            {
                Console.WriteLine("Method Child!");
            }
        }

        // AttributeUsage:
        //     Specifies the usage of another attribute class. This class cannot be inherited.
        //     AttributeTargets:
        //          Specifies the application elements on which it is valid to apply an attribute.
        [AttributeUsage(AttributeTargets.All)]
        public class MyAttribute : Attribute
        {
            public override object TypeId
            {
                get
                {
                    return base.TypeId;
                }
            }
        }

        static void show_string(params string[] args)
        {
            foreach(string s in args)
            {
                Console.WriteLine(s);
            }
        }

        static void Greeting(string name="World")
        {
            Console.WriteLine("Hello, {0}!", name);
        }

        // MyTask:
        //     Represents an asynchronous operation.
        // static async void MyTask(string name, int seconds)
        static async Task MyTask(string name, int seconds)
        {
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine("{0}: {1}", name, i);
                // Thread.Sleep(seconds * 1000);
                await Task.Delay(seconds * 1000);
            }
            Console.WriteLine("{0} finished!", name);
        }

        static async Task Run()
        {
            await MyTask("Task_04", 5);
        }
        

        public static void Principal()
        {
            {
                MyClass obj = new MyClass();
                obj.OldMethod();
                obj.MethodTrue();
                obj.MethodFalse();
            }

            // Funções Anonimas:
            // Func:
            {
                // Func:
                //     Encapsulates a method that has two parameters and returns a value of the
                //     type specified by the TResult parameter.
                Func<int, int, int> sum = (int x, int y) =>
                {
                    return x + y;
                };


                // Para uma função que recebe uma string e não retorna nada, o correto é Action<string>:
                // Action:
                //     Encapsulates a method that has a single parameter and does not return a value.
                Action<string> Greeting = (string name) => Console.WriteLine("Hello, {0}!", name);

                Console.WriteLine(sum(5, 7));
            }

            // Overloading
            {
                MyClass obj = new MyClass();
                obj.Greeting();
                obj.Greeting("Anderson");
            }

            // Overriding
            {

                MyClassChild obj2 = new MyClassChild();
                obj2.Method();
            }

            // Infinite parameters
            {
                show_string("");
                show_string("text1");
                show_string("text1", "text2");
                show_string("text1", "text2", "text3");
                show_string("text1", "text2", "text3", "text4");
                show_string("text1", "text2", "text3", "text4", "text5");
            }

            // Optional parameters:
            {
                Greeting();
                Greeting("Anderson");
            }

            // Extension Method:
            /*   
                Um Extension Method (método de extensão) é uma forma de adicionar novos 
                métodos a um tipo existente sem modificar o código desse tipo e sem criar 
                uma classe derivada.
            */
            {
                int x = 10;
                bool result = x.GreaterThan(5);
                Console.WriteLine(result);
                Console.WriteLine(6.GreaterThan(7));
            }

            // Using Async Functions:
            {
                if(false)
                {
                    int opc = 3;

                    if (opc == 1)
                    {
                        // Sequencial
                        MyTask("Task_01", 3).Wait();
                        MyTask("Task_02", 1).Wait();
                        MyTask("Task_03", 2).Wait();
                    }

                    if (opc == 2)
                    {
                        // Concorrente:
                        Task task1 = MyTask("Task_01", 3);
                        Task task2 = MyTask("Task_02", 1);
                        Task task3 = MyTask("Task_03", 2);
                        Task.WaitAll(task1, task2, task3);
                    }

                    if (opc == 3)
                    {
                        Run().Wait();
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
