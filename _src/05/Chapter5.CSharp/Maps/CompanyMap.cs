using System.Data.Entity.ModelConfiguration;
using Chapter5.CSharp.Models;

namespace Chapter5.CSharp.Maps
{
    public class CompanyMap : EntityTypeConfiguration<Company>
    {
        public CompanyMap()
        {
            Property(p => p.CompanyName)
                .HasMaxLength(30)
                .IsRequired();
        }
    }
}
