using EF01App.Data;
using EF01App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF01App
{
    public class Crud
    {
        private static Crud instance = null;

        private Crud()
        {
        }

        public static Crud Instance { 
            get { 
                if(instance == null){
                    instance = new Crud();
                }
                return instance;
            }
        }

        public int Insert(string name, bool status, string CPF, float salary, DateTime birthday, string gender)
        {
            try
            {
                NaturalPerson natural_person = new NaturalPerson(){
                    Name = name,
                    Status = status,
                    CPF = CPF,
                    Salary = salary,
                    Birthday = birthday,
                    Gender = gender,
                };

                using(var context = new MyContext())
                {
                    context.NaturalPeople.Add(natural_person);
                    context.SaveChanges();
                }
                return 1;
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                return -1;
            }
        }

        public NaturalPerson GetByCPF(string cpf)
        {
            try
            {
                using (var context = new MyContext())
                {
                    var person = context.NaturalPeople.Where(p => p.CPF == cpf).SingleOrDefault();
                    return person;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                return null;
            }
        }

        public List<Person> GetByName(string name)
        {
            try
            {
                using (var context = new MyContext())
                {
                    var natural_people = context.NaturalPeople.Where(p => p.Name.StartsWith(name)).ToList();
                    var legal_people = context.LegalPeople.Where(p => p.Name.StartsWith(name)).ToList();
                    return natural_people.Union<Person>(legal_people).ToList();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                return null;
            }
        }

        public int Update(string name, bool status, string CPF, float salary, DateTime birthday, string gender)
        {
            try
            {
                using (var context = new MyContext())
                {
                    NaturalPerson natural_person = context.NaturalPeople.FirstOrDefault(p => p.CPF == CPF);

                    // NaturalPerson natural_person = this.GetByCPF(CPF);

                    if(natural_person == null)
                    {
                        return 0;
                    }

                    natural_person.Name = name;
                    natural_person.Status = status;
                    natural_person.CPF = CPF;
                    natural_person.Salary = salary;
                    natural_person.Birthday = birthday;
                    natural_person.Gender = gender;
                    
                    context.SaveChanges();
                }
                return 1;
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                return -1;
            }
        }

        public int Delete(string CPF)
        {
            try
            {
                using (var context = new MyContext())
                {
                    NaturalPerson natural_person = context.NaturalPeople.FirstOrDefault(p => p.CPF == CPF);

                    // NaturalPerson natural_person = this.GetByCPF(CPF);

                    if (natural_person == null)
                    {
                        return 0;
                    }
                    context.NaturalPeople.Remove(natural_person);
                    context.SaveChanges();
                }
                return 1;
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                return -1;
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Crud crud = Crud.Instance;

            int result = crud.Insert("Anderson", true, "63125362532", 1542.36f, new DateTime(2000, 5, 1), "M");
            if(result == 1)
            {
                Console.WriteLine("Successfully inserted.");
            }
            
            NaturalPerson natural_person = crud.GetByCPF("63125362532");
            if (natural_person != null)
                Console.WriteLine(natural_person);
            else
                Console.WriteLine("Not found!");
            
            result = crud.Update("Anderson C.", false, "63125362532", 3000.11f, new DateTime(2001, 5, 1), "O");
            if (result == 1)
                Console.WriteLine("Successfully updated.");

            List<Person> people = crud.GetByName("A");
            foreach(Person person in people)
            {
                Console.WriteLine(person);
            }

            result = crud.Delete("63125362532");
            if (result == 1)
                Console.WriteLine("Successfully deleted.");

            Console.WriteLine("Finished!");
            Console.ReadKey();
        }
    }
}
