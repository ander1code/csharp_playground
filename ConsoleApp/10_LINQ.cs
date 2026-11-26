using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class LINQ
    {
        class Person
        {
            public int id;
            public string name;
            public int age;

            public Person(int id, string name, int age)
            {
                this.id = id;
                this.name = name;
                this.age = age;
            }

            public override string ToString()
            {
                return String.Format("ID: {0}, Name: {1}, Age: {2}", this.id, this.name, this.age);
            }
        }

        class City
        {
            public int id;
            public string city;

            public City(int id, string city)
            {
                this.id = id;
                this.city = city;
            }

            public override string ToString()
            {
                return String.Format("City: {0}", this.city);
            }
        }
        
        /*
            LINQ (Language Integrated Query) 
            é um recurso do C#/.NET que permite fazer consultas e transformações em coleções 
            de dados usando uma sintaxe parecida com SQL.Em vez de escrever vários for e if, você 
            pode expressar o que quer encontrar de forma mais direta.
            
            LINQ X lambda (Python):
         
                Sim — LINQ em C# tem uma relação bem direta com lambda em Python, mas são conceitos 
                diferentes.

                A ideia principal é:

                    Lambda é uma forma de criar uma função anônima. LINQ usa muito lambdas para dizer como os 
                    dados devem ser filtrados, transformados, ordenados etc.
         
         */
        public static void Principal()
        {

            int[] numbers = { 5, 6, 4, 7, 3, 8, 2, 9, 1, 0 };
            
            // Where(), First(), Max()
            {
                // First()
                Console.WriteLine(numbers.First());

                // Max()
                Console.WriteLine(numbers.Max());

                // Min()
                Console.WriteLine(numbers.Min());

                Array.Sort(numbers);

                // Where
                foreach (int n in numbers.Where(x => x % 2 == 0))
                {
                    Console.WriteLine(n);
                }
            }

            // Cast() and Select()
            {
                IEnumerable<float> floats = numbers.Cast<int>().Select(x => (float)x);
                foreach (float n in floats)
                {
                    Console.WriteLine("{0}, {1}", n, n.GetType());
                }
            }

            // Select()
            {
                IEnumerable<bool> booleans = numbers.Select(x => x % 2 == 0);
                foreach(bool b in booleans)
                {
                    Console.WriteLine("{0}", b);
                }

                /*
                IEnumerable<bool> teste = numbers.Select(x => x * 5 == 0);
                foreach (bool b in teste)
                {
                    Console.WriteLine("{0}", b);
                }
                */

                IEnumerable<int> multiply_5 = numbers.Select(x => x * 5);
                foreach (int i in multiply_5)
                {
                    Console.WriteLine("{0}", i);
                }

                Func<int, int> mult3 = (int x) => x * 3;

                IEnumerable<int> multiply_3 = numbers.Select<int, int>(mult3);

                foreach (int i in multiply_3)
                {
                    Console.WriteLine("{0}", i);
                }
            }

            // SelectMany()
            {
                Array.Sort(numbers);

                // SelectMany():
                //     Projects each element of a sequence to an System.Collections.Generic.IEnumerable<T>
                //     and flattens the resulting sequences into one sequence.
                var result = numbers.SelectMany(x => new[] {x, x * 5});
            }

            // ArrayList
            {
                // ArrayList:
                //     Implements the System.Collections.IList interface using an array whose size
                //     is dynamically increased as required.
                ArrayList array_list = new ArrayList();
                for(int i = 65; i < 91; i++)
                {
                    if(i % 2 == 0)
                        array_list.Add(i);
                    else
                        array_list.Add((char)i);
                }

                foreach(var i in array_list)
                {
                    Console.WriteLine(i);
                }

            }

            

            //OrderBy, ThenBy
            {
                List<Person> people = new List<Person>()
                {
                    new Person(1, "Anderson", 43),
                    new Person(2, "Carmen", 48),
                    new Person(3, "Alicia", 34),
                    new Person(4, "Anne", 30),
                    new Person(5, "Sara", 40),
                    new Person(6, "Tiffany", 43),
                    new Person(7, "Taylor", 43),
                    new Person(8, "Carol", 33),
                    new Person(9, "Jason", 43),
                };

                int[] vector = { 5, 6, 4, 7, 3, 8, 2, 9, 1, 0 };

                foreach (var i in vector.OrderBy(x => x))
                {
                    Console.WriteLine(i);
                }

                foreach (var i in vector.OrderByDescending(x => x))
                {
                    Console.WriteLine(i);
                }


                // ThenBy: funciona como um desempate.
                IOrderedEnumerable<Person> ordened = people.OrderBy(p => p.age).ThenBy(p => p.name);
                foreach(Person person in ordened)
                {
                    Console.WriteLine("{0}, {1}", person.name, person.age);
                }

            }

            // GroupBy
            {
                List<Person> people = new List<Person>()
                {
                    new Person(1, "Anderson", 43),
                    new Person(2, "Carmen", 48),
                    new Person(3, "Alicia", 34),
                    new Person(4, "Anne", 30),
                    new Person(5, "Sara", 40),
                    new Person(6, "Tiffany", 43),
                    new Person(7, "Taylor", 43),
                    new Person(8, "Carol", 33),
                    new Person(9, "Jason", 43),
                };

                // GroupBy:
                //     Groups the elements of a sequence according to a specified key selector function.
                IEnumerable<IGrouping<bool, Person>> group = people.GroupBy(p => ((p.age >= 40) && (p.age < 50)));

                foreach(var person_group in group)
                {
                    foreach (var person in person_group)
                    {
                        Console.WriteLine("{0}, {1}", person.name, person.age);
                    }
                }
            }


            //Interset, Union, Distinct, Except
            {
                List<int> list1 = new List<int>() { 1, 2, 2, 3, 5 };
                List<int> list2 = new List<int>() { 0, 0, 1, 2, 4 };

                Console.WriteLine("\n");

                foreach(int i in list1.Intersect(list2))
                {
                    Console.WriteLine("{0}", i);
                }

                Console.WriteLine("\n");

                foreach (int i in list1.Union(list2))
                {
                    Console.WriteLine("{0}", i);
                }

                Console.WriteLine("\n");

                foreach (int i in list1.Except(list2))
                {
                    Console.WriteLine("{0}", i);
                }

                Console.WriteLine("\n");

                foreach (int i in list1.Distinct())
                {
                    Console.WriteLine("{0}", i);
                }
                
            }

            // All, Any and Contains
            {
                List<int> list1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                Console.WriteLine(list1.All(x => x > 0));

                Console.WriteLine(list1.Any(x => x < 0));

                Console.WriteLine(list1.Contains(5));
            }

            // Skip, Take
            {
                List<int> list1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                IEnumerable<int> skip = list1.Skip(3);
                foreach (int i in skip)
                {
                    Console.WriteLine("{0}", i); // ignora os 3 primeiros
                }

                IEnumerable<int> take = list1.Take(3);
                foreach (int i in take)
                {
                    Console.WriteLine("{0}", i); // pega os 3 primeiros
                }
            }

            // join and groupjoin
            {
                /*
                  Join e GroupJoin: são métodos do LINQ usados quando você precisa relacionar duas 
                  coleções através de uma chave — pense como um JOIN de SQL.
                */

                // Join
                {
                    List<Person> people = new List<Person>()
                    {
                        new Person(1, "Anderson", 43),
                        new Person(2, "Carmen", 48),
                        new Person(3, "Alicia", 34),
                        new Person(4, "Anne", 30),
                        new Person(5, "Sara", 40),
                        new Person(6, "Tiffany", 43),
                        new Person(7, "Taylor", 43),
                        new Person(8, "Carol", 33),
                        new Person(9, "Jason", 43),
                    };

                        List<City> cities = new List<City>()
                    {
                        new City(1, "Rio de Janeiro"),
                        new City(2, "Los Angeles"),
                        new City(3, "Buenos Aires"),
                        new City(4, "London"),
                        new City(5, "Berlin"),
                        new City(6, "Toronto"),
                        new City(7, "Lisbon"),
                        new City(8, "Madrid"),
                        new City(9, "Tokyo"),
                    };

                    var result = people.Join(cities, person => person.id, city => city.id, (person, city) => new
                    {
                        Person = person.name,
                        City = city.city
                    });

                    foreach (var item in result)
                    {
                        Console.WriteLine("{0}: {1}", item.Person, item.City);
                    }
                }

                // GroupJoin
                {
                    

                    
                }
            }


            // SequenceEqual:
            //     Determines whether two sequences are equal by comparing the elements by using
            //     the default equality comparer for their type.
            {
                List<int> list1 = new List<int>() { 1, 2, 3};
                List<int> list2 = new List<int>() { 1, 2, 3};

                Console.WriteLine(list1.SequenceEqual(list2)); // True

                List<Person> p1 = new List<Person>()
                {
                    new Person(1, "Anderson", 43),
                    new Person(2, "Carmen", 48),
                    new Person(3, "Alicia", 34),
                };

                List<Person> p2 = new List<Person>()
                {
                    new Person(1, "Anderson", 43),
                    new Person(2, "Carmen", 48),
                    new Person(3, "Alicia", 34),
                };
                
                Console.WriteLine(p1.SequenceEqual(p2)); // False
            }

            // First, Last, Single, and ElementAt
            {
                List<int> list = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                Console.WriteLine(list.First());
                Console.WriteLine(list.Last());
                Console.WriteLine(list.Single((x) => x == (list.ElementAt(8) / 2)));
                Console.WriteLine(list.ElementAt(5));

                List<Person> people = new List<Person>()
                {
                    new Person(1, "Anderson", 43),
                    new Person(2, "Carmen", 48),
                    new Person(3, "Alicia", 34),
                    new Person(4, "Anne", 30),
                    new Person(5, "Sara", 40),
                    new Person(6, "Tiffany", 43),
                    new Person(7, "Taylor", 43),
                    new Person(8, "Carol", 33),
                    new Person(9, "Jason", 43),
                };

                Console.WriteLine(people.First());
                Console.WriteLine(people.Last());
                Console.WriteLine(people.Single(p => p.name == "Anderson"));
                Console.WriteLine(people.ElementAt(5));
            }

            // Concat
            {
                List<int> list1 = new List<int>() { 1, 2, 3 };
                List<int> list2 = new List<int>() { 4, 5, 6 };
                List<int> list3 = new List<int>() { 7, 8, 9 };

                IEnumerable<int> concat = list1.Concat(list2).Concat(list3);
                foreach(int n in concat)
                {
                    Console.WriteLine("{0}", n);
                }
            }

            // Aggregate
            {
                // Uso: acumular/reduzir uma sequência em um único resultado.

                List<int> list = new List<int>() { 1, 2, 3, 4, 5 };

                int summation = list.Aggregate((x, y) => x + y);
                Console.WriteLine("SUM = {0}", summation);
            }

            Console.ReadKey();
        }
    }
}
