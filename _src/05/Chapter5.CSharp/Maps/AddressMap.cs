using System.Data.Entity.ModelConfiguration;
using Chapter5.CSharp.Models;

namespace Chapter5.CSharp.Maps
{
public class AddressMap : ComplexTypeConfiguration<Address>
{
    public AddressMap()
    {
        Property(p => p.Street)
            .HasMaxLength(40)
            .IsRequired()
            .HasColumnName("Street");
        Property(p => p.City)
            .HasMaxLength(30)
            .IsRequired()
            .HasColumnName("City");
        Property(p => p.State)
            .HasMaxLength(2)
            .IsRequired()
            .HasColumnName("State");
        Property(p => p.Zip)
            .HasMaxLength(5)
            .IsRequired()
            .HasColumnName("Zip");
    }
}
}
