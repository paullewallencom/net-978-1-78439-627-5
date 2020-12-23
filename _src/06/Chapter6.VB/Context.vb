Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports Chapter6.VB.Models
Imports Chapter6.VB.Maps

Public Class Context
    Inherits DbContext
    Public Sub New()
        MyBase.New("name=chapter6")
    End Sub
    Property People As DbSet(Of Person)
    Property Phones() As DbSet(Of Phone)
    Property PersonTypes() As DbSet(Of PersonType)
    Property Companies() As DbSet(Of Company)
    Property PersonView() As DbSet(Of PersonViewInfo)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        modelBuilder.Configurations.Add(New PersonMap)
        modelBuilder.Configurations.Add(New PersonTypeMap)
        modelBuilder.Configurations.Add(New PersonViewInfoMap)
        modelBuilder.Configurations.Add(new CompanyMap)
    End Sub


End Class
