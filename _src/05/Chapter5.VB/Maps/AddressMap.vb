Imports System.Data.Entity.ModelConfiguration
Imports Chapter5.VB.Models

Namespace Maps

Public Class AddressMap
    Inherits ComplexTypeConfiguration(Of Address)
    Public Sub New()
        Me.Property(Function(p) p.Street) _ 
            .HasMaxLength(40) _ 
            .IsRequired() _ 
            .HasColumnName("Street")
        Me.Property(Function(p) p.City).HasMaxLength(30).IsRequired().HasColumnName("City")
        Me.Property(Function(p) p.State).HasMaxLength(2).IsRequired().HasColumnName("State")
        Me.Property(Function(p) p.Zip).HasMaxLength(5).IsRequired().HasColumnName("Zip")
    End Sub
End Class
End NameSpace