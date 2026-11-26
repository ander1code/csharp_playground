using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudClassLibraryEF.Entities
{
    public sealed class LegalPerson : Person
    {
        private string cnpj;

        public string Cnpj
        {
            get { return cnpj; }
            set { cnpj = value; }
        }

        public LegalPerson(int id, string name, string cnpj)
        {
            this.Id = id;
            this.Name = name;
            this.Cnpj = cnpj;
        }

        public override string ToString()
        {
            return String.Format("{0}, CNPJ: {1}", base.ToString(), this.Cnpj);
        }
    }
}
