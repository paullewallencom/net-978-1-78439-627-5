Imports System.Data.Entity.ModelConfiguration
Imports Chapter5.VB.Models

Namespace Maps
    Public Class CompanyMap
        Inherits EntityTypeConfiguration(Of Company)
        Public Sub New()
            Me.Property(Function(p) p.CompanyName) _
                .HasMaxLength(30) _
                .IsRequired()
        End Sub
    End Class
End NameSpace