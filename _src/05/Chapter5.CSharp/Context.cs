using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Chapter5.CSharp.Maps;
using Chapter5.CSharp.Models;

namespace Chapter5.CSharp
{
    public class Context : DbContext
    {
        public Context()
            : base("name=Chapter5")
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
            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new AddressMap());
        }

    }
}
