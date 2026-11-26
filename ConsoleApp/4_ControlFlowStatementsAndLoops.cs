using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ControlFlowStatementsAndLoops
    {
        public static void Principal()
        {
            {
                int age = 15;
                if (age > 18)
                {
                    if(age >= 21)
                    {
                        Console.WriteLine("Driver!");
                    }
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
                int opc = 2;
                switch(opc)
                {
                    case 1:
                    {
                        Console.WriteLine("Option 01!");
                        break;
                    }
                    
                    case 2:
                    {
                        Console.WriteLine("Option 02!");
                        break;
                    }
                    
                    case 3:
                    {
                        Console.WriteLine("Option 03!");
                        break;
                    }
                    
                    default:
                    {
                        Console.WriteLine("Invalid option!");
                        break;
                    }
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

            Console.WriteLine("\n");

            {
                foreach(char c in "abcdefghij")
                {
                    Console.WriteLine(c);
                }
            }

            {
                int sum = 0;
                int num;
                try
                {
                    do
                    {
                        Console.Write("Number: ");
                        num = int.Parse(Console.ReadLine());

                        if (num > 0)
                        {
                            sum += num;
                        }

                    } while (num > 0);

                    Console.WriteLine(String.Format("Sum: {0}", sum));
                }
                catch (Exception)
                {
                    Console.WriteLine(String.Format("Sum: {0}", sum));
                }
            }
            
            Console.ReadKey();
        }
    }
}
