Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class InitialMigration
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Companies",
                Function(c) New With
                    {
                        .CompanyId = c.Int(nullable := False, identity := True),
                        .Name = c.String()
                    }) _
                .PrimaryKey(Function(t) t.CompanyId)
            
            CreateTable(
                "dbo.People",
                Function(c) New With
                    {
                        .PersonId = c.Int(nullable := False, identity := True),
                        .FirstName = c.String(nullable := False, maxLength := 30),
                        .LastName = c.String(nullable := False, maxLength := 30),
                        .NickName = c.String(nullable := False, maxLength := 40),
                        .Age = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.PersonId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.People")
            DropTable("dbo.Companies")
        End Sub
    End Class
End Namespace
