using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF02App.Models.Validations
{
    public class BirthdayValidation : ValidationAttribute
    {
        private readonly int mininum_age;

        public BirthdayValidation(int mininum_age)
        {
            this.mininum_age = mininum_age;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            DateTime date = (DateTime)value;
            DateTime min_date = DateTime.Now.AddYears(-this.mininum_age);

            return date <= min_date;
        }
    }
}
