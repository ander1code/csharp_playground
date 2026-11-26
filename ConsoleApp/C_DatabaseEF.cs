using CrudClassLibraryEF.Facades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class DatabaseEF
    {
        public static void Principal()
        {
            // Rodar primeiro para criar o banco de dados
            /*
            try
            {
                using (var context = new AppContext())
                {
                    context.Database.CreateIfNotExists();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
            */
            
            Console.WriteLine(Crud.Instance.InsertNaturalPerson("Anderson", "61253625362"));
            Console.WriteLine(Crud.Instance.GetNaturalPersonByCPF("61253625362"));
            Console.WriteLine(Crud.Instance.UpdateNaturalPerson("61253625362", "Anderson C."));
            Console.WriteLine(Crud.Instance.GetNaturalPersonByCPF("61253625362"));
            
            /*
            IReadOnlyList<PersonDTO> people = Crud.Instance.GetPersonByName("");
            foreach (PersonDTO p in people)
            {
                Console.WriteLine(p);
            }
            Console.ReadKey();
            Console.WriteLine(Crud.Instance.DeleteNaturalPerson("61253625362"));
            */
            
            Console.WriteLine(Crud.Instance.DeleteNaturalPerson("61253625362"));
            Console.ReadKey();
        }
    }
}
