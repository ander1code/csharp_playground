using EF02App.Models.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF02App.Models
{
    public class NaturalPerson : Person
    {
        [RegularExpression(@"^\d{11}$")]
        public string CPF { get; set; }

        [Range(0, float.MaxValue)]
        public float Salary { get; set; }

        // [Range(typeof(DateTime), DateTime.Now.AddYears(-100).ToShortDateString(), DateTime.Now.AddYears(-18).ToShortDateString())]
        [BirthdayValidation(18)]
        public DateTime Birthday { get; set; }

        [MinLength(1)]
        [RegularExpression(@"^[MFO]$")]
        public string Gender { get; set; }

        // public virtual Person Person { get; set; }

        public override string ToString()
        {
            return String.Format("{0}, CPF: {1}, Salary: {2}, Birthday: {3}, Gender: {4}", base.ToString(), this.CPF, this.Salary, this.Birthday, this.Gender);
        }
    }
}
