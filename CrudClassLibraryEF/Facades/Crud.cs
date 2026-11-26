using CrudClassLibraryEF.Entities;
using CrudClassLibraryEF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudClassLibraryEF.Facades
{
    public class Crud
    {
        private static Crud instance = null;
        private Crud()
        {
        }

        public static Crud Instance
        {
            get { if (Crud.instance == null) { Crud.instance = new Crud(); } return instance; }
        }

        public int InsertNaturalPerson(string name, string cpf)
        {
            NaturalPersonRepository rep = new NaturalPersonRepository();
            NaturalPerson np = new NaturalPerson(0, name, cpf);
            return rep.InsertNaturalPerson(np);
        }

        public int UpdateNaturalPerson(string cpf, string name)
        {
            NaturalPersonRepository rep = new NaturalPersonRepository();
            NaturalPerson np = new NaturalPerson(0, name, cpf);
            return rep.UpdateNaturalPerson(np);
        }

        public int DeleteNaturalPerson(string cpf)
        {
            NaturalPersonRepository rep = new NaturalPersonRepository();
            NaturalPerson np = new NaturalPerson(0, "", cpf);
            return rep.DeleteNaturalPerson(np);
        }

        public NaturalPerson GetNaturalPersonByCPF(string cpf)
        {
            NaturalPersonRepository rep = new NaturalPersonRepository();
            return rep.GetNaturalPersonByCPF(cpf);
        }
    }
}
