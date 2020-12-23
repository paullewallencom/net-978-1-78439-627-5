Imports System.Data.Entity
Imports Chapter7.VB.Data

Module Module1

    Sub Main()
        Database.SetInitializer(New Initializer)
        Using context = New Context
            context.Database.Initialize(True)
        End Using
    End Sub

End Module
