using System.Data.Entity;

namespace Chapter2.CSharp
{
    public class Context : DbContext
    {
        public Context()
            : base("name=chapter2")
        {

        }
        public DbSet<Person> People { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
