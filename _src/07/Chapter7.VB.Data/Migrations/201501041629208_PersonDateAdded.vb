Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class PersonDateAdded
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.People", "DateAdded", Function(c) c.DateTime(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.People", "DateAdded")
        End Sub
    End Class
End Namespace
