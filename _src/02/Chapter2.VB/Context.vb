Imports System.Data.Entity

Public Class Context
    Inherits DbContext
    Public Sub New()
        MyBase.New("name=chapter2")
    End Sub
    Property People As DbSet(Of Person)
    Property Companies() As DbSet(of Company)
End Class
