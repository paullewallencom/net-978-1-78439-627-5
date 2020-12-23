Imports System.Data.Entity

Public Class Initializer
    Inherits DropCreateDatabaseIfModelChanges(Of Context)

    Protected Overrides Sub Seed(ByVal context As Context)
        context.Companies.Add(New Company() With {
             .Name = "My company"
        })
    End Sub
End Class
