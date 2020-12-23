Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports Chapter4.VB.Models
Imports Chapter4.VB.Maps

Public Class Context
    Inherits DbContext
    Public Sub New()
        MyBase.New("name=chapter4")
    End Sub
    Property People As DbSet(Of Person)
    Property Phones() As DbSet(Of Phone)
    Property PersonTypes() As DbSet(Of PersonType)
    Property Companies() As DbSet(Of Company)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        modelBuilder.Configurations.Add(New PersonMap)
        modelBuilder.Configurations.Add(New PersonTypeMap)
        modelBuilder.Conventions.Remove(Of OneToManyCascadeDeleteConvention)()
        modelBuilder.Conventions.Remove(Of ManyToManyCascadeDeleteConvention)()
    End Sub

End Class
