using System.Data.Entity.ModelConfiguration;
using Chapter6.CSharp.Models;

namespace Chapter6.CSharp.Maps
{
    public class PersonTypeMap : EntityTypeConfiguration<PersonType>
    {
        public PersonTypeMap()
        {
            HasMany(pt => pt.Persons)
                .WithOptional(p => p.PersonType)
                .HasForeignKey(p => p.PersonTypeId)
                .WillCascadeOnDelete(false);
        }
    }
}