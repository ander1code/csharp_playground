using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleApp
{
    public class UserInputAndFiles
    {
        [Serializable]
        public class Person
        {
            private int id;
            private string name;

            public Person(int id, string name)
            {
                this.id = id;
                this.name = name;
            }

            public override string ToString()
            {
                return String.Format("ID: {0}, Name: {1}", this.id, this.name);
            }
        }

        static void ReadLine_demo()
        {
            Console.Write("Enter a name: ");
            string name = Console.ReadLine();
            Console.WriteLine(String.Format("Hello, {0}!", name));
        }

        static void Read_demo()
        {
            Console.Write("Enter a char: ");
            int num = Console.Read();
            Console.WriteLine(String.Format("Char: {0}, number: {1}", Convert.ToChar(num), num));
        }

        static void ReadKey_demo()
        {
            // ReadKey(): Aprodundar!
            {
                ConsoleKeyInfo key;

                // TreatControlCAsInput:
                //     Gets or sets a value indicating whether the combination of the System.ConsoleModifiers.Control
                //     modifier key and System.ConsoleKey.C console key (Ctrl+C) is treated as ordinary
                //     input or as an interruption that is handled by the operating system.
                //
                // Returns:
                //     true if Ctrl+C is treated as ordinary input; otherwise, false.
                //
                // Exceptions:
                //   System.IO.IOException:
                //     Unable to get or set the input mode of the console input buffer.
                
                Console.TreatControlCAsInput = true;

                Console.WriteLine("Enter a key: ");
                key = Console.ReadKey();
                while (key.Key != ConsoleKey.Escape) 
                {
                    Console.WriteLine("Enter a key: ");
                    key = Console.ReadKey();
                }
                Console.WriteLine("Finished!");
            }
        }

        static void CustomConsole()
        {
            bool opc = false;
            if (opc)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.Green;
            }
        }

        static void InputInt_demo()
        {
            {
                string num;
                int sum = 0;
                do
                {
                    try
                    {
                        Console.Write("Enter a number: ");
                        num = Console.ReadLine();
                        sum += Int32.Parse(num);
                    }
                    catch (Exception)
                    {
                        break;
                    }
                } while (Int32.Parse(num) > 0);

                Console.Write(String.Format("Sum: {0}", sum));
            }
        }

        public static void Principal()
        {
            // Sample creating file:
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\text.txt";

                // File:
                //     Provides static methods for the creation, copying, deletion, moving, and
                //     opening of a single file, and aids in the creation of System.IO.FileStream
                //     objects.
                
                if(File.Exists(path))
                {
                    File.Delete(path);
                    Thread.Sleep(2000); // C# sleep (vem da Thread).
                    File.Create(path);
                }
                else
                {
                    File.Create(path);
                }
                Console.WriteLine("File successfully created.");
            }

            {
                FileStream fs = File.Open("text.txt", FileMode.OpenOrCreate);

                // UTF8Encoding:
                //     Represents a UTF-8 encoding of Unicode characters.//
                        // Parameters:
                        //   encoderShouldEmitUTF8Identifier:
                        //     true to specify that the System.Text.UTF8Encoding.GetPreamble() method should
                        //     return a Unicode byte order mark; otherwise, false.
                        //
                        //   throwOnInvalidBytes:
                        //     true to throw an exception when an invalid encoding is detected; otherwise,
                        //     false.

                Byte[] bytes = new UTF8Encoding(true).GetBytes("Hello, World!");
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }

            {
                FileStream fs = File.Open("text.txt", FileMode.Open);
                StreamReader str = new StreamReader(fs);
                string result = str.ReadToEnd();
                Console.WriteLine(result);
            }

            {
                try
                {
                    FileStream fs = File.Open("notfound.txt", FileMode.Open);
                }
                catch (FileNotFoundException error)
                {
                    Console.WriteLine(String.Format("StackTrace: {0}", error.StackTrace));
                    Console.WriteLine(String.Format("Message: {0}", error.Message));
                    Console.WriteLine(String.Format("TargetSite: {0}", error.TargetSite));
                    Console.WriteLine(String.Format("InnerException: {0}", error.InnerException));
                    Console.WriteLine(String.Format("Source: {0}", error.Source));
                    Console.WriteLine(String.Format("TargetSite: {0}", error.TargetSite));
                    Console.WriteLine(String.Format("HelpLink: {0}", error.HelpLink));
                    Console.WriteLine(String.Format("HResult: {0}", error.HResult));
                    Console.WriteLine(String.Format("FileName: {0}", error.FileName));
                    Console.WriteLine(String.Format("FusionLog: {0}", error.FusionLog));
                    Console.WriteLine(String.Format("Data: {0}", error.Data));
                }
            }

            // Tic-Tac-Toe
            {
                // sourcecode  
            }


            // File: Text to file
            {
                {
                    TextWriter txtw = new StreamWriter("hello.txt");
                    txtw.WriteLine("Hello, World!");
                    txtw.Close();
                }

                {
                    TextReader txtr = new StreamReader("hello.txt");
                    Console.WriteLine(txtr.ReadLine());
                    txtr.Close();
                }
            }


            // File: Text to Binary
            {
                {
                    BinaryWriter bw = new BinaryWriter(File.Open("hello.bin", FileMode.Create));
                    bw.Write("Hello, World!");
                    bw.Close();
                }

                {
                    BinaryReader br = new BinaryReader(File.OpenRead("hello.bin"));
                    Console.WriteLine(br.ReadString());
                    br.Close();
                }
            }


            // File: Object to Binary
            {
                {
                    Person person = new Person(123, "Anderson");
                    FileStream fs = File.Open("person.bin", FileMode.Create);
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, person);
                    fs.Close();
                }

                {
                    FileStream fs = File.Open("person.bin", FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    Console.WriteLine((Person)bf.Deserialize(fs));
                    fs.Close();
                }
            }

            // using...
            {
                try
                {
                    TextWriter txtw = new StreamWriter("hello.txt");
                    txtw.WriteLine("Hello, World!");
                    TextReader txtr = new StreamReader("hello.txt");
                    Console.WriteLine(txtr.ReadLine());
                }
                catch (Exception error)
                {
                    Console.WriteLine(String.Format("Error: {0}", error));
                }

                using (TextWriter tw = new StreamWriter(File.Open("using.txt", FileMode.Create)))
                {
                    tw.WriteLine("Hello, World!");
                }

                using (TextReader tr = new StreamReader(File.Open("using.txt", FileMode.Open)))
                {
                    Console.WriteLine(tr.ReadLine());
                }
            }

            // review outra forma de bytes
            {
                {
                    // Objeto original
                    Person person = new Person(123, "Anderson");

                    // Stream em memória
                    MemoryStream ms = new MemoryStream();

                    // Serializador
                    BinaryFormatter bf = new BinaryFormatter();

                    // Person -> MemoryStream
                    bf.Serialize(ms, person);

                    // Obtém os bytes gerados
                    byte[] bytes = ms.ToArray();

                    // Imprime os bytes em decimal
                    foreach (byte b in bytes)
                    {
                        Console.Write(b + " ");
                    }

                    Console.WriteLine();
                    Console.WriteLine();

                    // Volta o cursor do stream para o início
                    ms.Position = 0;

                    // MemoryStream -> Person
                    Person personResult = (Person)bf.Deserialize(ms);

                    Console.WriteLine(personResult);
                    
                    ms.Close();
                }
            }

            Console.ReadKey();
        }
    }
}
