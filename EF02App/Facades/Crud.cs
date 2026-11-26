using EF02App.Data;
using EF02App.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF02App.Facades
{
    public class Crud
    {
        private static Crud instance;

        private Crud()
        {
        }

        public static Crud Instance
        {
            get
            {
               if(instance == null)
               {
                   instance = new Crud();
               }
               return instance;
            }
        }

        /*
         * No EF6, context.Set<T>() retorna o DbSet da entidade T, permitindo adicionar e 
         * persistir os dados com SaveChanges(); em uma hierarquia TPT, o EF distribui automaticamente 
         * os dados nas tabelas da classe base e derivada, usando o mesmo Id como PK e FK.
        */
        public int Insert(string name, string cpf, float salary, DateTime birthday, string gender)
        {
            try
            {
                using (MyContext context = new MyContext())
                {
                    NaturalPerson natural_person = new NaturalPerson()
                    {
                        Name = name,
                        CPF = cpf,
                        Salary = salary,
                        Birthday = birthday,
                        Gender = gender
                    };

                    context.Set<NaturalPerson>().Add(natural_person);
                    context.SaveChanges();
                    return 1;
                }
            }
            catch (DbEntityValidationException error) // Pegando validações
            {
                foreach (var validationError in error.EntityValidationErrors)
                {
                    foreach (var validation in validationError.ValidationErrors)
                    {
                        Console.WriteLine(validation.ErrorMessage);
                    }
                }
                return -1;
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                return -1;
            }
            
        }

        public int Update(string name, string cpf, float salary, DateTime birthday, string gender)
        {
            try
            {
                using (MyContext context = new MyContext())
                {
                    NaturalPerson natural_person = context.Set<NaturalPerson>().FirstOrDefault(p => p.CPF == cpf);

                    if(natural_person == null)
                    {
                        return 0;
                    }
                    
                    natural_person.Name = name;
                    natural_person.Salary = salary;
                    natural_person.Birthday = birthday;
                    natural_person.Gender = gender;

                    // context.Set<NaturalPerson>().Add(natural_person);
                    context.SaveChanges();
                    return 1;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                return -1;
            }
        }

        public int Delete(string cpf)
        {
            try
            {
                using (MyContext context = new MyContext())
                {
                    NaturalPerson natural_person = context.Set<NaturalPerson>().FirstOrDefault(p => p.CPF == cpf);

                    if (natural_person == null)
                    {
                        return 0;
                    }

                    context.Set<NaturalPerson>().Remove(natural_person);
                    context.SaveChanges();
                    return 1;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                return -1;
            }
        }

        public List<NaturalPerson> GetNaturalPersonByName(string name)
        {
            try
            {
                using (MyContext context = new MyContext())
                {
                    return context.Set<NaturalPerson>().Where(p => p.Name.StartsWith(name)).ToList();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                return null;
            }
        }

        public NaturalPerson GetNaturalPersonByCPF(string cpf)
        {
            try
            {
                using (MyContext context = new MyContext())
                {
                    return context.Set<NaturalPerson>().FirstOrDefault(p => p.CPF == cpf);
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error);
                return null;
            }
        }
    }
}
