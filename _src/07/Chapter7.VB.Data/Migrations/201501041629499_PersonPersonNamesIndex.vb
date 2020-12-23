Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
Partial Public Class PersonPersonNamesIndex
    Inherits DbMigration

    Public Overrides Sub Up()
        CreateIndex( _
            "People", _
            New String() {"LastName", "FirstName"}, _
            name:="IX_PERSON_NAMES")
    End Sub

    Public Overrides Sub Down()
        DropIndex("People", "IX_PERSON_NAMES")
    End Sub
End Class
End Namespace
