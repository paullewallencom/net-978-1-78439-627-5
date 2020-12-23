using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Chapter7.CSharp.Data.Maps;
using Chapter7.CSharp.Data.Models;

namespace Chapter7.CSharp.Data
{
    public class Context : DbContext
    {
        public Context()
            : base("name=chapter7")
        {

        }
        public DbSet<Person> People { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new PersonMap());
        }
    }

    public class NonUnicodeConvention: Convention
    {
        public NonUnicodeConvention()
        {
            Properties<string>()
                .Configure(config=>config.IsUnicode(false));
        }
    }
}
