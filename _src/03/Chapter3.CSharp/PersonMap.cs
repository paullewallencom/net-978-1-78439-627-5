using System.Data.Entity.ModelConfiguration;

namespace Chapter3.CSharp
{
    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            Property(p => p.FirstName)
                .HasMaxLength(30)
                .IsRequired();
            Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(30);
            Property(p => p.MiddleName)
                .HasMaxLength(1)
                .IsFixedLength()
                .IsUnicode(false);
            Property(p => p.BirthDate)
                .HasPrecision(10);
            Property(p => p.HeightInFeet)
                .HasPrecision(4, 2);
            Property(p => p.Photo)
                .IsVariableLength()
                .HasMaxLength(4000);
            HasMany(p => p.Phones)
                .WithRequired()
                .HasForeignKey(ph => ph.PersonId);
        }
    }
}
