using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class ObjectOrientedProgramming
    {
        /*
         * Partial class (classe parcial) é um recurso do C# que permite dividir a definição de 
         * uma mesma classe em dois ou mais arquivos físicos diferentes no seu projeto.
        */
        public abstract partial class Person
        {
            private int id;

            public int Id
            {
                get { return id; }
                set { id = value; }
            }
        }

        public abstract partial class Person
        {
            private string name;

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public override string ToString()
            {
                return String.Format("ID: {0}, Name: {1}", this.Id, this.Name);
            }

            public virtual void ShowClass()
            {
                Console.WriteLine("Person Class!");
            }
        }

        public class NaturalPerson : Person
        {
            private string cpf;

            public string Cpf
            {
                get { return cpf; }
                set { cpf = value; }
            }

            // Overload
            public NaturalPerson(int id, string name, string cpf)
            {
                this.Id = id;
                this.Name = name;
                this.cpf = cpf;
            }

            // polymorphism
            public override string ToString()
            {
                return String.Format("{0}, CPF: {1}", base.ToString(), this.Cpf);
            }

            // polymorphism
            public override void ShowClass()
            {
                Console.WriteLine("NaturalPerson Class!");
            }
        }


        // Indexers:
        /*
         * Indexer (ou indexador) é um recurso do C# que permite acessar os elementos de uma 
         * classe usando colchetes ([]), exatamente como se ela fosse um array, uma lista ou 
         * um dicionário.
        */

        public class Box
        {
            private int[] numbers = new int[10];

            public int this[int index] // Atenção: pode usar o auto-complete aqui!
            {
                set
                {
                    numbers[index] = value;
                }
                get
                {
                    return numbers[index];
                }
            }
        }

        public static void Principal()
        {
            {
                NaturalPerson np = new NaturalPerson(123, "Anderson", "76532167512");
                Console.WriteLine(np);
                np = null;
            }

            // Covariance: Covariância (ou covariance, para os íntimos) em C# é, basicamente, a capacidade de você usar um tipo mais específico (derivado/filho) em um lugar onde o código espera um tipo mais genérico (base/pai).
            {
                Person p = new NaturalPerson(123, "Anderson", "76532167512");
                Console.WriteLine(p);
                p = null;
            }

            // Indexers: 
            /*
             * Indexers (ou indexadores, para os íntimos) em C# são, basicamente, a forma de fazer um objeto seu 
             * se comportar exatamente como se fosse um array ou uma lista, permitindo que você acesse os dados 
             * dele usando colchetes [].
             */ 
            {
                Box box = new Box();
                for(int i = 0; i < 10; i++)
                {
                    box[i] = (i + 1) * 5;
                }

                for (int i = 0; i < 10; i++)
                {
                    Console.Write(box[i] + ", ");
                }
            }
            
            Console.ReadKey();
        }
    }
}
