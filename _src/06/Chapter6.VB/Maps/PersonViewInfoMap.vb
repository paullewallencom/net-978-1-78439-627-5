Imports System.Data.Entity.ModelConfiguration
Imports Chapter6.VB.Models

Namespace Maps
Public Class PersonViewInfoMap
    Inherits EntityTypeConfiguration(Of PersonViewInfo)
    Public Sub New()
        HasKey(Function(p) p.PersonId)
        ToTable("PersonView")
    End Sub
End Class
End Namespace