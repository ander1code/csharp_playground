using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ConsoleApp
{
    public class DataStructuresAndCollections
    {
        private static void ShowNumber(int obj)
        {
            Console.WriteLine("- {0}", obj);
        }

        public struct ComplexStruct
        {
            private int x;

            public int X
            {
              get { return x; }
              set { x = value; }
            }
            private int y;

            public int Y
            {
              get { return y; }
              set { y = value; }
            }

            /*
            public Complex()
            {
                this.x = 0;
                this.y = 0;
            }
            */


            /*
             * Versões antigas: 
                 * O compilador do .NET Framework te obriga a parametrizar o 
                 * construtor. Você não tem margem para inventar moda: ou 
                 * coloca parâmetros exigindo que os campos sejam preenchidos, 
                 * ou o código nem compila.
            */
            public ComplexStruct(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public override string ToString()
            {
 	             return String.Format("[X = {0}, Y = {1}]", this.X, this.Y);
            }

            public static ComplexStruct operator +(ComplexStruct c1, ComplexStruct c2)
            {
                return new ComplexStruct(c1.x + c2.x, c1.y + c2.y);
            }
        };

        enum Week
        {
            SUNDAY = 1,
            MONDAY = 2,
            TUESDAY = 3,   
            WEDNESDAY = 4,
            THURSDAY = 5,  
            FRIDAY = 6,
            SATURDAY = 7
        }

        public static void Principal()
        {
            // Array
            #region
            {
                // Forma 01:
                // int[] numbers = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                // Forma 02:
                int index = 0;
                int[] numbers = new int[10];
                numbers[index++] = 5;
                numbers[index++] = 6;
                numbers[index++] = 4;
                numbers[index++] = 7;
                numbers[index++] = 3;
                numbers[index++] = 8;
                numbers[index++] = 2;
                numbers[index++] = 9;
                numbers[index++] = 1;
                numbers[index++] = 0;
                
                try
                {
                    numbers[index++] = 10;
                }
                catch (IndexOutOfRangeException error)
                {
                    Console.WriteLine("{0}: {1}", error.HResult, error.Message);
                }

                // Sort:
                Array.Sort(numbers);

                foreach(int num in numbers)
                {
                    Console.WriteLine(num);
                }

                // char
                char[] chars = new char[] { 'b', 'a', 'd', 'e', 'c' };
                Array.Sort(chars);
                foreach (char c in chars)
                {
                    Console.WriteLine(c);
                }
            }
            #endregion

            // List
            #region
            {
                List<int> list = new List<int>() {5,6,4,7,3,8,2,9,1,0};
                list.Sort();
                
                List<int> sublist1 = new List<int>() {13, 33};
                
                // AddRange():
                //     Adds the elements of the specified collection to the end of the System.Collections.Generic.List<T>.
                list.AddRange(sublist1);

                foreach (int n in list)
                {
                    Console.WriteLine(n);
                }

                // BinarySearch()?
                //     Searches the entire sorted System.Collections.Generic.List<T> for an element
                //     using the specified comparer and returns the zero-based index of the element.
                Console.WriteLine("list.BinarySearch(5): {0}", list.BinarySearch(5));

                // Contains
                Console.WriteLine("list.Contains(13): {0}", list.Contains(13));
                Console.WriteLine("list.Contains(1000): {0}", list.Contains(1000));

                // Insert
                list.Insert(0, 45);

                List<int> sublist2 = new List<int>() { 77, 12 };

                // InsertRange
                list.InsertRange(0, sublist2);

                foreach (int n in list)
                {
                    Console.WriteLine(n);
                }

                list.Remove(12);

                Console.WriteLine("\n");

                foreach (int n in list)
                {
                    Console.WriteLine(n);
                }

                list.RemoveRange(0, 2);
                list.RemoveRange(10, 2);

                foreach (int n in list)
                {
                    Console.WriteLine(n);
                }

                Console.WriteLine(list.TrueForAll(x => x >= 0)); // todo os numeros sao iguais ou mairo que zero?
                
                List<int> evens = list.Where(x => x % 2 == 0).ToList();
                
                foreach (int n in evens)
                {
                    Console.WriteLine(n);
                }

                IEnumerator<int> odds = list.Where(x => x % 2 != 0).GetEnumerator();
                while(odds.MoveNext())
                {
                    Console.WriteLine(odds.Current);
                }

                list.ForEach(ShowNumber);

                //indexOf()
                Console.WriteLine("- {0}", list.IndexOf(6));
            }
            #endregion 
            
            // Stack
            #region
            {

                Console.WriteLine("\n\n\nSTACK");
                // Stack:
                //     Represents a variable size last-in-first-out (LIFO) collection of instances
                //     of the same specified type.
                Stack<int> stack = new Stack<int>(capacity: 10);

                for (int i = 0; i < 10; i++)
                {
                    stack.Push((i * 5));
                }

                Console.WriteLine("Stack count: {0}", stack.Count);
                
                // Peek:
                //     Returns the object at the top of the System.Collections.Generic.Stack<T>
                //     without removing it.
                Console.WriteLine("\nstack.Peek(): {0}", stack.Peek());

                foreach(int i in stack)
                {
                    Console.WriteLine(i);
                }

                // Pop:
                //     Removes and returns the object at the top of the System.Collections.Generic.Stack<T>.
                 
                 Console.WriteLine("\nstack.Pop(): {0}", stack.Pop());

                 foreach (int i in stack)
                 {
                     Console.WriteLine(i);
                 }

                stack.Clear();

                Console.WriteLine("\nStack count: {0}", stack.Count);
            }
            #endregion

            // Dictionary
            #region
            {
                // Dictionary:
                //     Represents a collection of keys and values.
                Dictionary<int, string> person = new Dictionary<int, string>()
                {
                    {1, "Anderson"},
                    {2, "Lucy"},
                    {3, "Carmen"},
                    {4, "Paul"},
                    {5, "Taylor"},
                    {6, "Trace"},
                    {7, "Lya"},
                };

                Console.WriteLine(person.Count);

                person.Remove(4);

                foreach(KeyValuePair<int, string> p in person)
                {
                    Console.WriteLine("{0}: {1}", p.Key, p.Value);
                }

                person.Add(8, "Tiffany");

                Console.WriteLine(person.ContainsKey(8));
                Console.WriteLine(person.ContainsKey(13));

                // person.Add(null, "Tiffany"); // ERROR!

                foreach (KeyValuePair<int, string> p in person)
                {
                    Console.WriteLine("{0}: {1}", p.Key, p.Value);
                }

                string name = "";
                Console.WriteLine(person.TryGetValue(8, out name));
                Console.WriteLine(name);

                Console.WriteLine(person.Count);

                person.Clear();

                Console.WriteLine(person.Count);
            }
            #endregion

            // Struct
            #region
            {
                ComplexStruct c1 = new ComplexStruct(1, 2);
                ComplexStruct c2 = new ComplexStruct(2, 5);
                Console.WriteLine(c1 + c2);
            }
            #endregion

            // Enum
            #region
            {
                Console.WriteLine(Week.SATURDAY);
                Console.WriteLine((int)Week.SATURDAY);

                // Enum.GetName(typeof(Week), 7):
                //     Retrieves the name of the constant in the specified enumeration that has
                //     the specified value.
                Console.WriteLine(Enum.GetName(typeof(Week), 7));

                // listing all:
                foreach(string wkday in Enum.GetNames(typeof(Week)))
                {
                    Console.WriteLine(wkday);
                }
            }
            #endregion

            Console.ReadKey();
        }
    }
}
