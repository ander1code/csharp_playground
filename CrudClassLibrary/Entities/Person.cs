using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudClassLibrary.Entities
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return String.Format("ID: {0}, Name: {1}", this.Id, this.Name);
        }
    }
}
