using System.Data.Entity.ModelConfiguration;
using Chapter6.CSharp.Models;

namespace Chapter6.CSharp.Maps
{
    public class PersonViewInfoMap :
        EntityTypeConfiguration<PersonViewInfo>
    {
        public PersonViewInfoMap()
        {
            HasKey(p => p.PersonId);
            ToTable("PersonView");
        }
    }
}
