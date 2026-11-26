using EF02App.Facades;
using EF02App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF02App
{
    class Program
    {
        static void Main(string[] args)
        {
            Crud crud = Crud.Instance;
            int result = 0;
            result = crud.Insert("Anderson", "08219824421", 1543.76f, new DateTime(2000, 5, 1), "M");
            if(result == 1)
            {
                Console.WriteLine("Successfully inserted.");
            }

            NaturalPerson natural_person = crud.GetNaturalPersonByCPF("08219824421");
            if(natural_person != null)
            {
                Console.WriteLine(natural_person);
            }

            result = crud.Update("Anderson C.", "08219824421", 4154.81f, new DateTime(2001, 6, 3), "O");
            if (result == 1)
            {
                Console.WriteLine("Successfully updated.");
            }

            List<NaturalPerson> natural_people = crud.GetNaturalPersonByName("A");
            foreach(NaturalPerson _natural_person in natural_people)
            {
                Console.WriteLine(_natural_person);
            }

            result = crud.Delete("08219824421");
            if (result == 1)
            {
                Console.WriteLine("Successfully deleted.");
            }

            Console.ReadKey();
        }
    }
}
