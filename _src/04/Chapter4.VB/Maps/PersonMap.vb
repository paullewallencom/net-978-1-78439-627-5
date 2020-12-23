Imports System.Data.Entity.ModelConfiguration
Imports Chapter4.VB.Models

Namespace Maps
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

            HasMany(Function(p) p.Companies) _
                .WithMany(Function(c) c.Persons) _
                .Map(Sub(m)
                    m.MapLeftKey("PesonId")
                    m.MapRightKey("CompanyId")
                        End Sub)
        End Sub

    End Class
End NameSpace