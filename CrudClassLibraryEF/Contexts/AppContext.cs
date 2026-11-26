using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudClassLibraryEF.Entities;
using System.Data.Entity;

namespace CrudClassLibraryEF.Contexts
{
    // DbContext:
    //     A DbContext instance represents a combination of the Unit Of Work and Repository
    //     patterns such that it can be used to query from a database and group together
    //     changes that will then be written back to the store as a unit.  DbContext
    //     is conceptually similar to ObjectContext.
    public class AppContext : DbContext
    {
        // DbSet:
        //     A DbSet represents the collection of all entities in the context, or that
        //     can be queried from the database, of a given type. DbSet objects are created
        //     from a DbContext using the DbContext.Set method.

        // Precisam ser propridades
        public DbSet<Person> PersonDbSet { get; set; }
        public DbSet<NaturalPerson> NaturalPersonDbSet { get; set; }
        public DbSet<LegalPerson> LegalPersonDbSet { get; set; }

        public AppContext()
            : base("dbReviewsEF")
        {
        }

        // OnModelCreating:
        //     This method is called when the model for a derived context has been initialized,
        //     but before the model has been locked down and used to initialize the context.
        //     The default implementation of this method does nothing, but it can be overridden
        //     in a derived class such that the model can be further configured before it
        //     is locked down.

        // DbModelBuilder:
        //     DbModelBuilder is used to map CLR classes to a database schema.  This code
        //     centric approach to building an Entity Data Model (EDM) model is known as
        //     'Code First'.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Creating tables...
            modelBuilder.Entity<Person>().ToTable("PERSON");

            modelBuilder.Entity<NaturalPerson>().ToTable("NATURALPERSON");

            modelBuilder.Entity<LegalPerson>().ToTable("LEGALPERSON");

            // Creating properties...
            // Person
            modelBuilder.Entity<Person>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Person>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            // NaturalPerson
            modelBuilder.Entity<NaturalPerson>()
                .Property(x => x.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            // LegalPerson
            modelBuilder.Entity<LegalPerson>()
                .Property(x => x.Cnpj)
                .IsRequired()
                .HasMaxLength(14);

            // OnModelCreating:
            //     This method is called when the model for a derived context has been initialized,
            //     but before the model has been locked down and used to initialize the context.
            //     The default implementation of this method does nothing, but it can be overridden
            //     in a derived class such that the model can be further configured before it
            //     is locked down.
            base.OnModelCreating(modelBuilder);
        }
    }
}
