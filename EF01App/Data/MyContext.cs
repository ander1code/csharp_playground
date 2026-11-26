using EF01App.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF01App.Data
{
    public class MyContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<NaturalPerson> NaturalPeople { get; set; }
        public DbSet<LegalPerson> LegalPeople { get; set; }

        public MyContext()
            : base("MyContext")
        {
            
        }
    }
}
