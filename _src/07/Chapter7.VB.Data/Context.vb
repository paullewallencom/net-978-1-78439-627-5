Imports System.Data.Entity
Imports Chapter7.VB.Data.Models
Imports Chapter7.VB.Data.Maps

Public Class Context
    Inherits DbContext
    Public Sub New()
        MyBase.New("name=chapter7")
    End Sub
    Property People As DbSet(Of Person)
    Property Companies() As DbSet(Of Company)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        MyBase.OnModelCreating(modelBuilder)
        modelBuilder.Configurations.Add(New PersonMap())
    End Sub
End Class
