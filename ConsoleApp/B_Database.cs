using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using CrudClassLibrary.Facade;
using CrudClassLibrary.DTOs;

namespace ConsoleApp
{
    public class Database
    {
        public static void Principal()
        {
            Console.WriteLine(Crud.Instance.InsertNaturalPerson("Anderson", "61253625362"));
            Console.WriteLine(Crud.Instance.GetNaturalPersonByCPF("61253625362"));
            Console.WriteLine(Crud.Instance.UpdateNaturalPerson("61253625362", "Anderson C."));
            Console.WriteLine(Crud.Instance.GetNaturalPersonByCPF("61253625362"));
            Console.WriteLine(Crud.Instance.GetNaturalPersonByCPF("61253625362"));
            IReadOnlyList<PersonDTO> people = Crud.Instance.GetPersonByName("");
            foreach(PersonDTO p in people)
            {
                Console.WriteLine(p);
            }
            Console.ReadKey();
            Console.WriteLine(Crud.Instance.DeleteNaturalPerson("61253625362"));
        }
    }
}
