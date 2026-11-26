using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF01App.Models
{
    public class NaturalPerson : Person
    {
        /*
        [Key]
        public int NaturalPersonId { get; set; }
        */
        [Required]
        [RegularExpression(@"^\d{11}$")]
        public string CPF { get; set; }

        [Required]
        [Range(0, float.MaxValue)]
        public float Salary { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        [StringLength(1, MinimumLength = 1)]
        [Column(TypeName="char")]
        [RegularExpression(@"^[MFO]$")]
        public string Gender { get; set; }

        public virtual Person Person { get; set; }

        public override string ToString()
        {
            return String.Format("{0}, CPF: {1}, Salary: {2}, Birthday: {3}, Gender: {4}", base.ToString(), this.CPF, this.Salary, this.Birthday, this.Gender);
        }
    }
}
