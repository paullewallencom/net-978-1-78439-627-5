Imports System.Data.Entity.ModelConfiguration
Imports Chapter4.VB.Models

Namespace Maps
    Public Class PersonTypeMap
        Inherits EntityTypeConfiguration(Of PersonType)
        Public Sub New()
            HasMany(Function(pt) pt.Persons) _
                .WithOptional(Function(p) p.PersonType) _
                .HasForeignKey(Function(pt) pt.PersonTypeId) _
                .WillCascadeOnDelete(False)
        End Sub
    End Class
End NameSpace