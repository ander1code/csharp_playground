using EF02App.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF02App.Data
{
    public class MyContext: DbContext
    {
        public MyContext() : base("MyContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Invalid:
            /*
                var person = modelBuilder.Entity<Person>();

                person
                .HasKey(p => p.Id)
                .Property(p => p.Name).HasMaxLength(50).IsRequired()
                .Property(p => p.Status).IsRequired();
            */
            
            // Person
            var person = modelBuilder.Entity<Person>();

            person.HasKey(p => p.Id);
           
            person
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();

            person
                .Property(p => p.Status)
                .IsRequired();

            // Natural Person
            var natural_person = modelBuilder.Entity<NaturalPerson>();

            natural_person
                .Property(p => p.CPF)
                .IsRequired();

            natural_person
                .Property(p => p.Salary)
                .IsRequired();

            natural_person
                .Property(p => p.Birthday)
                .IsRequired();

            natural_person
                .Property(p => p.Gender)
                .HasColumnType("char")
                .HasMaxLength(1)
                .IsRequired();

            // Legal Person
            var legal_person = modelBuilder.Entity<LegalPerson>();

            legal_person
                .Property(p => p.CNPJ)
                .IsRequired();

            /*
               Se a herança já está definida nas classes, no Code First 
               do EF6 você não precisa criar um relacionamento adicional entre elas.
            */

            // porém...

            // podemos dividir as entidades em tabelas:

            modelBuilder.Entity<Person>().ToTable("PERSON");

            modelBuilder.Entity<NaturalPerson>().ToTable("NATURAL_PERSON");

            modelBuilder.Entity<LegalPerson>().ToTable("LEGAL_PERSON");

            /*
             * No EF6 Code First, podemos usar TPT (ToTable) para separar a hierarquia em tabelas, sendo que 
             * as classes derivadas compartilham o mesmo Id da classe base, que funciona como chave primária e 
             * também como chave estrangeira (Foreign Key) para a tabela Person, sem precisar configurar 
             * HasForeignKey() manualmente.
            */
            base.OnModelCreating(modelBuilder); 
         }
    }
}
