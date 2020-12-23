using System.Data.Entity;

namespace Chapter3.CSharp
{
    public class Context : DbContext
    {
        public Context()
            : base("name=chapter3")
        {

        }
        public DbSet<Person> People { get; set; }
        public DbSet<Phone> Phones { get; set; }
protected override void OnModelCreating(DbModelBuilder modelBuilder)
{
    modelBuilder.Configurations.Add(new PersonMap());
}

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Person>().Property(p => p.FirstName)
        //        .HasMaxLength(30);
        //    modelBuilder.Entity<Person>().Property(p => p.LastName)
        //        .HasMaxLength(30);
        //    modelBuilder.Entity<Person>().Property(p => p.MiddleName)
        //        .HasMaxLength(1)
        //        .IsFixedLength()
        //        .IsUnicode(false);
        //}
    }
}
