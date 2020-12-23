using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Chapter4.CSharp.Maps;
using Chapter4.CSharp.Models;

namespace Chapter4.CSharp
{
    public class Context : DbContext
    {
        public Context()
            : base("name=chapter4")
        {

        }
        public DbSet<Person> People { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<PersonType> PersonTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new PersonTypeMap());
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

    }
}
