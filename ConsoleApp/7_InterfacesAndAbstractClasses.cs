using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;

namespace ConsoleApp
{
    /*
        Interface x Classe abstrata: 
            Interface define um contrato. Classe abstrata define um contrato + pode 
            fornecer implementação e estado compartilhado.
    */
   
    // INotifyPropertyChanged:
    //     Notifies clients that a property value has changed.
    public class InterfacesAndAbstractClasses: INotifyPropertyChanged
    {
        interface IMyInterface
        {
            string Greeting(string name);
            int Sum(int x, int y);
        }

        interface IMyTaskInterface
        {
            // async Task MyTask(string name, int seconds); // ERROR!
            Task MyTask(string name, int seconds);
        }

        public class MyClass : IMyInterface, IMyTaskInterface
        {
            public string Greeting(string name)
            {
                return String.Format("Hello, {0}!", name);
            }

            public int Sum(int x, int y)
            {
                return x + y;
            }

            public async Task MyTask(string name, int seconds)
            {
                Console.WriteLine("Starting {0}...", name);
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("{0}: {1}", name, i);
                    await Task.Delay(500);
                }
                Console.WriteLine("{0} finished!", name);
            }
        }

        // PropertyChangedEventHandler:
        //     Represents the method that will handle the System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        //     event raised when a property is changed on a component.
        public event PropertyChangedEventHandler PropertyChanged;

        /*
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }
        */

        private void ChangeValue(ref int p)
        {
            p = 13;

            this.PropertyChanged += Program_PropertyChanged;
            
            if (PropertyChanged != null)
            {
                PropertyChanged(
                    this,
                    new PropertyChangedEventArgs("Value")
                );
            }
        }

        void Program_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("Changed!");
        }


/* --------------------------------------------------------------------------------------------------------- */

        class Complex : IComparable, IComparer, IEquatable<Complex>, IEqualityComparer<Complex>
        {
            public int x;
            public int y;

            public Complex()
            {
                this.x = 0;
                this.y = 0;
            }


            public Complex(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public static Complex operator + (Complex c1, Complex c2)
            {
                return new Complex(c1.x + c2.x, c1.y + c2.y);
            }
           
            public int CompareTo(object obj)
            {
                Complex c2 = (Complex)obj;
                if((this.x == c2.x) && (this.y == c2.y))
                {
                    return 0;
                }
                return -1;
            }

            public override string ToString()
            {
                return String.Format("[X = {0}, Y = {1}]", this.x, this.y);
            }

            public int Compare(object x, object y)
            {
                Complex c1 = (Complex)x;
                Complex c2 = (Complex)y;
                if ((c1.x == c2.x) && (c2.y == c2.y))
                {
                    return 0;
                }
                return -1;
            }

            public bool Equals(Complex other)
            {
                return ((this.x == other.x) && (this.y == other.y));
            }

            public bool Equals(Complex x, Complex y)
            {
                return ((x.x == y.x) && (x.y == y.y));
            }

            public int GetHashCode(Complex obj)
            {
                return obj.GetHashCode();
            }
        }

        // IEnumerable:
        //     Exposes an enumerator, which supports a simple iteration over a non-generic
        //     collection.

        // IEnumerator:
        //     Supports a simple iteration over a non-generic collection.
        class Box : IEnumerable // IEnumerator
        {
            public int[] numbers = new int[10];
            public int index = 0;

            public void Add(int num)
            {
                numbers[index] = num;
                index++;
            }

            // IEnumerable
            public IEnumerator GetEnumerator()
            {
                foreach (int num in numbers)
                {
                    if (num == 0)
                        break;
                    yield return num;
                }
            }

            // IEnumerator
            /*
            object IEnumerator.Current()
            {
                throw new NotImplementedException();
            }

            bool IEnumerator.MoveNext()
            {
                throw new NotImplementedException();
            }

            void IEnumerator.Reset()
            {
                throw new NotImplementedException();
            }
            */
        }

        /******************************************/

        public abstract class Parent
        {
            public Parent()
            {
                Console.WriteLine("Parent created!");
            }

            public void Method()
            {
                Console.WriteLine("Parent class!");
            }

            public virtual void MethodVirtual()
            {
                Console.WriteLine("Parent class (Virtual)!");
            }

            public abstract void ShowClass();

            // public virtual abstract void VirtualMethod(); // Error!
        }

        public class Child : Parent
        {
            public Child()
            {
                Console.WriteLine("Child created!");
            }

            public void Method()
            {
                Console.WriteLine("Child class!");
            }

            public void MethodVirtual()
            {
                Console.WriteLine("Child class (Virtual)!");
            }

            public override void ShowClass()
            {
                Console.WriteLine("Child Method!");
            }

            public void CallParentMethods()
            {
                /*
                    base: base serve para você acessar a implementação da 
                    classe pai a partir da classe filha.
                    
                    Campos (atributos) e propriedades também podem ser acessados com base, 
                    desde que sejam acessíveis (protected, public etc.).
                */
                base.Method();
                base.MethodVirtual();
            }
        }


        public static void Principal()
        {
            {
                MyClass obj = new MyClass();
                Console.WriteLine(obj.Greeting("Anderson"));
                //Task task = obj.MyTask("Custom_Task", 1);
                //Task.WaitAll(task);
            }

            {
                int x = 30;
                new InterfacesAndAbstractClasses().ChangeValue(ref x);
                Console.WriteLine(x);
            }

            {
                Complex c1 = new Complex(1, 3);
                Complex c2 = new Complex(2, 4);
                Console.WriteLine(c1.CompareTo(c2));
                Console.WriteLine(new Complex().Compare(new Complex(1, 2), new Complex(3, 6)));
                Console.WriteLine(c1.Equals(c2));
                Console.WriteLine("{0}, {1}", c1.GetHashCode(), c2.GetHashCode());
            }

            // IEnumerable
            {
                Box box = new Box();
                for (int i = 0; i < 10; i++)
                {
                    box.Add((i + 1) * 5);
                }

                foreach (int num in box)
                {
                    Console.WriteLine(num);
                }
            }

            // IEnumerator
            {
                // Estudar mais!
            }

            // ICollection 
            {
                ICollection<int> list = new List<int>();
                // Como criar uma classe com o ICollection?
            }

            {
                Child obj = new Child();
                obj.Method();
                obj.ShowClass();

                // Chamando Method() de Parent  (SEM SER VIRTUAL):

                {
                    // Forma 01:
                    Parent p = obj;
                    p.Method();
                    p.MethodVirtual();

                    // Forma 02:
                    ((Parent)obj).Method();
                    ((Parent)obj).MethodVirtual();

                    /* 
                     * Sobrescrita de metodos:
                       Quando você está falando de sobrescrita (override) em C#, a regra geral é:
                            - Na classe pai: virtual
                            - Na classe filha: override
                     */
                }

                


               


                
            }



            

            Console.ReadKey();
        }
    }
}


