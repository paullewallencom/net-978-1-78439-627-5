Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports Chapter5.VB.Models
Imports Chapter5.VB.Maps

Public Class Context
    Inherits DbContext
    Public Sub New()
        MyBase.New("name=chapter5")
    End Sub
    Property People As DbSet(Of Person)
    Property Phones() As DbSet(Of Phone)
    Property PersonTypes() As DbSet(Of PersonType)
    Property Companies() As DbSet(Of Company)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        modelBuilder.Configurations.Add(New PersonMap)
        modelBuilder.Configurations.Add(New PersonTypeMap)
        modelBuilder.Configurations.Add(New CompanyMap)
        modelBuilder.Configurations.Add(New AddressMap)
    End Sub

End Class
