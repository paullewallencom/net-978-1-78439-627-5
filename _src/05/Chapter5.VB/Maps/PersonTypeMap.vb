Imports System.Data.Entity.ModelConfiguration
Imports Chapter5.VB.Models

Namespace Maps
Public Class PersonTypeMap
    Inherits EntityTypeConfiguration(Of PersonType)
    Public Sub New()
        ToTable("TypeOfPerson")
        Me.Property(Function(p) p.TypeName) _
            .HasMaxLength(30) _
            .IsRequired()
        HasMany(Function(pt) pt.Persons) _
            .WithOptional(Function(p) p.PersonType) _
            .HasForeignKey(Function(pt) pt.PersonTypeId) _
            .WillCascadeOnDelete(False)
    End Sub
End Class
End NameSpace