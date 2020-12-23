Imports System.Data.Entity.ModelConfiguration

Public Class PersonMap
    Inherits EntityTypeConfiguration(Of Person)

    Public Sub New()
        Me.Property(Function(p) p.FirstName) _
           .HasMaxLength(30) _
           .IsRequired()
        Me.Property(Function(p) p.LastName) _
            .HasMaxLength(30) _ 
            .IsRequired()
        Me.Property(Function(p) p.MiddleName) _
            .HasMaxLength(1).IsFixedLength().IsUnicode(False)
        Me.Property(Function(p) p.BirthDate) _
            .HasPrecision(1)
        Me.Property(Function(p) p.HeightInFeet) _
           .HasPrecision(4, 2)
        Me.Property(Function(p) p.Photo) _
            .IsVariableLength().HasMaxLength(4000)
    End Sub

End Class
