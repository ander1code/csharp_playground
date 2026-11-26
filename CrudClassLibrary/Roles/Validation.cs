using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudClassLibrary.Roles
{
    internal class Validation
    {
        private static Validation instance = null;

        private Validation()
        {
        }

        public static Validation Instance
        {
            get
            {
                if (instance == null)
                {
                    Validation.instance = new Validation();
                }
                return Validation.instance;
            }
        }
    }
}
