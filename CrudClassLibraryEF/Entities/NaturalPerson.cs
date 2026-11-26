using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudClassLibraryEF.Entities
{
    public sealed class NaturalPerson : Person
    {
        private string cpf;

        public string Cpf
        {
            get { return cpf; }
            set { cpf = value; }
        }

        public NaturalPerson()
        {
        }

        public NaturalPerson(int id, string name, string cpf)
        {
            this.Id = id;
            this.Name = name;
            this.Cpf = cpf;
        }

        public override string ToString()
        {
            return String.Format("{0}, CPF: {1}", base.ToString(), this.Cpf);
        }
    }
}
