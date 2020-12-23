using System.Data.Entity.ModelConfiguration;
using Chapter4.CSharp.Models;

namespace Chapter4.CSharp.Maps
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