Imports System.Data.Entity.ModelConfiguration
Imports Chapter7.VB.Data.Models

Namespace Maps

    Public Class PersonMap
        Inherits EntityTypeConfiguration(Of Person)

        Public Sub New
             Me.Property(Function(p) p.FirstName) _ 
                .HasMaxLength(30) _ 
                .IsRequired()
            Me.Property(Function(p) p.LastName) _ 
                .IsRequired() _ 
                .HasMaxLength(30)
            Me.Property(Function(p) p.NickName) _ 
                .HasMaxLength(40) _ 
                .IsRequired()
            Me.Property(Function(p) p.Age) _ 
                .IsRequired()
        End Sub
        
    End Class
End Namespace