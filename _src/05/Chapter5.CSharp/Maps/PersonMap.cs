using System.Data.Entity.ModelConfiguration;
using Chapter5.CSharp.Models;

namespace Chapter5.CSharp.Maps
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
            Ignore(p => p.FullName);
            Property(p => p.BirthDate)
                .HasPrecision(10);
            Property(p => p.HeightInFeet)
                .HasPrecision(4, 2);
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
            Map(p =>
            {
                p.Properties(ph =>
                    new
                    {
                        ph.Photo,
                        ph.FamilyPicture
                    });
                p.ToTable("PersonBlob");
            });
            Map(p =>
            {
                p.Properties(ph =>
                    new
                    {
                        ph.Address,
                        ph.BirthDate,
                        ph.FirstName,
                        ph.HeightInFeet,
                        ph.IsActive,
                        ph.LastName,
                        ph.MiddleName,
                        ph.PersonState,
                        ph.PersonTypeId
                    });
                p.ToTable("Person");
            });
        }
    }
}
