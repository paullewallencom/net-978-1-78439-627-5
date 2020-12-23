Imports System.Data.Entity.ModelConfiguration
Imports Chapter6.VB.Models
Imports System.ComponentModel.DataAnnotations.Schema

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
            Me.Property(Function(p) p.HeightInFeet) _
                .HasPrecision(4, 2)
            Me.Property(Function(p) p.RowVersion) _
                .IsFixedLength() _
                .HasMaxLength(8) _
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed) _
                .IsRowVersion()
            HasMany(Function(p) p.Phones) _
                .WithRequired() _
                .HasForeignKey(Function(p) p.PersonId)
            HasMany(Function(p) p.Companies) _
                .WithMany(Function(c) c.Persons) _
                .Map(Sub(m)
                         m.MapLeftKey("PesonId")
                         m.MapRightKey("CompanyId")
                     End Sub)
        End Sub
    End Class
End Namespace