using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
   public class DataTypes
   {
       public static int GCD(int m, int n)
       {
           int r = 0;
           int min = m < n ? m : n;
           for(int i = 1; i <= min; i++)
           {
               if ((m % i == 0) && (n % i == 0))
               {
                   r = i;
               }
           }
           return r;
       }

       public static int LCD(int m, int n)
       {
           int max = m > n ? m : n; // busco o max de m e n;
           int i = max; 

           while (true) // passo a usar while aqui!
           {
               if ((i % m == 0) && (i % n == 0)) // inverte a condição aqui!
               {
                   return i; // retorno o valor unico encontrado!
               }

               i++; // atribuo o valor oriundo de max;
           }
       }

       public static int Factorial(int n)
       {
           if((n == 0) || (n == 1))
               return 1;
           return Factorial(n - 1) * n;
       }

       public static int Fibonacci(int n)
       {
           int a = 0;
           int b = 1;
           int p = 0;

           while(n >= 1)
           {
               p = a + b;
               a = b;
               b = p;
               n--;
           }

           return a;
       }

       static void ChangeValueRef(ref int n)
       {
           n = 13;
       }

       static void ChangeValueOut(out int n)
       {
           n = 33;
       }

       public static void Principal(String[] args)
       {
           // built-in types:
           {
               int id = 123;
               String name = "Anderson";
               bool status = true;
               float salary = 1541.21f;
               char gender = 'M';

               List<Type> list = new List<Type>()
               {
                   typeof(int),
                   // typeof(string), Error: string nao pode ser "mashealed"!
                   typeof(bool),
                   typeof(float),
                   typeof(char)
               };

               foreach (Type t in list)
               {
                    // Marshal:
                    //     Provides a collection of methods for allocating unmanaged memory, copying
                    //     unmanaged memory blocks, and converting managed to unmanaged types, as well
                    //     as other miscellaneous methods used when interacting with unmanaged code.

                   Console.WriteLine(String.Format("[{0}]: typeof: {1}, sizeof: {2} bytes", t.Name, t, System.Runtime.InteropServices.Marshal.SizeOf(t)));
               }
           }
           
           // built-in types:
           {
               System.Int32 id = 123;
               System.String name = "Anderson";
               System.Boolean status = true;
               System.Single salary = 1541.21f; 
               System.Char gender = 'M';

               List<Type> list = new List<Type>()
               {
                   typeof(System.Int32),
                   // typeof(System.String) , Error: string nao pode ser "mashealed"!
                   typeof(System.Boolean),
                   typeof(System.Single),
                   typeof(System.Char)
               };

               foreach (Type t in list)
               {
                   Console.WriteLine(String.Format("[{0}]: typeof: {1}, sizeof: {2} bytes", t.Name, t, System.Runtime.InteropServices.Marshal.SizeOf(t)));
               }
           }

           // Math
           {
                /*
                Abs(), Max(), Min(), Round(), Floor(), Ceiling(), Pow(), Sqrt(), GCD(), LCM(), Factorial(), Fibonacci()
                */

               Console.WriteLine(System.Math.Abs(-10));
               Console.WriteLine(System.Math.Max(10,3));
               Console.WriteLine(System.Math.Min(10, 3));
               Console.WriteLine(System.Math.Round(7.5));
               Console.WriteLine(System.Math.Floor(7.5));

               // Summary:
               //     Returns the smallest integral value that is greater than or equal to the
               //     specified double-precision floating-point number.
               Console.WriteLine(System.Math.Ceiling(7.5));

               Console.WriteLine(System.Math.Pow(9,2));

               Console.WriteLine(System.Math.Sqrt(81));

               // GCD: System.Numerics.BigInteger.GreatestCommonDivisor(): somente .NET mais modernos.
               Console.WriteLine(GCD(18, 24));
               Console.WriteLine(LCD(12, 18));
               Console.WriteLine(Factorial(5));
               Console.WriteLine(Fibonacci(9));
           }

           {
               string str1 = "Hello, World!";
               string str2 = "Hello, World!";
               Console.WriteLine(String.Format("{0} and {1}", str1.Equals(str2), str2.Equals(str1)));
           }

           // @
           {
               string text = "a\nb";
               Console.WriteLine(text); // a
                                        // b
               text = @"a\nb";
               Console.WriteLine(text); // a\nb

               // string path = "C:\Path1\Path2\Path3\text.txt"; // Error: Error	1	Unrecognized escape sequence	
               string path = @"C:\Path1\Path2\Path3\text.txt"; 
               Console.WriteLine(path);

               string @if = "Hello, World!"; // utilizando keywords como nome de variaveis;
               Console.WriteLine(@if);

               // string interpolation ($).
               // Nao funciona no VS 2013, pois o Visual Studio 2013 usa C# 5.0.
               /*
               string name = "Anderson";
               Console.WriteLine($"Hello, {name}!");
               */
           }

           {
               /*
               Contains(), StartsWith(), EndsWith(), IndexOf(), LastIndexOf(), Substring(), 
               Replace(), Remove(), Split(), Trim(), ToLower(), ToUpper(), Equals()
               */

               Console.WriteLine(String.Empty);
               string greeting = "Hello, World!";
               Console.WriteLine(greeting);
               Console.WriteLine(greeting.Insert(4, "Something"));
               Console.WriteLine(greeting);
               Console.WriteLine(greeting.Length);
               Console.WriteLine(greeting.Contains("World"));
               Console.WriteLine(greeting.StartsWith("H"));
               Console.WriteLine(greeting.EndsWith("d"));
               Console.WriteLine(greeting.IndexOf("W"));
               Console.WriteLine(greeting.LastIndexOf("o"));
               Console.WriteLine(greeting.Substring(7, 5)); // [inicio, tamanho]
               Console.WriteLine(greeting.Replace("World", "Anderson"));
               Console.WriteLine(greeting.Remove(7, 6));
              

               greeting = "    Hello, World!    ";
               Console.WriteLine(String.Format("{0}: {1}", greeting, greeting.Trim()));
               greeting = "Hello, World!";
               Console.WriteLine(greeting.ToLower());
               Console.WriteLine(greeting.ToUpper());
               string str1 = "  Hello, World!       ";
               string str2 = "Hello, World!";
               Console.WriteLine(str1.Equals(str2));

               string names = "Anderson,Carmen,Lucy,Alicia,Tiffany";
               string[] people = names.Split(',');
               // String.Join(", ", people):
               //     Concatenates all the elements of a string array, using the specified separator
               //     between each element.
               Console.WriteLine("[{0}], type: {1}", String.Join(", ", people), people.GetType());
           
               List<string> list = new List<string>()
               {
                   null, 
                   "",
                   " ",
                   String.Empty,
                   "Anderson"
               };

               foreach (string n in list)
               {
                   if(String.IsNullOrEmpty(n))
                   {
                       Console.WriteLine(String.Format("{0} is IsNullOrEmpty!", n));
                       continue;
                   }

                   if(String.IsNullOrWhiteSpace(n))
                   {
                       Console.WriteLine(String.Format("{0} is IsNullOrWhiteSpace!", n));
                       continue;
                   }
                   Console.WriteLine(n);
               }
           }

           {
               // caracteres de escapes: \n \\ \b entre outros;
           }

           {
               const float PI = 3.14159f;

               Console.WriteLine("{0:C}", PI); // imprimindo como dinheiro;
               Console.WriteLine("{0:F}", PI); // como float;
               Console.WriteLine("{0:P}", PI); // 314.16% como percent;

               float p = 0.00654f;
               Console.WriteLine("{0:P}", p);  // 0.65%
           }

           {
               // StringBuilder:
               //     Represents a mutable string of characters. This class cannot be inherited.
               //     A diferença crucial é: string é imutável; StringBuilder é mutável.
               StringBuilder stb = new StringBuilder();
               stb.Append("Hello");
               stb.Append(", ");
               stb.Append("World");
               stb.Append("!");
               Console.WriteLine(stb.ToString());
               stb.Insert(4, "Something");
               Console.WriteLine(stb.ToString());
               stb.Replace("Something", "");
               Console.WriteLine(stb.ToString());
               stb.Remove(5, 7);
               Console.WriteLine(stb.ToString());
           }

           {
               bool condition1 = true;
               bool condition2 = false;
               bool? condition3 = null;
               
               Console.WriteLine(String.Format("{0}, {1}, {2}", condition1, condition2, condition3));

               Console.WriteLine(condition1 | condition2);
               Console.WriteLine(condition1 & condition2);
               Console.WriteLine(condition1 || condition2);
               Console.WriteLine(condition1 && condition2);
               Console.WriteLine(condition1 &= condition2);
               Console.WriteLine(condition1 |= condition2);
               Console.WriteLine(!condition1);
               Console.WriteLine(!condition2);
               // Console.WriteLine(condition1 || condition3); // Error!!!
               // Console.WriteLine(condition1 && condition3); // Error!!!
               Console.WriteLine(!condition3); // vazio!
           }

           {
               float num1 = 1541.43f;
               Console.WriteLine("{0}, type: {1}", num1, num1.GetType());
               int num2 = (int)num1;
               Console.WriteLine("{0}, type: {1}", num2, num2.GetType());
               
               int num3 = 13;
               Console.WriteLine("{0}, type: {1}", num3, num3.GetType());
               float num4 = (float)num3;
               Console.WriteLine("{0}, type: {1}", num4, num4.GetType()); 
           }

           {
               // var é tipagem estática. dynamic é tipagem dinâmica.
               var var1 = "Anderson";
               Console.WriteLine("{0}, type: {1}", var1, var1.GetType());
               // var1 = 123; // Error!!!

               dynamic var2 = "Anderson";
               Console.WriteLine("{0}, type: {1}", var2, var2.GetType());
               var2 = 123;
               Console.WriteLine("{0}, type: {1}", var2, var2.GetType());
           }


           // ref em C# serve para passar uma variável para um método por referência, permitindo que o 
           // método altere a própria variável original.
           {
               int x = 0;
               ChangeValueRef(ref x); // ref tanto na passagem, quanto no parametro lá na função.
               Console.WriteLine(x);

               int n = 0;
               ChangeValueOut(out n); // out tanto na passagem, quanto no parametro lá na função.
               Console.WriteLine(n);
           }

           Console.ReadKey();
       }
   }
}
