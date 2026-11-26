using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudClassLibrary.DTOs
{
    public class PersonDTO
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string document;

        public string Document
        {
            get { return document; }
            set { document = value; }
        }

        public PersonDTO(int id, string name, string document)
        {
            this.id = id;
            this.name = name;
            this.document = document;
        }

        public override string ToString()
        {
            return String.Format("ID: {0}, Name: {1}, {2}: {3}", this.id, this.name, document.Length == 11 ? "CPF" : "CNPJ", document);
        }
    }
}
