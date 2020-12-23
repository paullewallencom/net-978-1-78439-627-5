using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chapter6.CSharp.Models;

namespace Chapter6.CSharp.Maps
{
    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(30);
            Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(30);
            Property(p => p.HeightInFeet)
                .HasPrecision(4, 2);
            Property(p => p.RowVersion)
                //.IsFixedLength()
                //.HasMaxLength(8)
                //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .IsRowVersion();
            HasMany(p => p.Phones)
                .WithRequired()
                .HasForeignKey(ph => ph.PersonId);
            HasMany(p => p.Companies)
                .WithMany(c => c.Persons)
                .Map(m =>
                {
                    m.MapLeftKey("PesonId");
                    m.MapRightKey("CompanyId");
                });
        }
    }
}
