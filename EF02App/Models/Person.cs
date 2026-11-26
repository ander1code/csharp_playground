using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF02App.Models
{
    public abstract class Person
    {
        public int Id { get; set; }

        [MinLength(3)] 
        public string Name { get; set; }

        public bool Status { get; set; }

        // public virtual NaturalPerson NaturalPerson { get; set; }

        // public virtual LegalPerson LegalPerson { get; set; }
        
        public override string ToString()
        {
            return String.Format("ID: {0}, Name: {1}, Status: {2}", this.Id, this.Name, this.Status);
        }
    }
}
