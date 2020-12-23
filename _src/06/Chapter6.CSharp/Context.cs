using System.Data.Entity;
using Chapter6.CSharp.Maps;
using Chapter6.CSharp.Models;

namespace Chapter6.CSharp
{
    public class Context : DbContext
    {
        public Context()
            : base("name=chapter6")
        {

        }
        public DbSet<Person> People { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<PersonType> PersonTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<PersonViewInfo> PersonView { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new PersonTypeMap());
            modelBuilder.Configurations.Add(new PersonViewInfoMap());
            modelBuilder.Configurations.Add(new CompanyMap());
            
        }


    }
}
