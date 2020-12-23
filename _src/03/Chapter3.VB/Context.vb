Imports System.Data.Entity

Public Class Context
    Inherits DbContext
    Public Sub New()
        MyBase.New("name=chapter3")
    End Sub
    Property People As DbSet(Of Person)
    Property Phones() As DbSet(Of Phone)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        modelBuilder.Configurations.Add(New PersonMap)
    End Sub

    'Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
    '    modelBuilder.Entity(of Person)().Property(Function(p) p.FirstName) _ 
    '        .HasMaxLength(30)

    '        modelBuilder.Entity(of Person)().Property(Function(p) p.LastName) _
    '            .HasMaxLength(30)

    '        modelBuilder.Entity(of Person)().Property(Function(p) p.MiddleName) _
    '            .HasMaxLength(1).IsFixedLength().IsUnicode(False)
    'End Sub
End Class
