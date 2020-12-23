using System.Data.Entity.ModelConfiguration;
using Chapter5.CSharp.Models;

namespace Chapter5.CSharp.Maps
{
public class PersonTypeMap : EntityTypeConfiguration<PersonType>
{
public PersonTypeMap()
{
    ToTable("TypeOfPerson");
    Property(p => p.TypeName)
            .HasMaxLength(30)
            .IsRequired();
    HasMany(pt => pt.Persons)
        .WithOptional(p => p.PersonType)
        .HasForeignKey(p => p.PersonTypeId)
        .WillCascadeOnDelete(false);
}
}
}