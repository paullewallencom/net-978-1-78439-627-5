using System.Data.Entity.ModelConfiguration;
using Chapter7.CSharp.Data.Models;

namespace Chapter7.CSharp.Data.Maps
{
    public class PersonMap: EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            Property(p => p.FirstName)
                .HasMaxLength(30)
                .IsRequired();
            Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(30);
            Property(p => p.NickName)
                .HasMaxLength(40)
                .IsRequired();
            Property(p => p.Age)
                .IsRequired();
        }
    }
}
