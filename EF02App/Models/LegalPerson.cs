using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF02App.Models
{
    public class LegalPerson : Person
    {
        [RegularExpression(@"^\d{14}$")]
        public string CNPJ { get; set; }
        //public virtual Person Person { get; set; }

        public override string ToString()
        {
            return String.Format("{0}, CNPJ: {1}", base.ToString(), this.CNPJ);
        }
    }
}
