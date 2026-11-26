using CrudClassLibraryEF.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudClassLibraryEF.Contexts;


namespace CrudClassLibraryEF.Repositories
{
    public class NaturalPersonRepository
    {
        public int InsertNaturalPerson(NaturalPerson naturalperson)
        {
            try
            {
                using (CrudClassLibraryEF.Contexts.AppContext context = new CrudClassLibraryEF.Contexts.AppContext())
                {
                    context.NaturalPersonDbSet.Add(naturalperson);
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

        public int UpdateNaturalPerson(NaturalPerson naturalperson)
        {
            try
            {
                using (CrudClassLibraryEF.Contexts.AppContext context = new CrudClassLibraryEF.Contexts.AppContext())
                {
                    NaturalPerson person = context.NaturalPersonDbSet.FirstOrDefault<NaturalPerson>(p => p.Cpf == naturalperson.Cpf);

                    if (person == null)
                    {
                        return 0;
                    }

                    // context.Entry<NaturalPerson>(naturalperson).State = EntityState.Modified;

                    person.Cpf = naturalperson.Cpf;
                    person.Name = naturalperson.Name;
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

        public int DeleteNaturalPerson(NaturalPerson naturalperson)
        {
            try
            {
                using (CrudClassLibraryEF.Contexts.AppContext context = new CrudClassLibraryEF.Contexts.AppContext())
                {
                    NaturalPerson person = context.NaturalPersonDbSet.FirstOrDefault<NaturalPerson>(p => p.Cpf == naturalperson.Cpf);
                    if (person == null)
                        return 0;
                    context.Entry<NaturalPerson>(person).State = EntityState.Deleted;
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

        public NaturalPerson GetNaturalPersonByCPF(string cpf)
        {
            try
            {
                using (CrudClassLibraryEF.Contexts.AppContext context = new CrudClassLibraryEF.Contexts.AppContext())
                {
                    NaturalPerson naturalperson = context.NaturalPersonDbSet.FirstOrDefault(x => x.Cpf == cpf);
                    return naturalperson;
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
