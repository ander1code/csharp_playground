using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class GettingStartedwithCSharp
    {
        static string Greeting_Static(string name)
        {
            return String.Format("Hello, {0}!", name);
        }

        string Greeting(string name)
        {
            return String.Format("Hello, {0}!", name);
        }

        abstract class Person
        {
            private int id;

            public int Id
            {
                get { return id; }
                set { id = value; }
            }

            private string name;

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            // para rescreve um codigo... coloco public override, e esperar o intellisense
            public override string ToString()
            {
                return String.Format("ID: {0}, Name: {1}", this.Id, this.Name);
            }
        }

        class NaturalPerson : Person
        {
            private static int cont;

            public static int Cont
            {
                get { return NaturalPerson.cont; }
            }
            private string cpf;

            public string Cpf
            {
                get { return cpf; }
                set { cpf = value; }
            }

            public NaturalPerson(int id, string name, string cpf)
            {
                this.Id = id;
                this.Name = name;
                this.Cpf = cpf;
                NaturalPerson.cont += 1;
            }

            public override string ToString()
            {
                // chamando o metodo de classe pai: base.<method>()
                return String.Format("{0}, CPF: {1}", base.ToString(), this.Cpf);
            }
        }


        class Complex
        {
            public int x;
            public int y;

            public Complex(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public override string ToString()
            {
                return String.Format("[X = {0}, Y = {1}]", this.x, this.y);
            }

            // sobrecarregando o operador +:
            public static Complex operator +(Complex c1, Complex c2)
            {
                return new Complex(c1.x + c2.x, c1.y + c2.y);
            }

            public static Complex operator -(Complex c1, Complex c2)
            {
                return new Complex(c1.x + c2.x, c1.y + c2.y);
            }

        }

        static void ShowSizeOf(string name, int size)
        {
            Console.WriteLine(String.Format("Type: {0}, Size: {1} bytes", name, size));
        }


        static void Principal() // Main: Entry point: primeiro metodo a ser chamado na execução
        {
            {
                // Debug: Provides a set of methods and properties that help debug your code.
                System.Diagnostics.Debug.WriteLine("Hello, World!");
                System.Diagnostics.Debug.WriteLineIf(false, "Hello, World!"); // Não mostra nada!
            }

            {
                int id = 123;
                string name = "Anderson";
                char gender = 'M';
                float salary = 1541.11f;
                bool status = true;

                Console.WriteLine(String.Format("ID: {0}, Name: {1}, Gender: {2}, Salary: {3}, Status: {4}", id, name, gender, salary, status));
            }

            {
                int age = 15;
                if (age > 18)
                {
                    Console.WriteLine("Adult!");
                }
                else if ((age >= 14) && (age < 18))
                {
                    Console.WriteLine("Teen!");
                }
                else
                {
                    Console.WriteLine("Child!");
                }
            }

            {
                {
                    int i = 0;
                    for (i = 0; i < 10; i++)
                    {
                        Console.Write(String.Format("{0}, ", i));
                    }

                    Console.WriteLine("\n");

                    for (i = 0; ; )
                    {
                        if (i == 10)
                            break;
                        Console.Write(String.Format("{0}, ", i));
                        i++;
                    }

                    Console.WriteLine("\n");

                    i = 0;
                    while (true)
                    {
                        if (i == 10)
                            break;
                        Console.Write(String.Format("{0}, ", i));
                        i++;
                    }

                    Console.WriteLine("\n");

                    i = 0;
                    while (i < 10)
                    {
                        if (i == 10)
                            break;
                        Console.Write(String.Format("{0}, ", i));
                        i++;
                    }

                    Console.WriteLine("\n");

                    i = 0;
                    do
                    {
                        if (i == 10)
                            break;
                        Console.Write(String.Format("{0}, ", i));
                        i++;
                    } while (i < 10);
                }
            }

            {
                // Commemts 01

                /*
                 * Comments 02
                 */

                ///<summary>
                /// Comments 03
                ///</summary>
            }

            {
                Console.WriteLine(Greeting_Static("Anderson")); // static
                GettingStartedwithCSharp obj = new GettingStartedwithCSharp();
                Console.WriteLine(obj.Greeting("Anderson")); // instance
            }

            {
                try
                {
                    int num1 = 10;
                    int num2 = 0;
                    float div = num1 / num2; // Error: 10 / 0: typing error.
                }
                catch (Exception error)
                {
                    Console.WriteLine(String.Format("Error: {0}", error));
                }
            }

            {
                NaturalPerson np = new NaturalPerson(123, "Anderson", "65498712309");
                Console.WriteLine(np.ToString());
                Console.WriteLine(NaturalPerson.Cont);
                // Person p = new Person(); // Error!!!

                Console.WriteLine(np is NaturalPerson); // is: usado para verificar o tipo de um objeto ou variavel;
                int num = 10;
                Console.WriteLine(num is int);
                // is: Evitar o InvalidCastException error;

                // as: gerar um variable of type 
                object name = "Anderson"; // assume o tipo string automaticamente
                string name_str = name as string; // tenta converter para string; se não conseguir, retorna null.

                // GetType(): utilizado para saber o tipo de uma variavel ou objeto.
                Console.WriteLine(String.Format("{0}: {1}, {2}: {3}", name, name.GetType(), name_str, name_str.GetType()));
            }

            {
                // Ternar3y operator
                List<int> list = new List<int>()
                {
                    0,1,2,3,4,5,6,7,8,9
                };

                int num = -1;
                foreach (int n in list)
                {
                    num = n % 2 == 0 ? n : -1;
                    if (num != -1)
                    {
                        Console.WriteLine(n);
                    }
                }
            }

            {
                // Null-coalescing (??): “coalescência de nulo”. “Se esse valor for null, use outro valor como alternativa.”
                string name = null;
                string registered = name ?? "Name is empty!";
                Console.WriteLine(registered);

                NaturalPerson obj = null;
                NaturalPerson np = obj ?? new NaturalPerson(123, "Anderson", "65498712309");
                Console.WriteLine(np.ToString());
            }

            {
                Dictionary<string, int> dict = new Dictionary<string, int>()
                {
                    {"int", sizeof(int)},
                    //{"string",  sizeof(string)},
                    {"float", sizeof(float)},
                    {"bool", sizeof(bool)},
                    {"char", sizeof(char)},
                    {"uint", sizeof(uint)},
                    {"double", sizeof(double)},
                    //{"NaturalPerson", sizeof(NaturalPerson)},
                    {"byte", sizeof(byte)}
                };

                foreach (KeyValuePair<string, int> item in dict)
                {
                    ShowSizeOf(item.Key, item.Value);
                }
            }

            {
                /*
                System.Int32
                System.Single
                System.Boolean
                System.Char
                System.UInt32
                System.Double
                System.Byte
                */

                Console.WriteLine(typeof(int));
                Console.WriteLine(typeof(float));
                Console.WriteLine(typeof(bool));
                Console.WriteLine(typeof(char));
                Console.WriteLine(typeof(uint));
                Console.WriteLine(typeof(double));
                Console.WriteLine(typeof(byte));
            }

            {
                Complex c1 = new Complex(1, 3);
                Complex c2 = new Complex(2, 4);
                Console.WriteLine(c1);

                // utilizando a sobrecarga de operadores:
                Console.WriteLine(c1 + c2);
                Console.WriteLine(c1 - c2);
            }

            {
                // Keywords:
                /*
                 * abstract, as, base, bool, break, byte, case, catch, char, checked, class, const, 
                 * continue, decimal, default, delegate, do, double, else, enum, event, explicit, 
                 * extern, false, finally, fixed, float, for, foreach, goto, if, implicit, in, int, 
                 * interface, internal, is, lock, long, namespace, new, null, object, operator, out, 
                 * override, params, private, protected, public, readonly, ref, return, sbyte, sealed, 
                 * short, sizeof, stackalloc, static, string, struct, switch, this, throw, true, try, 
                 * typeof, uint, ulong, unchecked, unsafe, ushort, using, virtual, void, volatile, while
                */

                // Contextuais:
                /*
                add, alias, and, ascending, async, await, by, descending, dynamic, equals, file, 
                from, get, global, group, init, into, join, let, managed, nameof, nint, not, 
                notnull, nuint, on, or, orderby, partial, remove, required, scoped, select, 
                set, unmanaged, value, var, when, where, with, yield
                */
            }

            Console.ReadKey();
        }
    }
}
