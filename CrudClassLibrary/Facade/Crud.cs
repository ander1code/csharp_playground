using CrudClassLibrary.Controllers;
using CrudClassLibrary.DTOs;
using CrudClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudClassLibrary.Facade
{
    public class Crud
    {
        /*
        Facade:
            Facade é um padrão de projeto que fornece uma interface simples para acessar uma 
            funcionalidade que internamente envolve várias classes ou operações.
        */
        private static Crud instance = null;

        private Crud()
        {
        }

        public static Crud Instance
        {
            get { if (instance == null) { instance = new Crud(); } return Crud.instance; }
        }

        #region Natural Person
        public int InsertNaturalPerson(string name, string cpf)
        {
            NaturalPerson naturalperson = new NaturalPerson(0, name, cpf);
            Controller ctr = new Controller(naturalperson);
            return ctr.InsertNaturalPersonData();
        }

        public int UpdateNaturalPerson(string cpf, string name)
        {
            NaturalPerson naturalperson = new NaturalPerson(0, name, cpf);
            Controller ctr = new Controller(naturalperson);
            return ctr.UpdateNaturalPersonData();
        }

        public int DeleteNaturalPerson(string cpf)
        {
            NaturalPerson naturalperson = new NaturalPerson(0, "", cpf);
            Controller ctr = new Controller(naturalperson);
            return ctr.DeleteNaturalPersonData();
        }

        public NaturalPerson GetNaturalPersonByCPF(string cpf)
        {
            NaturalPerson naturalperson = new NaturalPerson(0, "", cpf);
            Controller ctr = new Controller(naturalperson);
            return ctr.GetNaturalPersonByCPF();
        }
        #endregion

        #region Legal Person
        public int InsertLegalPerson(string name, string cnpj)
        {
            throw new NotImplementedException();
        }

        public int UpdateLegalPerson(string cnpj, string name)
        {
            throw new NotImplementedException();
        }

        public int DeleteLegalPerson(string cnpj)
        {
            throw new NotImplementedException();
        }

        public LegalPerson GetLegalPersonByCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Person
        /*
            *  // IReadOnlyList:
               //   Represents a read-only collection of elements that can be accessed by index.
               //   "Eu vou fornecer uma coleção de resultados, mas quem chamou o método não deve modificar essa coleção."
            */
        public IReadOnlyList<PersonDTO> GetPersonByName(string name)
        {
            PersonDTO personDTO = new PersonDTO(0, name, "");
            Controller ctr = new Controller(personDTO);
            return ctr.GetPersonByName();
        }
        #endregion
    }
}
